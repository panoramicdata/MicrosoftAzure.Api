using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class AlertRuleIncidentConfig
{
	[JsonPropertyName("createIncident")]
	public required bool CreateIncident { get; set; }

	[JsonPropertyName("groupingConfiguration")]
	public required AlertRuleGroupingConfig GroupingConfiguration { get; set; }
}
