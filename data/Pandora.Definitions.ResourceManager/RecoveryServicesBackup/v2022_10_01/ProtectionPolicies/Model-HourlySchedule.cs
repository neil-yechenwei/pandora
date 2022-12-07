using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Pandora.Definitions.Attributes;
using Pandora.Definitions.Attributes.Validation;
using Pandora.Definitions.CustomTypes;


// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See NOTICE.txt in the project root for license information.


namespace Pandora.Definitions.ResourceManager.RecoveryServicesBackup.v2022_10_01.ProtectionPolicies;


internal class HourlyScheduleModel
{
    [JsonPropertyName("interval")]
    public int? Interval { get; set; }

    [JsonPropertyName("scheduleWindowDuration")]
    public int? ScheduleWindowDuration { get; set; }

    [DateFormat(DateFormatAttribute.DateFormat.RFC3339)]
    [JsonPropertyName("scheduleWindowStartTime")]
    public DateTime? ScheduleWindowStartTime { get; set; }
}
