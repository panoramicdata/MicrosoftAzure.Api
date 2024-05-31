﻿namespace MicrosoftAzure.Api.Test.ManagedTenants;

public class ManagedTenantTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetManagedTenantsAsync_Succeeds()
	{
		var response = await Client
			.ManagedTenants
			.GetAsync(default)
			.ConfigureAwait(true);

		response.Should().NotBeNull();
		response.Values.Should().NotBeNullOrEmpty();
	}
}

