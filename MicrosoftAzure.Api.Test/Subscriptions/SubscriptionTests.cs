﻿using MicrosoftAzureSentinel.Api.Test;

namespace MicrosoftAzure.Api.Test.Subscriptions;

public class SubscriptionTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetAllAsync_Succeeds()
	{
		var response = await Client
			.Subscriptions
			.GetSubscriptionsAsync(default)
			.ConfigureAwait(true);

		response.Should().NotBeNull();
		response.Values.Should().NotBeNullOrEmpty();
	}
}

