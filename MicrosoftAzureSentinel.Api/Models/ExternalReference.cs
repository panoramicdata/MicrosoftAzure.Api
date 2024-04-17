using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class ExternalReference
{
	[JsonPropertyName("description")]
	public required string Description { get; set; }

	[JsonPropertyName("externalId")]
	public required string ExternalId { get; set; }

	[JsonPropertyName("sourceName")]
	public required string SourceName { get; set; }
}
