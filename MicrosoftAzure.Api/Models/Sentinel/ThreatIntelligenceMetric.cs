using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class ThreatIntelligenceMetric
{
	[JsonPropertyName("threatTypeMetrics")]
	public required IReadOnlyCollection<Metric> ThreatTypeMetrics { get; set; }

	[JsonPropertyName("patternTypeMetrics")]
	public required IReadOnlyCollection<Metric> PatternTypeMetrics { get; set; }

	[JsonPropertyName("sourceMetrics")]
	public required IReadOnlyCollection<Metric> SourceMetrics { get; set; }
}