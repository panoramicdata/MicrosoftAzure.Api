using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class DataConnectionsResponseValue
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
	public required string Kind { get; set; }

	[JsonPropertyName("properties")]
	public required DataConnectionsResponseProperties Properties { get; set; }
}

