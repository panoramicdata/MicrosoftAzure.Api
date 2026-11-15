using Microsoft.Extensions.Logging.Abstractions;

namespace MicrosoftAzure.Api.Test.Support;

internal static class TestOptions
{
	public static readonly Guid TenantId = new("11111111-1111-1111-1111-111111111111");

	public static readonly Guid ClientId = new("22222222-2222-2222-2222-222222222222");

	public static MicrosoftAzureClientOptions Create(ILogger? logger = null)
		=> new()
		{
			TenantId = TenantId,
			ClientId = ClientId,
			ClientSecret = "test-client-secret",
			Logger = logger ?? NullLogger.Instance,
		};
}
