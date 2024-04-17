using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.SecurityInsights;

public class Column
{
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	[JsonPropertyName("type")]
	public required string Type { get; set; }
}
