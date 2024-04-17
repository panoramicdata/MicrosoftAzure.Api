using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class Metric
{
	[JsonPropertyName("metricName")]
	public required string Name { get; set; }

	[JsonPropertyName("metricValue")]
	public required int Value { get; set; }
}
