using MicrosoftAzure.Api.Models.Responses;
using MicrosoftAzure.Api.Models.Subscriptions;
using Refit;

namespace MicrosoftAzure.Api.Interfaces;

/// <summary>
/// Defines the contract for managed tenants operations.
/// </summary>
public interface IManagedTenants
{
	/// <summary>
	/// https://learn.microsoft.com/en-us/graph/api/managedtenants-managedtenant-list-tenants?view=graph-rest-beta&amp;tabs=csharp#http-request
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	[Get("/beta/tenantRelationships/managedTenants/tenants")]
	Task<PlainResponse<Tenant>> GetAsync(CancellationToken cancellationToken);
}
