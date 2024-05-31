using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Responses;

public class SimpleResponseValue<TProperties>
{
	[JsonPropertyName("properties")]
	public required TProperties Properties { get; set; }
}