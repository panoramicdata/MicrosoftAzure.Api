namespace MicrosoftAzure.Api.Test.ResourceGroups;

public class ResourceGroupTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetResourceGroupsAsync_Succeeds()
	{
		foreach (var subscriptionId in await GetSubscriptionIdsAsync(CancellationToken))
		{
			var response = await Client
				.ResourceGroups
				.GetAsync(
					subscriptionId,
					CancellationToken);

			response.Should().NotBeNull();
			response.Values.Should().NotBeNullOrEmpty();
		}
	}
}