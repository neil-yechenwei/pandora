using System.Collections.Generic;
using Pandora.Definitions.Interfaces;


// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See NOTICE.txt in the project root for license information.


namespace Pandora.Definitions.ResourceManager.Network.v2023_05_01.NetworkSecurityGroups;

internal class Definition : ResourceDefinition
{
    public string Name => "NetworkSecurityGroups";
    public IEnumerable<Interfaces.ApiOperation> Operations => new List<Interfaces.ApiOperation>
    {
        new CreateOrUpdateOperation(),
        new DeleteOperation(),
        new GetOperation(),
        new ListOperation(),
        new ListAllOperation(),
        new UpdateTagsOperation(),
    };
    public IEnumerable<System.Type> Constants => new List<System.Type>
    {
        typeof(DdosSettingsProtectionModeConstant),
        typeof(DeleteOptionsConstant),
        typeof(FlowLogFormatTypeConstant),
        typeof(GatewayLoadBalancerTunnelInterfaceTypeConstant),
        typeof(GatewayLoadBalancerTunnelProtocolConstant),
        typeof(IPAllocationMethodConstant),
        typeof(IPVersionConstant),
        typeof(LoadBalancerBackendAddressAdminStateConstant),
        typeof(NatGatewaySkuNameConstant),
        typeof(NetworkInterfaceAuxiliaryModeConstant),
        typeof(NetworkInterfaceAuxiliarySkuConstant),
        typeof(NetworkInterfaceMigrationPhaseConstant),
        typeof(NetworkInterfaceNicTypeConstant),
        typeof(ProvisioningStateConstant),
        typeof(PublicIPAddressDnsSettingsDomainNameLabelScopeConstant),
        typeof(PublicIPAddressMigrationPhaseConstant),
        typeof(PublicIPAddressSkuNameConstant),
        typeof(PublicIPAddressSkuTierConstant),
        typeof(RouteNextHopTypeConstant),
        typeof(SecurityRuleAccessConstant),
        typeof(SecurityRuleDirectionConstant),
        typeof(SecurityRuleProtocolConstant),
        typeof(SyncModeConstant),
        typeof(TransportProtocolConstant),
        typeof(VirtualNetworkPrivateEndpointNetworkPoliciesConstant),
        typeof(VirtualNetworkPrivateLinkServiceNetworkPoliciesConstant),
    };
    public IEnumerable<System.Type> Models => new List<System.Type>
    {
        typeof(ApplicationGatewayBackendAddressModel),
        typeof(ApplicationGatewayBackendAddressPoolModel),
        typeof(ApplicationGatewayBackendAddressPoolPropertiesFormatModel),
        typeof(ApplicationGatewayIPConfigurationModel),
        typeof(ApplicationGatewayIPConfigurationPropertiesFormatModel),
        typeof(ApplicationSecurityGroupModel),
        typeof(ApplicationSecurityGroupPropertiesFormatModel),
        typeof(BackendAddressPoolModel),
        typeof(BackendAddressPoolPropertiesFormatModel),
        typeof(CustomDnsConfigPropertiesFormatModel),
        typeof(DdosSettingsModel),
        typeof(DelegationModel),
        typeof(FlowLogModel),
        typeof(FlowLogFormatParametersModel),
        typeof(FlowLogPropertiesFormatModel),
        typeof(FrontendIPConfigurationModel),
        typeof(FrontendIPConfigurationPropertiesFormatModel),
        typeof(GatewayLoadBalancerTunnelInterfaceModel),
        typeof(IPConfigurationModel),
        typeof(IPConfigurationProfileModel),
        typeof(IPConfigurationProfilePropertiesFormatModel),
        typeof(IPConfigurationPropertiesFormatModel),
        typeof(IPTagModel),
        typeof(InboundNatRuleModel),
        typeof(InboundNatRulePropertiesFormatModel),
        typeof(LoadBalancerBackendAddressModel),
        typeof(LoadBalancerBackendAddressPropertiesFormatModel),
        typeof(NatGatewayModel),
        typeof(NatGatewayPropertiesFormatModel),
        typeof(NatGatewaySkuModel),
        typeof(NatRulePortMappingModel),
        typeof(NetworkInterfaceModel),
        typeof(NetworkInterfaceDnsSettingsModel),
        typeof(NetworkInterfaceIPConfigurationModel),
        typeof(NetworkInterfaceIPConfigurationPrivateLinkConnectionPropertiesModel),
        typeof(NetworkInterfaceIPConfigurationPropertiesFormatModel),
        typeof(NetworkInterfacePropertiesFormatModel),
        typeof(NetworkInterfaceTapConfigurationModel),
        typeof(NetworkInterfaceTapConfigurationPropertiesFormatModel),
        typeof(NetworkSecurityGroupModel),
        typeof(NetworkSecurityGroupPropertiesFormatModel),
        typeof(PrivateEndpointModel),
        typeof(PrivateEndpointConnectionModel),
        typeof(PrivateEndpointConnectionPropertiesModel),
        typeof(PrivateEndpointIPConfigurationModel),
        typeof(PrivateEndpointIPConfigurationPropertiesModel),
        typeof(PrivateEndpointPropertiesModel),
        typeof(PrivateLinkServiceModel),
        typeof(PrivateLinkServiceConnectionModel),
        typeof(PrivateLinkServiceConnectionPropertiesModel),
        typeof(PrivateLinkServiceConnectionStateModel),
        typeof(PrivateLinkServiceIPConfigurationModel),
        typeof(PrivateLinkServiceIPConfigurationPropertiesModel),
        typeof(PrivateLinkServicePropertiesModel),
        typeof(PublicIPAddressModel),
        typeof(PublicIPAddressDnsSettingsModel),
        typeof(PublicIPAddressPropertiesFormatModel),
        typeof(PublicIPAddressSkuModel),
        typeof(ResourceNavigationLinkModel),
        typeof(ResourceNavigationLinkFormatModel),
        typeof(ResourceSetModel),
        typeof(RetentionPolicyParametersModel),
        typeof(RouteModel),
        typeof(RoutePropertiesFormatModel),
        typeof(RouteTableModel),
        typeof(RouteTablePropertiesFormatModel),
        typeof(SecurityRuleModel),
        typeof(SecurityRulePropertiesFormatModel),
        typeof(ServiceAssociationLinkModel),
        typeof(ServiceAssociationLinkPropertiesFormatModel),
        typeof(ServiceDelegationPropertiesFormatModel),
        typeof(ServiceEndpointPolicyModel),
        typeof(ServiceEndpointPolicyDefinitionModel),
        typeof(ServiceEndpointPolicyDefinitionPropertiesFormatModel),
        typeof(ServiceEndpointPolicyPropertiesFormatModel),
        typeof(ServiceEndpointPropertiesFormatModel),
        typeof(SubResourceModel),
        typeof(SubnetModel),
        typeof(SubnetPropertiesFormatModel),
        typeof(TagsObjectModel),
        typeof(TrafficAnalyticsConfigurationPropertiesModel),
        typeof(TrafficAnalyticsPropertiesModel),
        typeof(VirtualNetworkTapModel),
        typeof(VirtualNetworkTapPropertiesFormatModel),
    };
}
