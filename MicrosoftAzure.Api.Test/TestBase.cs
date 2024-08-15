using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MicrosoftAzure.Api.Test;

public class TestBase
{
	private MicrosoftAzureClient? _client;

	protected TestConfig TestConfig { get; }

	protected ILogger Logger { get; }

	public TestBase(ITestOutputHelper testOutputHelper)
	{
		Logger = testOutputHelper.BuildLoggerFor<TestBase>();
		TestConfig = TestConfig.Load();
		TestConfig.Options.Logger = testOutputHelper.BuildLoggerFor<MicrosoftAzureClient>();
	}

	protected MicrosoftAzureClient Client => _client ??= new(TestConfig.Options);

	protected async Task<IEnumerable<Guid>> GetSubscriptionIdsAsync(CancellationToken cancellationToken)
	{
		var response = await Client
			.Subscriptions
			.GetAsync(cancellationToken)
			.ConfigureAwait(false);

		response.Values.Should().NotBeNullOrEmpty();

		return response.Values.Take(TestConfig.MaxSubscriptionTake).Select(s => new Guid(s.Id.Split('/').Last()));
	}
}
