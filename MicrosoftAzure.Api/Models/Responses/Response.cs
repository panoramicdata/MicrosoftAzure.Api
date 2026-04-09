using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Responses;

/// <summary>
/// Represents the response.
/// </summary>
public class Response<TProperties>
{
	/// <summary>
	/// Gets or sets the values.
	/// </summary>
	[JsonPropertyName("value")]
	public required IReadOnlyCollection<ResponseValue<TProperties>> Values { get; set; }
}
