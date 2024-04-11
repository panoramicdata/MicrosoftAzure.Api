
namespace MicrosoftAzureSentinel.Api;

internal class BearerToken
{
	public required string AccessToken { get; set; }

	public required DateTime ExpiryDateTimeUtc { get; set; }

	public bool IsExpired => ExpiryDateTimeUtc < DateTime.UtcNow;
}