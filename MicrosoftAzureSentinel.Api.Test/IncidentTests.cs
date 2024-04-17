using MicrosoftAzureSentinel.Api.Test.Extensions;

namespace MicrosoftAzureSentinel.Api.Test;

public class IncidentTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetIncidentsAsync_SimpleQuery_Succeeds()
	{
		var result = await Client
			.Incidents
			.GetAsync(
				TestConfig.SubscriptionId,
				TestConfig.ResourceGroupName,
				TestConfig.WorkspaceName,
				default
			)
			.ConfigureAwait(true);

		result.CheckValues();
	}
}
