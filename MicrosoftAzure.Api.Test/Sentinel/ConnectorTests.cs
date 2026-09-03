using MicrosoftAzure.Api.Test.Extensions;

namespace MicrosoftAzure.Api.Test.Sentinel;

public class ConnectorTests(ITestOutputHelper testOutputHelper) : SentinelTestBase(testOutputHelper)
{
	[Fact]
	public Task GetAllAsync_Succeeds()
		=> ForEachWorkspaceAsync(
			async (subscriptionId, resourceGroupName, workspaceName) =>
			{
				var response = await Client
					.Sentinel
					.GetDataConnectorsAsync(
						subscriptionId,
						resourceGroupName,
						workspaceName,
						CancellationToken)
					.ConfigureAwait(false);

				response.CheckValues();
				response.Values.Should().OnlyContain(x => x.Kind != null);
			},
			CancellationToken);
}
