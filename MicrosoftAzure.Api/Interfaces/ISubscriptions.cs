using MicrosoftAzure.Api.Models.Responses;
using MicrosoftAzure.Api.Models.Subscriptions;
using Refit;

namespace MicrosoftAzure.Api.Interfaces;

/// <summary>
/// Defines the contract for subscriptions operations.
/// </summary>
public interface ISubscriptions
{
	/// <summary>
	/// See https://learn.microsoft.com/en-us/rest/api/subscription/subscriptions/list?view=rest-subscription-2021-10-01&amp;tabs=HTTP#listsubscriptions
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	[Get("/subscriptions?api-version=2016-06-01")]
	Task<PlainResponse<Subscription>> GetAsync(CancellationToken cancellationToken);
}
