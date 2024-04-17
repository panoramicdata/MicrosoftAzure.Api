using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class ThreatIntelligenceIndicator
{
	[JsonPropertyName("confidence")]
	public required int Confidence { get; set; }

	[JsonPropertyName("created")]
	public required DateTime Created { get; set; }

	[JsonPropertyName("createdByRef")]
	public required string CreatedByRef { get; set; }

	[JsonPropertyName("extensions")]
	public required ThreatIntelligenceIndicatorExtensions Extensions { get; set; }

	[JsonPropertyName("externalId")]
	public required string ExternalId { get; set; }

	[JsonPropertyName("externalLastUpdatedTimeUtc")]
	public required DateTime ExternalLastUpdatedTimeUtc { get; set; }

	[JsonPropertyName("externalReferences")]
	public required IReadOnlyCollection<ExternalReference> ExternalReferences { get; set; }

	[JsonPropertyName("labels")]
	public required IReadOnlyCollection<string> Labels { get; set; }

	[JsonPropertyName("lastUpdatedTimeUtc")]
	public required DateTime LastUpdatedTimeUtc { get; set; }

	[JsonPropertyName("objectMarkingRefs")]
	public required IReadOnlyCollection<string> ObjectMarkingRefs { get; set; }

	[JsonPropertyName("source")]
	public required string Source { get; set; }

	[JsonPropertyName("threatIntelligenceTags")]
	public required IReadOnlyCollection<string> ThreatIntelligenceTags { get; set; }

	[JsonPropertyName("displayName")]
	public required string DisplayName { get; set; }

	[JsonPropertyName("description")]
	public required string Description { get; set; }

	[JsonPropertyName("threatTypes")]
	public required IReadOnlyCollection<string> ThreatTypes { get; set; }

	[JsonPropertyName("parsedPattern")]
	public required IReadOnlyCollection<ParsedPattern> ParsedPattern { get; set; }

	[JsonPropertyName("pattern")]
	public required string Pattern { get; set; }

	[JsonPropertyName("patternType")]
	public required string PatternType { get; set; }

	[JsonPropertyName("validFrom")]
	public required DateTime ValidFrom { get; set; }

	[JsonPropertyName("validUntil")]
	public required DateTime ValidUntil { get; set; }
}
