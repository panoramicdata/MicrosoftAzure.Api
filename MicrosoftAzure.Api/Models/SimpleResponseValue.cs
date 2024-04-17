using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models;

public class SimpleResponseValue<TProperties>
{
	[JsonPropertyName("properties")]
	public required TProperties Properties { get; set; }
}