using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class SentinelExtension
{
	[JsonPropertyName("severity")]
	public required object Severity { get; set; }
}
