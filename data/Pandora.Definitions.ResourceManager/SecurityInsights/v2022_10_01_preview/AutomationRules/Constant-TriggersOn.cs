using Pandora.Definitions.Attributes;
using System.ComponentModel;

namespace Pandora.Definitions.ResourceManager.SecurityInsights.v2022_10_01_preview.AutomationRules;

[ConstantType(ConstantTypeAttribute.ConstantType.String)]
internal enum TriggersOnConstant
{
    [Description("Alerts")]
    Alerts,

    [Description("Incidents")]
    Incidents,
}
