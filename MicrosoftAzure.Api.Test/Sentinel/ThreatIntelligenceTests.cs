using MicrosoftAzure.Api.Test.Extensions;
using System.Linq;

namespace MicrosoftAzure.Api.Test.Sentinel;

public class ThreatIntelligenceTests(ITestOutputHelper testOutputHelper) : SentinelTestBase(testOutputHelper)
{
	[Fact]
	public Task GetIndicatorsAsync_SimpleQuery_Succeeds()
		=> ForEachWorkspaceAsync(
			async (subscriptionId, resourceGroupName, workspaceName) =>
			{
				var response = await Client
					.Sentinel
					.GetThreatIndicatorsAsync(
						subscriptionId,
						resourceGroupName,
						workspaceName,
						CancellationToken);

				response.CheckValues();
				if (response.Values.Count > 0)
				{
					response.Values.Should().OnlyContain(x => x.Kind != null);
				}
			},
			CancellationToken);

	[Fact]
	public Task GetMetricsAsync_SimpleQuery_Succeeds()
		=> ForEachWorkspaceAsync(
			async (subscriptionId, resourceGroupName, workspaceName) =>
			{
				var response = await Client
					.Sentinel
					.GetThreatIndicatorMetricsAsync(
						subscriptionId,
						resourceGroupName,
						workspaceName,
						CancellationToken);

				response.Should().NotBeNull();
				response.Values.First().Properties.Should().NotBeNull();
			},
			CancellationToken);

	[Fact]
	public Task GetWorkspacesAsync_SimpleQuery_Succeeds()
		=> ForEachWorkspaceAsync(
			async (subscriptionId, resourceGroupName, _) =>
			{
				var response = await Client
					.Sentinel
					.GetWorkspacesAsync(
						subscriptionId,
						resourceGroupName,
						CancellationToken);

				response.Should().NotBeNull();
				response.Values.First().Properties.Should().NotBeNull();
			},
			CancellationToken);
}
