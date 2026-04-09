using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the alert rule grouping config.
/// </summary>
public class AlertRuleGroupingConfig
{
	/// <summary>
	/// Gets or sets a value indicating whether d is enabled.
	/// </summary>
	[JsonPropertyName("enabled")]
	public required bool Enabled { get; set; }

	/// <summary>
	/// Gets or sets the reopen closed incident.
	/// </summary>
	[JsonPropertyName("reopenClosedIncident")]
	public required bool ReopenClosedIncident { get; set; }

	/// <summary>
	/// Gets or sets the lookback duration.
	/// </summary>
	[JsonPropertyName("lookbackDuration")]
	public required string LookbackDuration { get; set; }

	/// <summary>
	/// Gets or sets the matching method.
	/// </summary>
	[JsonPropertyName("matchingMethod")]
	public required string MatchingMethod { get; set; }

	/// <summary>
	/// Gets or sets the group by entities.
	/// </summary>
	[JsonPropertyName("groupByEntities")]
	public required IReadOnlyCollection<string> GroupByEntities { get; set; }

	/// <summary>
	/// Gets or sets the group by alert details.
	/// </summary>
	[JsonPropertyName("groupByAlertDetails")]
	public required IReadOnlyCollection<object> GroupByAlertDetails { get; set; }

	/// <summary>
	/// Gets or sets the group by custom details.
	/// </summary>
	[JsonPropertyName("groupByCustomDetails")]
	public required IReadOnlyCollection<object> GroupByCustomDetails { get; set; }
}
