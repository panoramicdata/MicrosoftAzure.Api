using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class Settings
{
	[JsonPropertyName("aggregationKind")]
	public required string AggregationKind { get; set; }
}
