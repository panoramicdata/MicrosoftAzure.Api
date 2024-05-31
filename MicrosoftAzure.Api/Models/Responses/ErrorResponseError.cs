using System.Diagnostics;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Responses;

[DebuggerDisplay("{Code}: {Message}")]
public class ErrorResponseError
{
	[JsonPropertyName("code")]
	public required string Code { get; set; }

	[JsonPropertyName("message")]
	public required string Message { get; set; }
}
