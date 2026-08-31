using MicrosoftAzure.Api.Models.Resources;
using MicrosoftAzure.Api.Models.Responses;
using Refit;

namespace MicrosoftAzure.Api.Interfaces;

/// <summary>
/// Defines the contract for resources operations.
/// </summary>
public interface IResources
{

	/// <summary>
	/// Executes the get operation.
	/// </summary>
	[Get("/subscriptions/{subscriptionId}/resources?api-version=2024-03-01")]
	Task<PlainResponse<Resource>> GetAsync(
		Guid subscriptionId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Executes the get operation, restricted by a filter.
	/// </summary>
	[Get("/subscriptions/{subscriptionId}/resources?api-version=2024-03-01")]
	Task<PlainResponse<Resource>> GetAsync(
		Guid subscriptionId,
		[AliasAs("$filter")] string? filter,
		CancellationToken cancellationToken);

	/// <summary>
	/// Executes the get operation, restricted by a filter, expansion and paging.
	/// </summary>
	[Get("/subscriptions/{subscriptionId}/resources?api-version=2024-03-01")]
	Task<PlainResponse<Resource>> GetAsync(
		Guid subscriptionId,
		[AliasAs("$filter")] string? filter,
		[AliasAs("$expand")] string? expand,
		[AliasAs("$skip")] int? skip,
		[AliasAs("$take")] int? take,
		CancellationToken cancellationToken);

	/// <summary>
	/// Gets the properties.
	/// </summary>
	[Get("/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/{providerName}/workspaces/{workspaceName}?api-version=2023-09-01")]
	Task<ResourcePropertiesResponse> GetPropertiesAsync(
		Guid subscriptionId,
		string resourceGroupName,
		string providerName,
		string workspaceName,
		CancellationToken cancellationToken);
}
