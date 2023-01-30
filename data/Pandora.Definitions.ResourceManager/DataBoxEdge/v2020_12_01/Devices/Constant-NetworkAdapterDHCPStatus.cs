using Pandora.Definitions.Attributes;
using System.ComponentModel;

namespace Pandora.Definitions.ResourceManager.DataBoxEdge.v2020_12_01.Devices;

[ConstantType(ConstantTypeAttribute.ConstantType.String)]
internal enum NetworkAdapterDHCPStatusConstant
{
    [Description("Disabled")]
    Disabled,

    [Description("Enabled")]
    Enabled,
}