using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Responses;

/// <summary>
/// Represents the response value.
/// </summary>
public class ResponseValue<TProperties> : SimpleResponseValue<TProperties>
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
	/// Gets or sets the etag.
	/// </summary>
	[JsonPropertyName("etag")]
	public required string Etag { get; set; }

	/// <summary>
	/// Gets or sets the type.
	/// </summary>
	[JsonPropertyName("type")]
	public required string Type { get; set; }

	/// <summary>
	/// Gets or sets the kind.
	/// </summary>
	[JsonPropertyName("kind")]
	public string? Kind { get; set; }

	/// <summary>
	/// Gets or sets the next link.
	/// </summary>
	[JsonPropertyName("nextLink")]
	public string? NextLink { get; set; }

}

