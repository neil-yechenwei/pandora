using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Pandora.Definitions.Attributes;
using Pandora.Definitions.Attributes.Validation;
using Pandora.Definitions.CustomTypes;


// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See NOTICE.txt in the project root for license information.


namespace Pandora.Definitions.ResourceManager.Resources.v2023_07_01.Providers;


internal class ProviderPermissionModel
{
    [JsonPropertyName("applicationId")]
    public string? ApplicationId { get; set; }

    [JsonPropertyName("managedByRoleDefinition")]
    public RoleDefinitionModel? ManagedByRoleDefinition { get; set; }

    [JsonPropertyName("providerAuthorizationConsentState")]
    public ProviderAuthorizationConsentStateConstant? ProviderAuthorizationConsentState { get; set; }

    [JsonPropertyName("roleDefinition")]
    public RoleDefinitionModel? RoleDefinition { get; set; }
}
