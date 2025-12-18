namespace MicrosoftAzure.Api.Test.Subscriptions;

public class SubscriptionTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetAllAsync_Succeeds()
	{
		var response = await Client
			.Subscriptions
			.GetAsync(CancellationToken);

		response.Should().NotBeNull();
		response.Values.Should().NotBeNullOrEmpty();
	}
}

