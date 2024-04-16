using MicrosoftAzureSentinel.Api.Models;

namespace MicrosoftAzureSentinel.Api.Test;

public class ConnectorTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetConnectorsAsync_SimpleQuery_Succeeds()
	{
		var result = await Client
			.GetConnectorsAsync(
				TestConfig.SubscriptionId,
				TestConfig.ResourceGroupName,
				TestConfig.WorkspaceName,
				default
			)
			.ConfigureAwait(true);
		result.Should().BeOfType<DataConnectionsResponse>();
		result.Value.Should().NotBeNullOrEmpty();
		result.Value.Should().NotContainNulls();
		result.Value.Should().OnlyContain(x => x.Id != null);
		result.Value.Should().OnlyContain(x => x.Name != null);
		result.Value.Should().OnlyContain(x => x.Type != null);
		result.Value.Should().OnlyContain(x => x.Kind != null);
		result.Value.Should().OnlyContain(x => x.Properties != null);
	}
}

public class AlertRuleTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetAlertRulesAsync_SimpleQuery_Succeeds()
	{
		var result = await Client
			.GetAlertRulesAsync(
				TestConfig.SubscriptionId,
				TestConfig.ResourceGroupName,
				TestConfig.WorkspaceName,
				default
			)
			.ConfigureAwait(true);
		result.Should().BeOfType<AlertRulesResponse>();
		result.Value.Should().NotBeNullOrEmpty();
		result.Value.Should().NotContainNulls();
		result.Value.Should().OnlyContain(x => x.Id != null);
		result.Value.Should().OnlyContain(x => x.Name != null);
		result.Value.Should().OnlyContain(x => x.Etag != null);
		result.Value.Should().OnlyContain(x => x.Type != null);
		result.Value.Should().OnlyContain(x => x.Kind != null);
		result.Value.Should().OnlyContain(x => x.Properties != null);
	}
}
