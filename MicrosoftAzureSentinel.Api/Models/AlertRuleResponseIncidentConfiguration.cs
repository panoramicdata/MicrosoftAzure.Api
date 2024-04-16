using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class AlertRuleResponseIncidentConfiguration
{
	[JsonPropertyName("createIncident")]
	public required bool CreateIncident { get; set; }

	[JsonPropertyName("groupingConfiguration")]
	public required AlertRuleResponseGroupingConfiguration GroupingConfiguration { get; set; }
}
