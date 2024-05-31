using System.Diagnostics;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Responses;

[DebuggerDisplay("Error = {Error}")]
public class ErrorResponse
{
	[JsonPropertyName("error")]
	public required ErrorResponseError Error { get; set; }
}
