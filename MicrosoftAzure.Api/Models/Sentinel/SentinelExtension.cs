using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the sentinel extension.
/// </summary>
public class SentinelExtension
{
	/// <summary>
	/// Gets or sets the severity.
	/// </summary>
	[JsonPropertyName("severity")]
	public required object Severity { get; set; }
}
