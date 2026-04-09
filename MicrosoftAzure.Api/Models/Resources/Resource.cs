using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

/// <summary>
/// Represents the resource.
/// </summary>
public class Resource
{
	/// <summary>
	/// Gets or sets the id.
	/// </summary>
	[JsonPropertyName("id")]
	public required string Id { get; set; }

	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	/// <summary>
	/// Gets or sets the type.
	/// </summary>
	[JsonPropertyName("type")]
	public required string Type { get; set; }

	/// <summary>
	/// Gets or sets the location.
	/// </summary>
	[JsonPropertyName("location")]
	public required string Location { get; set; }

	/// <summary>
	/// Gets or sets the tags.
	/// </summary>
	[JsonPropertyName("tags")]
	public IReadOnlyDictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();

	/// <summary>
	/// Gets or sets the properties.
	/// </summary>
	[JsonPropertyName("properties")]
	public IReadOnlyDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

	/// <summary>
	/// Gets or sets the managed by.
	/// </summary>
	[JsonPropertyName("managedBy")]
	public string? ManagedBy { get; set; }

	/// <summary>
	/// Gets or sets the system data.
	/// </summary>
	[JsonPropertyName("systemData")]
	public SystemData? SystemData { get; set; }
}
