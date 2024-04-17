using MicrosoftAzure.Api.Test;

namespace MicrosoftAzure.Api.Test.Resources;

public class ResourceTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetAllAsync_Succeeds()
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
}

