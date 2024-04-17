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
}
