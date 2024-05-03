using MicrosoftAzure.Api.Models;
using MicrosoftAzure.Api.Models.Resources;
using Refit;

namespace MicrosoftAzure.Api.Interfaces;

public interface IResources
{
	[Get("/subscriptions/{subscriptionId}/resourcegroups?api-version=2024-03-01")]
	Task<PlainResponse<ResourceGroup>> GetResourceGroupsAsync(
		Guid subscriptionId,
		CancellationToken cancellationToken);

	[Get("/subscriptions/{subscriptionId}/resources?api-version=2024-03-01")]
	Task<PlainResponse<Resource>> GetResourcesAsync(
	Guid subscriptionId,
	CancellationToken cancellationToken);
}