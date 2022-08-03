using System.Collections.Generic;
using Pandora.Definitions.Interfaces;

namespace Pandora.Definitions.ResourceManager.Maintenance.v2021_05_01;

public partial class Definition : ApiVersionDefinition
{
    public string ApiVersion => "2021-05-01";
    public bool Preview => false;
    public Source Source => Source.ResourceManagerRestApiSpecs;

    public IEnumerable<ResourceDefinition> Resources => new List<ResourceDefinition>
    {
        new ApplyUpdate.Definition(),
        new ApplyUpdates.Definition(),
        new ConfigurationAssignments.Definition(),
        new MaintenanceConfigurations.Definition(),
        new PublicMaintenanceConfigurations.Definition(),
        new Updates.Definition(),
    };
}