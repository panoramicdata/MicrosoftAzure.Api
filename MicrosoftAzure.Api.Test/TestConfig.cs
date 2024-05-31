namespace MicrosoftAzure.Api.Test;

public class TestConfig
{
	public MicrosoftAzureClientOptions Options { get; set; } = null!;

	public int MaxSubscriptionTake { get; internal set; } = 5;

	internal static TestConfig Load()
	{
		var builder = new ConfigurationBuilder()
			.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../.."))
			.AddJsonFile("appsettings.json");
		var configurationRoot = builder.Build();
		var config = new TestConfig();
		configurationRoot.Bind(config);
		return config;
	}
}
