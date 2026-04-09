using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the settings.
/// </summary>
public class Settings
{
	/// <summary>
	/// Gets or sets the aggregation kind.
	/// </summary>
	[JsonPropertyName("aggregationKind")]
	public required string AggregationKind { get; set; }
}
