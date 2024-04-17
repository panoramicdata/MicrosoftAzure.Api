using MicrosoftAzure.Api.Test;
using MicrosoftAzure.Api.Test.Extensions;
using System.Linq;

namespace MicrosoftAzure.Api.Test.Sentinel;

public class ThreatIntelligenceTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetIndicatorsAsync_SimpleQuery_Succeeds()
	{
		var response = await Client
			.Sentinel
			.GetThreatIndicatorsAsync(
				TestConfig.SubscriptionId,
				TestConfig.ResourceGroupName,
				TestConfig.WorkspaceName,
				default
			)
			.ConfigureAwait(true);

		response.CheckValues();
		response.Values.Should().OnlyContain(x => x.Kind != null);
	}

	[Fact]
	public async Task GetMetricsAsync_SimpleQuery_Succeeds()
	{
		var response = await Client
			.Sentinel
			.GetThreatIndicatorMetricsAsync(
				TestConfig.SubscriptionId,
				TestConfig.ResourceGroupName,
				TestConfig.WorkspaceName,
				default
			)
			.ConfigureAwait(true);
		response.Should().NotBeNull();
		response.Values.First().Properties.Should().NotBeNull();
	}
}

