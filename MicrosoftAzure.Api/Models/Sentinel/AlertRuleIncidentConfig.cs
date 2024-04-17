using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class AlertRuleIncidentConfig
{
	[JsonPropertyName("createIncident")]
	public required bool CreateIncident { get; set; }

	[JsonPropertyName("groupingConfiguration")]
	public required AlertRuleGroupingConfig GroupingConfiguration { get; set; }
}
