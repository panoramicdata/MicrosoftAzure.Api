namespace MicrosoftAzure.Api.Test.ManagedTenants;

public class ManagedTenantTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetManagedTenantsAsync_Succeeds()
	{
		var response = await Client
			.ManagedTenants
			.GetAsync(CancellationToken);

		response.Should().NotBeNull();
		response.Values.Should().NotBeNullOrEmpty();
	}
}

