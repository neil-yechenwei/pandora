using Pandora.Definitions.Attributes;
using System.ComponentModel;

namespace Pandora.Definitions.ResourceManager.Network.v2023_05_01.ConnectivityConfigurations;

[ConstantType(ConstantTypeAttribute.ConstantType.String)]
internal enum GroupConnectivityConstant
{
    [Description("DirectlyConnected")]
    DirectlyConnected,

    [Description("None")]
    None,
}
