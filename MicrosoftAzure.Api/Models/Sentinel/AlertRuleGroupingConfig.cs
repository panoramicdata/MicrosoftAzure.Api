using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class AlertRuleGroupingConfig
{
	[JsonPropertyName("enabled")]
	public required bool Enabled { get; set; }

	[JsonPropertyName("reopenClosedIncident")]
	public required bool ReopenClosedIncident { get; set; }

	[JsonPropertyName("lookbackDuration")]
	public required string LookbackDuration { get; set; }

	[JsonPropertyName("matchingMethod")]
	public required string MatchingMethod { get; set; }

	[JsonPropertyName("groupByEntities")]
	public required IReadOnlyCollection<string> GroupByEntities { get; set; }

	[JsonPropertyName("groupByAlertDetails")]
	public required IReadOnlyCollection<object> GroupByAlertDetails { get; set; }

	[JsonPropertyName("groupByCustomDetails")]
	public required IReadOnlyCollection<object> GroupByCustomDetails { get; set; }
}
