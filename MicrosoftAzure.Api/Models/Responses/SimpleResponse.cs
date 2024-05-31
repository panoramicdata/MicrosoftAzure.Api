using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Responses;

public class SimpleResponse<TProperties>
{
	[JsonPropertyName("value")]
	public required IReadOnlyCollection<SimpleResponseValue<TProperties>> Values { get; set; }
}