using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.SecurityInsights;

public class Table
{
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	[JsonPropertyName("columns")]
	public required IList<Column> Columns { get; init; }

	[JsonPropertyName("rows")]
	public required IList<IList<object?>> Rows { get; init; }
}
