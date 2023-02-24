using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Pandora.Definitions.Attributes;
using Pandora.Definitions.Attributes.Validation;
using Pandora.Definitions.CustomTypes;


// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See NOTICE.txt in the project root for license information.


namespace Pandora.Definitions.ResourceManager.DataFactory.v2018_06_01.Factories;


internal class FactoryRepoUpdateModel
{
    [JsonPropertyName("factoryResourceId")]
    public string? FactoryResourceId { get; set; }

    [JsonPropertyName("repoConfiguration")]
    public FactoryRepoConfigurationModel? RepoConfiguration { get; set; }
}
