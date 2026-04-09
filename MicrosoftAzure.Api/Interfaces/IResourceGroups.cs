using MicrosoftAzure.Api.Models.Resources;
using MicrosoftAzure.Api.Models.Responses;
using Refit;

namespace MicrosoftAzure.Api.Interfaces;

/// <summary>
/// Defines the contract for resource groups operations.
/// </summary>
public interface IResourceGroups
{
	/// <summary>
	/// Executes the get operation.
	/// </summary>
	[Get("/subscriptions/{subscriptionId}/resourcegroups?api-version=2024-03-01")]
	Task<PlainResponse<ResourceGroup>> GetAsync(
		Guid subscriptionId,
		CancellationToken cancellationToken);
}
