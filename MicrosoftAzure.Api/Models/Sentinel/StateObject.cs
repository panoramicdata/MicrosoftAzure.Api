using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the state object.
/// </summary>
public class StateObject
{
	/// <summary>
	/// Gets or sets the state.
	/// </summary>
	[JsonPropertyName("state")]
	public required string State { get; set; }
}

