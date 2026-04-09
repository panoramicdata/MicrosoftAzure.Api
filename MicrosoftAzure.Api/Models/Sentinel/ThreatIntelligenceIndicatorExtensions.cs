using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the threat intelligence indicator extensions.
/// </summary>
public class ThreatIntelligenceIndicatorExtensions
{
	/// <summary>
	/// Gets or sets the sentinel extension.
	/// </summary>
	[JsonPropertyName("sentinelext")]
	public SentinelExtension? SentinelExtension { get; set; }

	/// <summary>
	/// Gets or sets the indicator provider.
	/// </summary>
	[JsonPropertyName("indicatorProvider")]
	public string? IndicatorProvider { get; set; }
}
