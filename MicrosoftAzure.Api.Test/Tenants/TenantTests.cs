namespace MicrosoftAzure.Api.Test.Tenants;

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
}

