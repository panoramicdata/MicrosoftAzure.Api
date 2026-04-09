using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the parsed pattern.
/// </summary>
public class ParsedPattern
{
	/// <summary>
	/// Gets or sets the pattern type key.
	/// </summary>
	[JsonPropertyName("patternTypeKey")]
	public required string PatternTypeKey { get; set; }

	/// <summary>
	/// Gets or sets the pattern type values.
	/// </summary>
	[JsonPropertyName("patternTypeValues")]
	public required IReadOnlyCollection<PatternTypeValue> PatternTypeValues { get; set; }
}
