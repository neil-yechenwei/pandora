using Pandora.Definitions.Attributes;
using System.ComponentModel;

namespace Pandora.Definitions.ResourceManager.ContainerRegistry.v2023_07_01.Registries;

[ConstantType(ConstantTypeAttribute.ConstantType.String)]
internal enum PasswordNameConstant
{
    [Description("password")]
    Password,

    [Description("password2")]
    PasswordTwo,
}
