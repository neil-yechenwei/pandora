using Pandora.Definitions.Attributes;
using System.ComponentModel;

namespace Pandora.Definitions.ResourceManager.DataFactory.v2018_06_01.IntegrationRuntimes;

[ConstantType(ConstantTypeAttribute.ConstantType.String)]
internal enum IntegrationRuntimeAutoUpdateConstant
{
    [Description("Off")]
    Off,

    [Description("On")]
    On,
}
