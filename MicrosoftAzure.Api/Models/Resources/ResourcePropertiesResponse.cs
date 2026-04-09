using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

/// <summary>
/// Represents the resource properties response.
/// </summary>
public class ResourcePropertiesResponse
{
	/// <summary>
	/// Gets or sets the properties.
	/// </summary>
	[JsonPropertyName("properties")]
	public required ResourceProperties Properties { get; set; }

	/// <summary>
	/// Gets or sets the location.
	/// </summary>
	[JsonPropertyName("location")]
	public required string Location { get; set; }

	/// <summary>
	/// Gets or sets the tags.
	/// </summary>
	[JsonPropertyName("tags")]
	public IReadOnlyDictionary<string, string>? Tags { get; set; }

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
	/// Gets or sets the etag.
	/// </summary>
	[JsonPropertyName("etag")]
	public required string Etag { get; set; }
}
