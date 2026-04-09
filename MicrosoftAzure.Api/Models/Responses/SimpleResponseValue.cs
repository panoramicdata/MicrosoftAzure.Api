using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Responses;

/// <summary>
/// Represents the simple response value.
/// </summary>
public class SimpleResponseValue<TProperties>
{
	/// <summary>
	/// Gets or sets the properties.
	/// </summary>
	[JsonPropertyName("properties")]
	public required TProperties Properties { get; set; }
}
