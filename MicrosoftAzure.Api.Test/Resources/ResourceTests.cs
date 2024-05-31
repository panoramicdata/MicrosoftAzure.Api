namespace MicrosoftAzure.Api.Test.Resources;

public class ResourceTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetResourcesAsync_Succeeds()
	{
		var response = await Client
			.Resources
			.GetAsync(
				TestConfig.SubscriptionId,
				default)
			.ConfigureAwait(true);

		response.Should().NotBeNull();
		response.Values.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public async Task GetResourcesAsync_WithFilter_Succeeds()
	{
		var response = await Client
			.Resources
			.GetAsync(
				TestConfig.SubscriptionId,
				filter: "resourceType eq 'Microsoft.OperationalInsights/workspaces'",
				cancellationToken: default
				)
			.ConfigureAwait(true);

		response.Should().NotBeNull();
		response.Values.Should().NotBeNullOrEmpty();
		response.Values.Should().OnlyContain(x => x.Type == "Microsoft.OperationalInsights/workspaces");
	}
}
