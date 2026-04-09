using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the pattern type value.
/// </summary>
public class PatternTypeValue
{
	/// <summary>
	/// Gets or sets the value type.
	/// </summary>
	[JsonPropertyName("valueType")]
	public required string ValueType { get; set; }

	/// <summary>
	/// Gets or sets the value.
	/// </summary>
	[JsonPropertyName("value")]
	public required string Value { get; set; }
}
