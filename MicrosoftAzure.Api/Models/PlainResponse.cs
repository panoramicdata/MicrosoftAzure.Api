using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class PlainResponse<TProperties>
{
	[JsonPropertyName("value")]
	public required IReadOnlyCollection<TProperties> Values { get; set; }
}
