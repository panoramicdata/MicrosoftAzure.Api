using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class AlertRuleResponseEventGroupingSettings
{
	[JsonPropertyName("aggregationKind")]
	public required string AggregationKind { get; set; }
}
