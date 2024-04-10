namespace MicrosoftAzureSentinel.Api.Test;

public class TestBase
{
	private MicrosoftAzureSentinelClient? _client;

	protected TestConfig TestConfig { get; }
	protected ILogger Logger { get; }

	public TestBase(ITestOutputHelper testOutputHelper)
	{
		Logger = testOutputHelper.BuildLoggerFor<TestBase>();
		TestConfig = TestConfig.Load();
		TestConfig.Options.Logger = testOutputHelper.BuildLoggerFor<MicrosoftAzureSentinelClient>();
	}

	protected MicrosoftAzureSentinelClient Client
		=> _client ??= new(TestConfig.Options);
}
