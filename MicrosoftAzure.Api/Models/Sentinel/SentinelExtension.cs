using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class SentinelExtension
{
	[JsonPropertyName("severity")]
	public required object Severity { get; set; }
}
