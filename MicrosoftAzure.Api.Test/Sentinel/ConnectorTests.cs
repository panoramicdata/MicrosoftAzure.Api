using MicrosoftAzure.Api.Exceptions;
using MicrosoftAzure.Api.Test.Extensions;
using System.Linq;

namespace MicrosoftAzure.Api.Test.Sentinel;

public class ConnectorTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetAllAsync_Succeeds()
	{
		foreach (var subscriptionId in await GetSubscriptionIdsAsync(CancellationToken))
		{
			var resourceGroups = await Client
				.ResourceGroups
				.GetAsync(subscriptionId, CancellationToken);

			var resourceGroupNames = resourceGroups
				.Values
				.Select(x => x.Name)
				.ToList();

			foreach (var resourceGroupName in resourceGroupNames)
			{
				var workspaces = await Client
					.Resources
					.GetAsync(
						subscriptionId,
						filter: $"resourceGroup eq '{resourceGroupName}' and resourceType eq 'Microsoft.OperationalInsights/workspaces'",
						cancellationToken: CancellationToken);

				var workspaceNames = workspaces
					.Values
					.Select(x => x.Name)
					.ToList();

				foreach (var workspaceName in workspaceNames)
				{
					try
					{
						var response = await Client
						.Sentinel
						.GetDataConnectorsAsync(
							subscriptionId,
							resourceGroupName,
							workspaceName,
							CancellationToken);

						response.CheckValues();
						response.Values.Should().OnlyContain(x => x.Kind != null);
					}
					catch (BadRequestException ex)
					{
						// Expected responses
						if (
							ex.ErrorResponse.Error.Message.Contains("is not onboarded to Microsoft Sentinel", System.StringComparison.Ordinal) ||
							ex.ErrorResponse.Error.Message.Contains("is not registered to 'Microsoft.SecurityInsights'", System.StringComparison.Ordinal)
						)
						{
							continue;
						}
					}
				}
			}
		}
	}
}