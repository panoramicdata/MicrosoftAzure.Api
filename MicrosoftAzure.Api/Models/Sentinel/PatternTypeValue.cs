using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class PatternTypeValue
{
	[JsonPropertyName("valueType")]
	public required string ValueType { get; set; }

	[JsonPropertyName("value")]
	public required string Value { get; set; }
}
