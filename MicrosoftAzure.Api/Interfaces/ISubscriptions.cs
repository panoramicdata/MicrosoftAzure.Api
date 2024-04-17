using MicrosoftAzure.Api.Models.Subscriptions;
using MicrosoftAzureSentinel.Api.Models;
using Refit;

namespace MicrosoftAzureSentinel.Api.Interfaces;

public interface ISubscriptions
{
	[Get("/subscriptions?api-version=2024-03-01")]
	Task<PlainResponse<Subscription>> GetSubscriptionsAsync(CancellationToken cancellationToken);
}