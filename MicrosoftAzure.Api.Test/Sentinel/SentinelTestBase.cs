using MicrosoftAzure.Api.Exceptions;
using System;
using System.Linq;
using System.Threading;

namespace MicrosoftAzure.Api.Test.Sentinel;

/// <summary>
/// Shared plumbing for the Sentinel tests, each of which exercises an operation against
/// every Log Analytics workspace in every accessible subscription.
/// </summary>
public abstract class SentinelTestBase(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	private const string WorkspaceResourceType = "Microsoft.OperationalInsights/workspaces";

	/// <summary>
	/// Invokes <paramref name="assertAsync"/> once per workspace, passing the subscription id,
	/// resource group name and workspace name. Workspaces that are not onboarded to Sentinel
	/// are skipped, as the operations under test cannot apply to them.
	/// </summary>
	protected async Task ForEachWorkspaceAsync(
		Func<Guid, string, string, Task> assertAsync,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(assertAsync);

		foreach (var subscriptionId in await GetSubscriptionIdsAsync(cancellationToken).ConfigureAwait(false))
		{
			var resourceGroups = await Client
				.ResourceGroups
				.GetAsync(subscriptionId, cancellationToken)
				.ConfigureAwait(false);

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
						$"resourceGroup eq '{resourceGroupName}' and resourceType eq '{WorkspaceResourceType}'",
						cancellationToken)
					.ConfigureAwait(false);

				var workspaceNames = workspaces
					.Values
					.Select(x => x.Name)
					.ToList();

				foreach (var workspaceName in workspaceNames)
				{
					try
					{
						await assertAsync(subscriptionId, resourceGroupName, workspaceName).ConfigureAwait(false);
					}
					catch (BadRequestException ex) when (IsNotSentinelWorkspace(ex))
					{
						// Expected: the workspace has no Sentinel on it, so there is nothing to assert.
					}
				}
			}
		}
	}

	private static bool IsNotSentinelWorkspace(BadRequestException ex)
		=> ex.ErrorResponse.Error.Message.Contains("is not onboarded to Microsoft Sentinel", StringComparison.Ordinal)
			|| ex.ErrorResponse.Error.Message.Contains("is not registered to 'Microsoft.SecurityInsights'", StringComparison.Ordinal);
}
