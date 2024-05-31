using MicrosoftAzure.Api.Exceptions;
using MicrosoftAzure.Api.Test.Extensions;
using System.Linq;

namespace MicrosoftAzure.Api.Test.Sentinel;

public class IncidentTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetAllAsync_Succeeds()
	{
		foreach (var subscriptionId in await GetSubscriptionIdsAsync(default).ConfigureAwait(true))
		{
			var resourceGroups = await Client
				.ResourceGroups
				.GetAsync(subscriptionId, default)
				.ConfigureAwait(true);

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
						filter: $"resourceGroup eq '{resourceGroupName}' and resourceType eq 'Microsoft.OperationalInsights/workspaces'", default)
					.ConfigureAwait(true);

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
							.GetIncidentsAsync(
								subscriptionId,
								resourceGroupName,
								workspaceName,
								default
							)
							.ConfigureAwait(true);

						response.CheckValues();
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
