using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class ResponseValue<TProperties> : SimpleResponseValue<TProperties>
{
	[JsonPropertyName("id")]
	public required string Id { get; set; }

	[JsonPropertyName("name")]
	public required string Name { get; set; }

	[JsonPropertyName("etag")]
	public required string Etag { get; set; }

	[JsonPropertyName("type")]
	public required string Type { get; set; }

	[JsonPropertyName("kind")]
	public string? Kind { get; set; }

	[JsonPropertyName("nextLink")]
	public string? NextLink { get; set; }

}

