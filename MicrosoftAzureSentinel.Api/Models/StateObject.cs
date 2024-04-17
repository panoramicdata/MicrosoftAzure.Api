using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class StateObject
{
	[JsonPropertyName("state")]
	public required string State { get; set; }
}

