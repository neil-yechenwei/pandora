using Pandora.Definitions.Attributes;
using Pandora.Definitions.Interfaces;
using Pandora.Definitions.Operations;
using System;
using System.Collections.Generic;
using System.Net;

namespace Pandora.Definitions.ResourceManager.Purview.v2020_12_01_preview.PrivateLinkResource
{
    internal class GetByGroupIdOperation : Operations.GetOperation
    {
        public override ResourceID? ResourceId()
        {
            return new PrivateLinkResourceId();
        }

        public override Type? ResponseObject()
        {
            return typeof(PrivateLinkResourceModel);
        }


    }
}
