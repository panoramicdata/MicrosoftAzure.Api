using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.SecurityInsights;

/// <summary>
/// Represents the table.
/// </summary>
public class Table
{
	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	/// <summary>
	/// Gets or sets the columns.
	/// </summary>
	[JsonPropertyName("columns")]
	public required IList<Column> Columns { get; init; }

	/// <summary>
	/// Gets or sets the rows.
	/// </summary>
	[JsonPropertyName("rows")]
	public required IList<IList<object?>> Rows { get; init; }
}
