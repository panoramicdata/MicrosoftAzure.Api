using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the threat intelligence indicator.
/// </summary>
public class ThreatIntelligenceIndicator
{
	/// <summary>
	/// Gets or sets the confidence.
	/// </summary>
	[JsonPropertyName("confidence")]
	public required int Confidence { get; set; }

	/// <summary>
	/// Gets or sets the created.
	/// </summary>
	[JsonPropertyName("created")]
	public required DateTime Created { get; set; }

	/// <summary>
	/// Gets or sets the created by ref.
	/// </summary>
	[JsonPropertyName("createdByRef")]
	public required string CreatedByRef { get; set; }

	/// <summary>
	/// Gets or sets the extensions.
	/// </summary>
	[JsonPropertyName("extensions")]
	public required ThreatIntelligenceIndicatorExtensions Extensions { get; set; }

	/// <summary>
	/// Gets or sets the external id.
	/// </summary>
	[JsonPropertyName("externalId")]
	public required string ExternalId { get; set; }

	/// <summary>
	/// Gets or sets the external last updated time utc.
	/// </summary>
	[JsonPropertyName("externalLastUpdatedTimeUtc")]
	public DateTime? ExternalLastUpdatedTimeUtc { get; set; }

	/// <summary>
	/// Gets or sets the external references.
	/// </summary>
	[JsonPropertyName("externalReferences")]
	public required IReadOnlyCollection<ExternalReference> ExternalReferences { get; set; }

	/// <summary>
	/// Gets or sets the labels.
	/// </summary>
	[JsonPropertyName("labels")]
	public required IReadOnlyCollection<string> Labels { get; set; }

	/// <summary>
	/// Gets or sets the last updated time utc.
	/// </summary>
	[JsonPropertyName("lastUpdatedTimeUtc")]
	public required DateTime LastUpdatedTimeUtc { get; set; }

	/// <summary>
	/// Gets or sets the object marking refs.
	/// </summary>
	[JsonPropertyName("objectMarkingRefs")]
	public IReadOnlyCollection<string>? ObjectMarkingRefs { get; set; }

	/// <summary>
	/// Gets or sets the source.
	/// </summary>
	[JsonPropertyName("source")]
	public required string Source { get; set; }

	/// <summary>
	/// Gets or sets the threat intelligence tags.
	/// </summary>
	[JsonPropertyName("threatIntelligenceTags")]
	public required IReadOnlyCollection<string> ThreatIntelligenceTags { get; set; }

	/// <summary>
	/// Gets or sets the display name.
	/// </summary>
	[JsonPropertyName("displayName")]
	public required string DisplayName { get; set; }

	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	[JsonPropertyName("description")]
	public required string Description { get; set; }

	/// <summary>
	/// Gets or sets the threat types.
	/// </summary>
	[JsonPropertyName("threatTypes")]
	public required IReadOnlyCollection<string> ThreatTypes { get; set; }

	/// <summary>
	/// Gets or sets the parsed pattern.
	/// </summary>
	[JsonPropertyName("parsedPattern")]
	public required IReadOnlyCollection<ParsedPattern> ParsedPattern { get; set; }

	/// <summary>
	/// Gets or sets the pattern.
	/// </summary>
	[JsonPropertyName("pattern")]
	public required string Pattern { get; set; }

	/// <summary>
	/// Gets or sets the pattern type.
	/// </summary>
	[JsonPropertyName("patternType")]
	public required string PatternType { get; set; }

	/// <summary>
	/// Gets or sets the valid from.
	/// </summary>
	[JsonPropertyName("validFrom")]
	public required DateTime ValidFrom { get; set; }

	/// <summary>
	/// Gets or sets the valid until.
	/// </summary>
	[JsonPropertyName("validUntil")]
	public required DateTime ValidUntil { get; set; }
}
