using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;
public class ResourceGroup
{
	[JsonPropertyName("id")]
	public required string Id { get; set; }

	[JsonPropertyName("name")]
	public required string Name { get; set; }

	[JsonPropertyName("type")]
	public required string Type { get; set; }

	[JsonPropertyName("location")]
	public required string Location { get; set; }

	[JsonPropertyName("tags")]
	public IReadOnlyDictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();

	[JsonPropertyName("properties")]
	public required ResourceGroupProperties Properties { get; set; }

	[JsonPropertyName("managedBy")]
	public string? ManagedBy { get; set; }
}