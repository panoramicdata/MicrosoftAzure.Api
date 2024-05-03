using MicrosoftAzure.Api.Models;
using MicrosoftAzure.Api.Models.Subscriptions;
using Refit;

namespace MicrosoftAzure.Api.Interfaces;

public interface IManagedTenants
{
	/// <summary>
	/// https://learn.microsoft.com/en-us/graph/api/managedtenants-managedtenant-list-tenants?view=graph-rest-beta&tabs=csharp#http-request
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	[Get("/beta/tenantRelationships/managedTenants/tenants")]
	Task<PlainResponse<Tenant>> GetManagedTenantsAsync(CancellationToken cancellationToken);
}