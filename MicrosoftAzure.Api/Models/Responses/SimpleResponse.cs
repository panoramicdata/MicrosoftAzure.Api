using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Responses;

/// <summary>
/// Represents the simple response.
/// </summary>
public class SimpleResponse<TProperties>
{
	/// <summary>
	/// Gets or sets the values.
	/// </summary>
	[JsonPropertyName("value")]
	public required IReadOnlyCollection<SimpleResponseValue<TProperties>> Values { get; set; }
}
