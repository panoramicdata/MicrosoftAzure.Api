using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

public class Resource
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
	public IReadOnlyDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

	[JsonPropertyName("managedBy")]
	public string? ManagedBy { get; set; }

	[JsonPropertyName("systemData")]
	public SystemData? SystemData { get; set; }
}
