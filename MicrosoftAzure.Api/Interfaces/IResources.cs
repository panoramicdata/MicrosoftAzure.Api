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
	[AliasAs("$filter")] string? filter = default,
	[AliasAs("$expand")] string? expand = default,
	[AliasAs("$skip")] int? skip = default,
	[AliasAs("$take")] int? take = default,
	CancellationToken cancellationToken = default);

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
