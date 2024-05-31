namespace MicrosoftAzure.Api.Test.Resources;

public class ResourceTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetResourcesAsync_Succeeds()
	{
		foreach (var subscriptionId in await GetSubscriptionIdsAsync(default).ConfigureAwait(true))
		{
			var response = await Client
				.Resources
				.GetAsync(
					subscriptionId,
					default)
				.ConfigureAwait(true);

			response.Should().NotBeNull();
			response.Values.Should().NotBeNullOrEmpty();
		}
	}

	[Fact]
	public async Task GetResourcesAsync_WithFilter_Succeeds()
	{
		foreach (var subscriptionId in await GetSubscriptionIdsAsync(default).ConfigureAwait(true))
		{
			var response = await Client
			.Resources
			.GetAsync(
				subscriptionId,
				filter: "resourceType eq 'Microsoft.OperationalInsights/workspaces'",
				cancellationToken: default
				)
			.ConfigureAwait(true);

			response.Should().NotBeNull();
			response.Values.Should().NotBeNull();
			if (response.Values.Count != 0)
			{
				response.Values.Should().OnlyContain(x => x.Type == "Microsoft.OperationalInsights/workspaces");
			}
		}
	}
}