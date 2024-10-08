// Copyright (c) HashiCorp, Inc.
// SPDX-License-Identifier: MPL-2.0

package pipeline

import (
	"fmt"
	"strings"

	"github.com/hashicorp/go-azure-helpers/lang/pointer"
	sdkModels "github.com/hashicorp/pandora/tools/data-api-sdk/v1/models"
	"github.com/hashicorp/pandora/tools/importer-msgraph-metadata/components/normalize"
	"github.com/hashicorp/pandora/tools/importer-msgraph-metadata/components/parser"
	"github.com/hashicorp/pandora/tools/importer-msgraph-metadata/components/versions"
	"github.com/hashicorp/pandora/tools/importer-msgraph-metadata/internal/logging"
)

func (p pipelineForService) translateServiceToDataApiSdkTypes() (*sdkModels.Service, error) {
	sdkService := sdkModels.Service{
		APIVersions:         make(map[string]sdkModels.APIVersion),
		Generate:            true,
		Name:                normalize.CleanName(p.service),
		ResourceProvider:    nil,
		TerraformDefinition: nil,
	}

	for _, resource := range p.resources[p.service] {
		// First scaffold the version and SDK resources (categories)
		if _, ok := sdkService.APIVersions[resource.Version]; !ok {
			sdkService.APIVersions[resource.Version] = sdkModels.APIVersion{
				APIVersion: resource.Version,
				Generate:   true,
				Preview:    versions.IsPreview(resource.Version),
				Resources:  make(map[string]sdkModels.APIResource),
				Source:     sdkModels.MicrosoftGraphMetaDataSourceDataOrigin,
			}
		}

		if _, ok := sdkService.APIVersions[resource.Version].Resources[resource.Category]; !ok {
			sdkService.APIVersions[resource.Version].Resources[resource.Category] = sdkModels.APIResource{
				Constants:   make(map[string]sdkModels.SDKConstant),
				Models:      make(map[string]sdkModels.SDKModel),
				Operations:  make(map[string]sdkModels.SDKOperation),
				ResourceIDs: make(map[string]sdkModels.ResourceID),
			}
		}

		serviceConstants := make(parser.Constants)
		serviceModels := make(parser.Models)

		// Populate everything else
		for _, operation := range resource.Operations {
			var resourceIdName *string

			// Note we longer output resource IDs per service, they are now common types
			if operation.ResourceId != nil {
				resourceIdName = &operation.ResourceId.Name
			}

			options := map[string]sdkModels.SDKOperationOption{
				// All operations can specify the odata.metadata Accept parameter
				"Metadata": {
					Type:           sdkModels.SDKOperationOptionTypeData,
					ODataFieldName: pointer.To("Metadata"),
					ObjectDefinition: sdkModels.SDKOperationOptionObjectDefinition{
						ReferenceName: pointer.To("odata.Metadata"),
						Type:          sdkModels.ReferenceSDKOperationOptionObjectDefinitionType,
					},
				},

				// All operations can accept a custom RetryFunc
				"RetryFunc": {
					Type: sdkModels.SDKOperationOptionTypeRetryFunc,
				},
			}

			var requestObject *sdkModels.SDKObjectDefinition

			if operation.RequestModelName != nil {
				schemaName := *operation.RequestModelName
				requestObjectIsCommonType := true

				var model *parser.Model
				if operation.RequestModel != nil {
					model = operation.RequestModel
				} else if p.models.Found(schemaName) {
					model = p.models[schemaName]
				}

				if model == nil {
					return nil, fmt.Errorf("request model %q was not found for operation: %s", schemaName, operation.Name)
				}

				if !model.Common {
					requestObjectIsCommonType = false
					serviceModels[schemaName] = model
				}

				requestObject = &sdkModels.SDKObjectDefinition{
					ReferenceName:             pointer.To(normalize.CleanName(*operation.RequestModelName)),
					ReferenceNameIsCommonType: &requestObjectIsCommonType,
					Type:                      sdkModels.ReferenceSDKObjectDefinitionType,
				}
			} else if operation.RequestType != nil {
				requestObject = &sdkModels.SDKObjectDefinition{
					// This is a regular type, i.e. not a model or typed constant
					Type: operation.RequestType.DataApiSdkObjectDefinitionType(),
				}

				if *operation.RequestType == parser.DataTypeBinary {
					// Add a ContentType option so the caller can specify the media type for the request body
					options["ContentType"] = sdkModels.SDKOperationOption{
						Type: sdkModels.SDKOperationOptionTypeContentType,
					}
				}
			}

			if operation.RequestHeaders != nil {
				for _, header := range *operation.RequestHeaders {
					// Some operations support the ConsistencyLevel header, this is structured via the odata package
					if strings.EqualFold(header.Name, "ConsistencyLevel") {
						options[normalize.CleanName(header.Name)] = sdkModels.SDKOperationOption{
							ODataFieldName: &header.Name,
							ObjectDefinition: sdkModels.SDKOperationOptionObjectDefinition{
								ReferenceName: pointer.To("odata.ConsistencyLevel"),
								Type:          sdkModels.ReferenceSDKOperationOptionObjectDefinitionType,
							},
						}
						continue
					}

					objectDefinition, err := header.DataApiSdkObjectDefinition()
					if err != nil {
						return nil, err
					}

					if objectDefinition == nil {
						logging.Warnf("could not determine SDKOperationOptionObjectDefinition for header %q, skipping", header.Name)
						continue
					}

					options[normalize.CleanName(header.Name)] = sdkModels.SDKOperationOption{
						HeaderName:       &header.Name,
						Required:         false,
						ObjectDefinition: *objectDefinition,
					}
				}
			}

			if operation.RequestParams != nil {
				for _, param := range *operation.RequestParams {
					switch normalize.CleanName(param.Name) {
					case "Count":
						options["Count"] = sdkModels.SDKOperationOption{
							ODataFieldName: pointer.To("Count"),
							ObjectDefinition: sdkModels.SDKOperationOptionObjectDefinition{
								Type: sdkModels.BooleanSDKOperationOptionObjectDefinitionType,
							},
						}

					case "Expand":
						options["Expand"] = sdkModels.SDKOperationOption{
							ODataFieldName: pointer.To("Expand"),
							ObjectDefinition: sdkModels.SDKOperationOptionObjectDefinition{
								ReferenceName: pointer.To("odata.Expand"),
								Type:          sdkModels.ReferenceSDKOperationOptionObjectDefinitionType,
							},
						}

					case "Format":
						options["Format"] = sdkModels.SDKOperationOption{
							ODataFieldName: pointer.To("Format"),
							ObjectDefinition: sdkModels.SDKOperationOptionObjectDefinition{
								ReferenceName: pointer.To("odata.Format"),
								Type:          sdkModels.ReferenceSDKOperationOptionObjectDefinitionType,
							},
						}

					case "OrderBy":
						options["OrderBy"] = sdkModels.SDKOperationOption{
							ODataFieldName: pointer.To("OrderBy"),
							ObjectDefinition: sdkModels.SDKOperationOptionObjectDefinition{
								ReferenceName: pointer.To("odata.OrderBy"),
								Type:          sdkModels.ReferenceSDKOperationOptionObjectDefinitionType,
							},
						}

					case "Select":
						options["Select"] = sdkModels.SDKOperationOption{
							ODataFieldName: pointer.To("Select"),
							ObjectDefinition: sdkModels.SDKOperationOptionObjectDefinition{
								NestedItem: &sdkModels.SDKOperationOptionObjectDefinition{
									Type: sdkModels.StringSDKOperationOptionObjectDefinitionType,
								},
								Type: sdkModels.ListSDKOperationOptionObjectDefinitionType,
							},
						}

					case "Filter", "Search":
						options[normalize.CleanName(param.Name)] = sdkModels.SDKOperationOption{
							ODataFieldName: pointer.To(normalize.CleanName(param.Name)),
							ObjectDefinition: sdkModels.SDKOperationOptionObjectDefinition{
								Type: sdkModels.StringSDKOperationOptionObjectDefinitionType,
							},
						}

					case "Skip", "Top":
						// Don't set here, we handle this implicitly for list operations

					default:
						objectDefinition, err := param.DataApiSdkObjectDefinition()
						if err != nil {
							return nil, err
						}

						if objectDefinition == nil {
							logging.Warnf("could not determine SDKOperationOptionObjectDefinition for param %q, skipping", param.Name)
							continue
						}

						options[normalize.CleanName(param.Name)] = sdkModels.SDKOperationOption{
							QueryStringName:  &param.Name,
							Required:         false,
							ObjectDefinition: *objectDefinition,
						}
					}
				}
			}

			// Allow the caller to control paging for list operations
			if operation.Type == parser.OperationTypeList {
				options["Top"] = sdkModels.SDKOperationOption{
					ODataFieldName: pointer.To("Top"),
					ObjectDefinition: sdkModels.SDKOperationOptionObjectDefinition{
						Type: sdkModels.IntegerSDKOperationOptionObjectDefinitionType,
					},
				}

				options["Skip"] = sdkModels.SDKOperationOption{
					ODataFieldName: pointer.To("Skip"),
					ObjectDefinition: sdkModels.SDKOperationOptionObjectDefinition{
						Type: sdkModels.IntegerSDKOperationOptionObjectDefinitionType,
					},
				}
			}

			var responseObject *sdkModels.SDKObjectDefinition

			if operation.ResponseType != nil && *operation.ResponseType == parser.DataTypeReference && operation.ResponseReferenceName != nil {
				schemaName := *operation.ResponseReferenceName
				responseObjectIsCommonType := true

				if !p.constants.Found(schemaName) && !p.models.Found(schemaName) {
					return nil, fmt.Errorf("response constant or model %q was not found for operation: %s", schemaName, operation.Name)
				}
				if p.constants.Found(schemaName) && p.models.Found(schemaName) {
					return nil, fmt.Errorf("response object %q was found as both a constant and a model for operation: %s", schemaName, operation.Name)
				}

				if p.constants.Found(schemaName) {
					constant := p.constants[schemaName]

					if !constant.Common {
						responseObjectIsCommonType = false
						serviceConstants[schemaName] = constant
					}
				}

				if p.models.Found(schemaName) {
					model := p.models[schemaName]

					if !model.Common {
						responseObjectIsCommonType = false
						serviceModels[schemaName] = model
					}
				}

				responseObject = &sdkModels.SDKObjectDefinition{
					ReferenceName:             pointer.To(normalize.CleanName(schemaName)),
					ReferenceNameIsCommonType: &responseObjectIsCommonType,
					Type:                      sdkModels.ReferenceSDKObjectDefinitionType,
				}
			} else if operation.ResponseType != nil {
				responseObject = &sdkModels.SDKObjectDefinition{
					Type: operation.ResponseType.DataApiSdkObjectDefinitionType(),
				}
			}

			contentType := "application/json"
			if operation.ResponseContentType != nil && *operation.ResponseContentType != "" {
				contentType = *operation.ResponseContentType
			}

			sdkService.APIVersions[resource.Version].Resources[resource.Category].Operations[operation.Name] = sdkModels.SDKOperation{
				ContentType:                      contentType,
				Description:                      operation.Description,
				ExpectedStatusCodes:              operation.ResponseStatusCodes,
				FieldContainingPaginationDetails: operation.PaginationField,
				LongRunning:                      false,
				Method:                           operation.Method,
				Options:                          options,
				ResourceIDName:                   resourceIdName,
				ResourceIDNameIsCommonType:       pointer.To(true),
				URISuffix:                        operation.UriSuffix,
				RequestObject:                    requestObject,
				ResponseObject:                   responseObject,
			}
		}

		// Append any service-local models that were discovered
		for _, model := range serviceModels {
			sdkModel, err := model.DataApiSdkModel(p.models, p.constants)
			if err != nil {
				return nil, err
			}

			sdkService.APIVersions[resource.Version].Resources[resource.Category].Models[model.Name] = *sdkModel
		}
	}

	return &sdkService, nil
}
