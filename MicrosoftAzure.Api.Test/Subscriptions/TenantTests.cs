namespace MicrosoftAzure.Api.Test.Subscriptions;

public class TenantTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{

	[Fact]
	public async Task GetTenantsAsync_Succeeds()
	{
		var response = await Client
			.Tenants
			.GetTenantsAsync(default)
			.ConfigureAwait(true);

		response.Should().NotBeNull();
		response.Values.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public async Task GetManagedTenantsAsync_Succeeds()
	{
		var response = await Client
			.Tenants
			.GetManagedTenantsAsync(default)
			.ConfigureAwait(true);

		response.Should().NotBeNull();
		response.Values.Should().NotBeNullOrEmpty();
	}
}

