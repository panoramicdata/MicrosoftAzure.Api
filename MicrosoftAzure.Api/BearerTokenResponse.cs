using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api;

public class BearerTokenResponse
{
	[JsonPropertyName("token_type")]
	public required string TokenType { get; set; }

	[JsonPropertyName("resource")]
	public required string Resource { get; set; }

	[JsonPropertyName("expires_in")]
	public required string ExpiresIn { get; set; }

	[JsonPropertyName("expires_on")]
	public required string ExpiresOn { get; set; }

	[JsonPropertyName("not_before")]
	public required string NotBefore { get; set; }

	[JsonPropertyName("ext_expires_in")]
	public required string ExtExpiresIn { get; set; }

	[JsonPropertyName("access_token")]
	public required string AccessToken { get; set; }
}
