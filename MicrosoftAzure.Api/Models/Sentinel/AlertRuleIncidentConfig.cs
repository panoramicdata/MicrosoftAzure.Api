using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the alert rule incident config.
/// </summary>
public class AlertRuleIncidentConfig
{
	/// <summary>
	/// Gets or sets the create incident.
	/// </summary>
	[JsonPropertyName("createIncident")]
	public required bool CreateIncident { get; set; }

	/// <summary>
	/// Gets or sets the grouping configuration.
	/// </summary>
	[JsonPropertyName("groupingConfiguration")]
	public required AlertRuleGroupingConfig GroupingConfiguration { get; set; }
}
