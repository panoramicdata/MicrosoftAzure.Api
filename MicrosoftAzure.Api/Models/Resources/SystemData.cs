using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

/// <summary>
/// Represents the system data.
/// </summary>
public class SystemData
{
	/// <summary>
	/// Gets or sets the created by.
	/// </summary>
	[JsonPropertyName("createdBy")]
	public string? CreatedBy { get; set; }

	/// <summary>
	/// Gets or sets the created by type.
	/// </summary>
	[JsonPropertyName("createdByType")]
	public string? CreatedByType { get; set; }

	/// <summary>
	/// Gets or sets the created at.
	/// </summary>
	[JsonPropertyName("createdAt")]
	public DateTime? CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the last modified by.
	/// </summary>
	[JsonPropertyName("lastModifiedBy")]
	public string? LastModifiedBy { get; set; }

	/// <summary>
	/// Gets or sets the last modified by type.
	/// </summary>
	[JsonPropertyName("lastModifiedByType")]
	public string? LastModifiedByType { get; set; }

	/// <summary>
	/// Gets or sets the last modified at.
	/// </summary>
	[JsonPropertyName("lastModifiedAt")]
	public DateTime? LastModifiedAt { get; set; }
}
