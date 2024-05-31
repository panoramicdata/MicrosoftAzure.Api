using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Responses;

public class Response<TProperties>
{
	[JsonPropertyName("value")]
	public required IReadOnlyCollection<ResponseValue<TProperties>> Values { get; set; }
}