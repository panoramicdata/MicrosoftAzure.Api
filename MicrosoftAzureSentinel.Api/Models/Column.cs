using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class Column
{
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	[JsonPropertyName("type")]
	public required string Type { get; set; }
}
