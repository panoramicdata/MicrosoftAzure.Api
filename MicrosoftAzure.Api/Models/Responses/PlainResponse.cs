using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Responses;

/// <summary>
/// Represents the plain response.
/// </summary>
public class PlainResponse<TProperties>
{
	/// <summary>
	/// Gets or sets the values.
	/// </summary>
	[JsonPropertyName("value")]
	public required IReadOnlyCollection<TProperties> Values { get; set; }
}
