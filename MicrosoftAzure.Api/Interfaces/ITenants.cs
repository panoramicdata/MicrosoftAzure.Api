using MicrosoftAzure.Api.Models;
using MicrosoftAzure.Api.Models.Subscriptions;
using Refit;

namespace MicrosoftAzure.Api.Interfaces;

public interface ITenants
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	[Get("/tenants")]
	Task<PlainResponse<Tenant>> GetTenantsAsync(CancellationToken cancellationToken);

	/// <summary>
	/// https://learn.microsoft.com/en-us/graph/api/managedtenants-managedtenant-list-tenants?view=graph-rest-beta&tabs=csharp#http-request
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	[Get("/beta/tenantRelationships/managedTenants/tenants")]
	Task<PlainResponse<Tenant>> GetManagedTenantsAsync(CancellationToken cancellationToken);
}