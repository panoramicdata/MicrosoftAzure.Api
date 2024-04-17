using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class FieldMapping
{
	[JsonPropertyName("identifier")]
	public required string Identifier { get; set; }

	[JsonPropertyName("columnName")]
	public required string ColumnName { get; set; }
}