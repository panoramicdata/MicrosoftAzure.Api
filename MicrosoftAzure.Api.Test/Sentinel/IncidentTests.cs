using MicrosoftAzure.Api.Test;
using MicrosoftAzure.Api.Test.Extensions;

namespace MicrosoftAzure.Api.Test.Sentinel;

public class IncidentTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetAllAsync_Succeeds()
	{
		var result = await Client
			.Sentinel
			.GetIncidentsAsync(
				TestConfig.SubscriptionId,
				TestConfig.ResourceGroupName,
				TestConfig.WorkspaceName,
				default
			)
			.ConfigureAwait(true);

		result.CheckValues();
	}
}
