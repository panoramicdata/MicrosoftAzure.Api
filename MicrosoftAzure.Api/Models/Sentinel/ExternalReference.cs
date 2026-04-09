using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the external reference.
/// </summary>
public class ExternalReference
{
	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	[JsonPropertyName("description")]
	public required string Description { get; set; }

	/// <summary>
	/// Gets or sets the external id.
	/// </summary>
	[JsonPropertyName("externalId")]
	public required string ExternalId { get; set; }

	/// <summary>
	/// Gets or sets the source name.
	/// </summary>
	[JsonPropertyName("sourceName")]
	public required string SourceName { get; set; }
}
