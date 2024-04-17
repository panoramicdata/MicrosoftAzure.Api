using MicrosoftAzureSentinel.Api.Test.Extensions;

namespace MicrosoftAzureSentinel.Api.Test;

public class ThreatIntelligenceIndicatorTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetThreatIntelligenceIndicatorsAsync_SimpleQuery_Succeeds()
	{
		var response = await Client
			.ThreatIntelligenceIndicators
			.GetAsync(
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
