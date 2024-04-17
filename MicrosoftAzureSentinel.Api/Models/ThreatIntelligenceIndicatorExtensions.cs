using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class ThreatIntelligenceIndicatorExtensions
{
	[JsonPropertyName("sentinelext")]
	public SentinelExtension? SentinelExtension { get; set; }

	[JsonPropertyName("indicatorProvider")]
	public required string IndicatorProvider { get; set; }
}
