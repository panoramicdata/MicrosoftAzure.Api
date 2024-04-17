using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class StateObject
{
	[JsonPropertyName("state")]
	public required string State { get; set; }
}

