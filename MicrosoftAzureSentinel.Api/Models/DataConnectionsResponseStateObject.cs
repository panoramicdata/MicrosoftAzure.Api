using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class DataConnectionsResponseStateObject
{
	[JsonPropertyName("state")]
	public required string State { get; set; }
}

