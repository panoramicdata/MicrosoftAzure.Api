using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class QueryResponse
{
	[JsonPropertyName("tables")]
	public required IList<Table> Tables { get; init; }

	internal void Sanitize()
	{
		foreach (var table in Tables)
		{
			var columns = table.Columns;

			foreach (var row in table.Rows)
			{
				for (var i = 0; i < row.Count; i++)
				{
					if (row[i] is null)
					{
						continue;
					}

					var value = row[i] is JsonElement element ? element : throw new InvalidOperationException("Deserialization failure.");
					row[i] = value.ValueKind switch
					{
						JsonValueKind.String => value.GetString(),
						JsonValueKind.Number => value.GetDouble(),
						JsonValueKind.True or JsonValueKind.False => value.GetBoolean(),
						JsonValueKind.Object => value,
						JsonValueKind.Array => value,
						JsonValueKind.Null => null,
						JsonValueKind.Undefined => null,
						_ => throw new NotSupportedException("Unknown value kind {value.ValueKind}."),
					};
				}
			}
		}
	}
}