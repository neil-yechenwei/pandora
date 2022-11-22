using Pandora.Definitions.Attributes;
using System.ComponentModel;

namespace Pandora.Definitions.ResourceManager.Batch.v2022_10_01.Pool;

[ConstantType(ConstantTypeAttribute.ConstantType.String)]
internal enum CertificateStoreLocationConstant
{
    [Description("CurrentUser")]
    CurrentUser,

    [Description("LocalMachine")]
    LocalMachine,
}