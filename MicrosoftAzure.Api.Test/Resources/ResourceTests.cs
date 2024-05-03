namespace MicrosoftAzure.Api.Test.Resources;

public class ResourceTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetResourceGroupsAsync_Succeeds()
	{
		var response = await Client
			.Resources
			.GetResourceGroupsAsync(
				TestConfig.SubscriptionId,
				default)
			.ConfigureAwait(true);

		response.Should().NotBeNull();
		response.Values.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public async Task GetResourcesAsync_Succeeds()
	{
		var response = await Client
			.Resources
			.GetResourcesAsync(
				TestConfig.SubscriptionId,
				default)
			.ConfigureAwait(true);

		response.Should().NotBeNull();
		response.Values.Should().NotBeNullOrEmpty();
	}
}

