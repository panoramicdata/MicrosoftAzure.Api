using MicrosoftAzure.Api.Models.Responses;
using MicrosoftAzure.Api.Models.Subscriptions;
using Refit;

namespace MicrosoftAzure.Api.Interfaces;

/// <summary>
/// Defines the contract for tenants operations.
/// </summary>
public interface ITenants
{
	/// <summary>
	/// https://learn.microsoft.com/en-us/rest/api/resources/tenants/list?view=rest-resources-2022-12-01&amp;tabs=HTTP
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	[Get("/tenants?api-version=2022-12-01")]
	Task<PlainResponse<Tenant>> GetTenantsAsync(CancellationToken cancellationToken);
}
