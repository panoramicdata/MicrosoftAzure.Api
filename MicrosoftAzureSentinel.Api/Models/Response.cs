using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class Response<TProperties>
{
	[JsonPropertyName("value")]
	public required IReadOnlyCollection<ResponseValue<TProperties>> Values { get; set; }
}
