using Pandora.Definitions.Attributes;
using System.ComponentModel;

namespace Pandora.Definitions.ResourceManager.Resources.v2023_07_01.Providers;

[ConstantType(ConstantTypeAttribute.ConstantType.String)]
internal enum AliasPatternTypeConstant
{
    [Description("Extract")]
    Extract,

    [Description("NotSpecified")]
    NotSpecified,
}
