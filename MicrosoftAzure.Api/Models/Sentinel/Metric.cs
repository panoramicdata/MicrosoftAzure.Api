using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the metric.
/// </summary>
public class Metric
{
	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	[JsonPropertyName("metricName")]
	public required string Name { get; set; }

	/// <summary>
	/// Gets or sets the value.
	/// </summary>
	[JsonPropertyName("metricValue")]
	public required int Value { get; set; }
}
