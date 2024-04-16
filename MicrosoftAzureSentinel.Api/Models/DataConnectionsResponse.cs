using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class DataConnectionsResponse
{
	[JsonPropertyName("value")]
	public required IReadOnlyCollection<DataConnectionsResponseValue> Value { get; set; }
}