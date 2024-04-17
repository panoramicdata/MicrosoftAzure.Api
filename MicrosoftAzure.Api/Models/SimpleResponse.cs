using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class SimpleResponse<TProperties>
{
	[JsonPropertyName("value")]
	public required IReadOnlyCollection<SimpleResponseValue<TProperties>> Values { get; set; }
}