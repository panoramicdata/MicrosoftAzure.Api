namespace MicrosoftAzure.Api.Test.ResourceGroups;

public class ResourceGroupTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetResourceGroupsAsync_Succeeds()
	{
		var response = await Client
			.ResourceGroups
			.GetAsync(
				TestConfig.SubscriptionId,
				default)
			.ConfigureAwait(true);

		response.Should().NotBeNull();
		response.Values.Should().NotBeNullOrEmpty();
	}
}