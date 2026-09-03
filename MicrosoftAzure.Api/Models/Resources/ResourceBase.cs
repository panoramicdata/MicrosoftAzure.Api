using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

/// <summary>
/// The envelope that every Azure Resource Manager resource shares, whatever its type.
/// Type-specific payloads live on the derived classes, because their shapes differ.
/// </summary>
public abstract class ResourceBase
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
	/// Gets or sets the managed by.
	/// </summary>
	[JsonPropertyName("managedBy")]
	public string? ManagedBy { get; set; }
}
