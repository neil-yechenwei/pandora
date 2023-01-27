using Pandora.Definitions.Attributes;
using Pandora.Definitions.CustomTypes;
using Pandora.Definitions.Interfaces;
using Pandora.Definitions.Operations;
using System;
using System.Collections.Generic;
using System.Net;


// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See NOTICE.txt in the project root for license information.


namespace Pandora.Definitions.ResourceManager.DataMigration.v2021_06_30.ServiceTaskResource;

internal class ServiceTasksDeleteOperation : Operations.DeleteOperation
{
    public override IEnumerable<HttpStatusCode> ExpectedStatusCodes() => new List<HttpStatusCode>
        {
                HttpStatusCode.NoContent,
                HttpStatusCode.OK,
        };

    public override ResourceID? ResourceId() => new ServiceTaskId();

    public override Type? OptionsObject() => typeof(ServiceTasksDeleteOperation.ServiceTasksDeleteOptions);

    internal class ServiceTasksDeleteOptions
    {
        [QueryStringName("deleteRunningTasks")]
        [Optional]
        public bool DeleteRunningTasks { get; set; }
    }
}