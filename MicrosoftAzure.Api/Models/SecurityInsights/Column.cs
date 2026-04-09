using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.SecurityInsights;

/// <summary>
/// Represents the column.
/// </summary>
public class Column
{
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
}
