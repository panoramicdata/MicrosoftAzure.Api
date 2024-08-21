using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

public class ResourcePropertiesResponse
{
	[JsonPropertyName("properties")]
	public ResourceProperties Properties { get; set; }

	[JsonPropertyName("location")]
	public string Location { get; set; }

	[JsonPropertyName("tags")]
	public IReadOnlyDictionary<string, string>? Tags { get; set; }

	[JsonPropertyName("id")]
	public string Id { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("type")]
	public string Type { get; set; }

	[JsonPropertyName("etag")]
	public string Etag { get; set; }
}
