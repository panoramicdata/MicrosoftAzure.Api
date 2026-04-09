using System.Diagnostics;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Responses;

/// <summary>
/// Represents the error response.
/// </summary>
[DebuggerDisplay("Error = {Error}")]
public class ErrorResponse
{
	/// <summary>
	/// Gets or sets the error.
	/// </summary>
	[JsonPropertyName("error")]
	public required ErrorResponseError Error { get; set; }
}
