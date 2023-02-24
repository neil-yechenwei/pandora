using Pandora.Definitions.Attributes;
using System.ComponentModel;

namespace Pandora.Definitions.ResourceManager.SecurityInsights.v2022_10_01_preview.AutomationRules;

[ConstantType(ConstantTypeAttribute.ConstantType.String)]
internal enum AutomationRuleBooleanConditionSupportedOperatorConstant
{
    [Description("And")]
    And,

    [Description("Or")]
    Or,
}
