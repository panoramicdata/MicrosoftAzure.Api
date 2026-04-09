using System.Diagnostics;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Responses;

/// <summary>
/// Represents the error response error.
/// </summary>
[DebuggerDisplay("{Code}: {Message}")]
public class ErrorResponseError
{
	/// <summary>
	/// Gets or sets the code.
	/// </summary>
	[JsonPropertyName("code")]
	public required string Code { get; set; }

	/// <summary>
	/// Gets or sets the message.
	/// </summary>
	[JsonPropertyName("message")]
	public required string Message { get; set; }
}
