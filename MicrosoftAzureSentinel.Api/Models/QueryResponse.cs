using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class QueryResponse
{
	[JsonPropertyName("tables")]
	public required ICollection<Table> Tables { get; set; }
}