using MicrosoftAzure.Api.Models.Resources;
using MicrosoftAzure.Api.Models.Responses;
using Refit;

namespace MicrosoftAzure.Api.Interfaces;

public interface IResources
{

	[Get("/subscriptions/{subscriptionId}/resources?api-version=2024-03-01")]
	Task<PlainResponse<Resource>> GetAsync(
	Guid subscriptionId,
	[AliasAs("$filter")] string? filter = default,
	[AliasAs("$expand")] string? expand = default,
	[AliasAs("$skip")] int? skip = default,
	[AliasAs("$take")] int? take = default,
	CancellationToken cancellationToken = default);

	[Get("/subscriptions/{subscriptionId}/resources?api-version=2024-03-01")]
	Task<PlainResponse<Resource>> GetAsync(
		Guid subscriptionId,
		CancellationToken cancellationToken);
}