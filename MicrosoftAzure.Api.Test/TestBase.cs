using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MicrosoftAzure.Api.Test;

public class TestBase
{
	private readonly ILoggerFactory _loggerFactory;

	protected TestConfig TestConfig { get; }

	protected static CancellationToken CancellationToken => TestContext.Current.CancellationToken;

	protected ILogger Logger { get; }

	public TestBase(ITestOutputHelper testOutputHelper)
	{
		_ = testOutputHelper; // Kept for compatibility with derived test classes
		_loggerFactory = LoggerFactory.Create(builder =>
		{
			builder
				.AddDebug()
				.AddFilter(level => level >= LogLevel.Debug);
		});

		Logger = _loggerFactory.CreateLogger<TestBase>();
		TestConfig = TestConfig.Load();
		TestConfig.Options.Logger = _loggerFactory.CreateLogger<MicrosoftAzureClient>();
	}

	protected MicrosoftAzureClient Client => field ??= new(TestConfig.Options);

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
