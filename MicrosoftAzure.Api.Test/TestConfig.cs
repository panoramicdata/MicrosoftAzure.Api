using System;

namespace MicrosoftAzure.Api.Test;

public class TestConfig
{
	public MicrosoftAzureClientOptions Options { get; set; } = null!;

	public string WorkspaceName { get; set; } = null!;

	public string ResourceGroupName { get; set; } = null!;

	public Guid SubscriptionId { get; set; }

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
