using Microsoft.Extensions.Logging.Abstractions;
using MicrosoftAzure.Api.Exceptions;
using MicrosoftAzure.Api.Test.Support;

namespace MicrosoftAzure.Api.Test.Client;

public class ClientAndOptionsTests
{
	[Theory]
	[InlineData("00000000-0000-0000-0000-000000000000", "22222222-2222-2222-2222-222222222222", "secret", "TenantId must be set.")]
	[InlineData("11111111-1111-1111-1111-111111111111", "00000000-0000-0000-0000-000000000000", "secret", "ClientId must be set.")]
	[InlineData("11111111-1111-1111-1111-111111111111", "22222222-2222-2222-2222-222222222222", "", "ClientSecret must be set.")]
	[InlineData("11111111-1111-1111-1111-111111111111", "22222222-2222-2222-2222-222222222222", "   ", "ClientSecret must be set.")]
	public void Validate_RejectsMissingValues(string tenantId, string clientId, string clientSecret, string expectedMessage)
	{
		var options = new MicrosoftAzureClientOptions
		{
			TenantId = new Guid(tenantId),
			ClientId = new Guid(clientId),
			ClientSecret = clientSecret,
		};

		options.Invoking(o => o.Validate()).Should().Throw<ConfigurationException>().WithMessage(expectedMessage);
	}

	[Fact]
	public void Validate_AcceptsCompleteOptions()
		=> TestOptions.Create().Invoking(o => o.Validate()).Should().NotThrow();

	[Fact]
	public void Logger_DefaultsToNullLogger()
		=> TestOptions.Create().Logger.Should().BeSameAs(NullLogger.Instance);

	[Fact]
	public void Constructor_WithNullOptions_Throws()
		=> FluentActions.Invoking(() => new MicrosoftAzureClient(null!)).Should().Throw<ArgumentNullException>();

	[Fact]
	public void Constructor_WithInvalidOptions_Throws()
	{
		var options = new MicrosoftAzureClientOptions
		{
			TenantId = Guid.Empty,
			ClientId = TestOptions.ClientId,
			ClientSecret = "secret",
		};

		FluentActions.Invoking(() => new MicrosoftAzureClient(options)).Should().Throw<ConfigurationException>();
	}

	[Fact]
	public void Constructor_CreatesEveryApiSurface()
	{
		using var client = new MicrosoftAzureClient(TestOptions.Create());

		client.LogAnalytics.Should().NotBeNull();
		client.ManagedTenants.Should().NotBeNull();
		client.ResourceGroups.Should().NotBeNull();
		client.Resources.Should().NotBeNull();
		client.Sentinel.Should().NotBeNull();
		client.Subscriptions.Should().NotBeNull();
		client.Tenants.Should().NotBeNull();
	}

	[Fact]
	public void Dispose_CanBeCalledRepeatedly()
	{
		var client = new MicrosoftAzureClient(TestOptions.Create());

		client.Dispose();

		client.Invoking(c => c.Dispose()).Should().NotThrow();
	}
}
