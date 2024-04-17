using MicrosoftAzureSentinel.Api.Test;
using MicrosoftAzureSentinel.Api.Test.Extensions;

namespace MicrosoftAzure.Api.Test.Sentinel;

public class AlertRuleTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetAllAsync_Succeeds()
	{
		var response = await Client
			.Sentinel
			.GetAlertRulesAsync(
				TestConfig.SubscriptionId,
				TestConfig.ResourceGroupName,
				TestConfig.WorkspaceName,
				default
			)
			.ConfigureAwait(true);

		response.CheckValues();
		response.Values.Should().OnlyContain(x => x.Kind != null);
	}
}