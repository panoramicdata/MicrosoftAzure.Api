using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class FieldMapping
{
	[JsonPropertyName("identifier")]
	public required string Identifier { get; set; }

	[JsonPropertyName("columnName")]
	public required string ColumnName { get; set; }
}