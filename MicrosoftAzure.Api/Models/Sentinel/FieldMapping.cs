using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the field mapping.
/// </summary>
public class FieldMapping
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	[JsonPropertyName("identifier")]
	public required string Identifier { get; set; }

	/// <summary>
	/// Gets or sets the column name.
	/// </summary>
	[JsonPropertyName("columnName")]
	public required string ColumnName { get; set; }
}
