using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class Settings
{
	[JsonPropertyName("aggregationKind")]
	public required string AggregationKind { get; set; }
}
