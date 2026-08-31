using MicrosoftAzure.Api.Test.Extensions;

namespace MicrosoftAzure.Api.Test.Sentinel;

public class IncidentTests(ITestOutputHelper testOutputHelper) : SentinelTestBase(testOutputHelper)
{
	[Fact]
	public Task GetAllAsync_Succeeds()
		=> ForEachWorkspaceAsync(
			async (subscriptionId, resourceGroupName, workspaceName) =>
			{
				var response = await Client
					.Sentinel
					.GetIncidentsAsync(
						subscriptionId,
						resourceGroupName,
						workspaceName,
						CancellationToken);

				response.CheckValues();
			},
			CancellationToken);
}
