using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models;

public class SimpleResponse<TProperties>
{
	[JsonPropertyName("value")]
	public required IReadOnlyCollection<SimpleResponseValue<TProperties>> Values { get; set; }
}