using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the threat intelligence metric.
/// </summary>
public class ThreatIntelligenceMetric
{
	/// <summary>
	/// Gets or sets the threat type metrics.
	/// </summary>
	[JsonPropertyName("threatTypeMetrics")]
	public required IReadOnlyCollection<Metric> ThreatTypeMetrics { get; set; }

	/// <summary>
	/// Gets or sets the pattern type metrics.
	/// </summary>
	[JsonPropertyName("patternTypeMetrics")]
	public required IReadOnlyCollection<Metric> PatternTypeMetrics { get; set; }

	/// <summary>
	/// Gets or sets the source metrics.
	/// </summary>
	[JsonPropertyName("sourceMetrics")]
	public required IReadOnlyCollection<Metric> SourceMetrics { get; set; }
}
