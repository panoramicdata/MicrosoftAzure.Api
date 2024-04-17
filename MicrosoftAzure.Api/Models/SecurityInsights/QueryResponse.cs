using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.SecurityInsights;

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
					var column = columns[i];
					row[i] = value.ValueKind switch
					{
						JsonValueKind.String => column.Type switch
						{
							"datetime" => value.GetDateTime(),
							"datetimeoffset" => value.GetDateTimeOffset(),
							"guid" => value.GetGuid(),
							"string" => value.GetString(),
							_ => value.GetString()
						},
						JsonValueKind.Number => column.Type switch
						{
							"long" => value.GetInt64(),
							"int" => value.GetInt32(),
							"short" => value.GetInt16(),
							"byte" => value.GetByte(),
							"float" => value.GetSingle(),
							_ => value.GetDouble()
						},
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