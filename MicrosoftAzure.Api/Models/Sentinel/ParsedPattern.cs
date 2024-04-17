using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class ParsedPattern
{
	[JsonPropertyName("patternTypeKey")]
	public required string PatternTypeKey { get; set; }

	[JsonPropertyName("patternTypeValues")]
	public required IReadOnlyCollection<PatternTypeValue> PatternTypeValues { get; set; }
}
