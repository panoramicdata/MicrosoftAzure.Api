using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class PatternTypeValue
{
	[JsonPropertyName("valueType")]
	public required string ValueType { get; set; }

	[JsonPropertyName("value")]
	public required string Value { get; set; }
}
