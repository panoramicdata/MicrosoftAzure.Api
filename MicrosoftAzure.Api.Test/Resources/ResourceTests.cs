namespace MicrosoftAzure.Api.Test.Resources;

public class ResourceTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetResourcesAsync_Succeeds()
	{
		foreach (var subscriptionId in await GetSubscriptionIdsAsync(CancellationToken))
		{
			var response = await Client
				.Resources
				.GetAsync(
					subscriptionId,
					cancellationToken: CancellationToken);

			response.Should().NotBeNull();
			response.Values.Should().NotBeNullOrEmpty();
		}
	}

	[Fact]
	public async Task GetResourcesAsync_WithFilter_Succeeds()
	{
		foreach (var subscriptionId in await GetSubscriptionIdsAsync(CancellationToken))
		{
			var response = await Client
				.Resources
				.GetAsync(
					subscriptionId,
					filter: "resourceType eq 'Microsoft.OperationalInsights/workspaces'",
					cancellationToken: CancellationToken);

			response.Should().NotBeNull();
			response.Values.Should().NotBeNull();
			if (response.Values.Count != 0)
			{
				response.Values.Should().OnlyContain(x => x.Type == "Microsoft.OperationalInsights/workspaces");

				foreach (var resource in response.Values)
				{
					// The resource id will be in the form: /subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/myRgName/providers/Microsoft.OperationalInsights/workspaces/MyWorkspaceName
					var resourceParts = resource.Id.Split('/');
					resourceParts.Should().NotBeNullOrEmpty();
					resourceParts.Should().HaveCount(9);

					var resourceGroupName = resourceParts[4];
					var providerName = resourceParts[6];
					var workspaceName = resourceParts[8];

					var resourceResponse = await Client
						.Resources
						.GetPropertiesAsync(
							subscriptionId,
							resourceGroupName,
							providerName,
							workspaceName,
							CancellationToken);
				}
			}
		}
	}
}
