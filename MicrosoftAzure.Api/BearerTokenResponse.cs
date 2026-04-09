using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api;

/// <summary>
/// Represents the bearer token response.
/// </summary>
public class BearerTokenResponse
{
	/// <summary>
	/// Gets or sets the token type.
	/// </summary>
	[JsonPropertyName("token_type")]
	public required string TokenType { get; set; }

	/// <summary>
	/// Gets or sets the resource.
	/// </summary>
	[JsonPropertyName("resource")]
	public required string Resource { get; set; }

	/// <summary>
	/// Gets or sets the expires in.
	/// </summary>
	[JsonPropertyName("expires_in")]
	public required string ExpiresIn { get; set; }

	/// <summary>
	/// Gets or sets the expires on.
	/// </summary>
	[JsonPropertyName("expires_on")]
	public required string ExpiresOn { get; set; }

	/// <summary>
	/// Gets or sets the not before.
	/// </summary>
	[JsonPropertyName("not_before")]
	public required string NotBefore { get; set; }

	/// <summary>
	/// Gets or sets the ext expires in.
	/// </summary>
	[JsonPropertyName("ext_expires_in")]
	public required string ExtExpiresIn { get; set; }

	/// <summary>
	/// Gets or sets the access token.
	/// </summary>
	[JsonPropertyName("access_token")]
	public required string AccessToken { get; set; }
}
