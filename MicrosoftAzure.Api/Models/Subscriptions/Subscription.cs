using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Subscriptions;

public class Subscription
{
	[JsonPropertyName("id")]
	public required string Id { get; set; }

	[JsonPropertyName("subscriptionId")]
	public required string SubscriptionId { get; set; }

	[JsonPropertyName("tenantId")]
	public string? TenantId { get; set; }

	[JsonPropertyName("displayName")]
	public required string DisplayName { get; set; }

	[JsonPropertyName("state")]
	public required string State { get; set; }

	[JsonPropertyName("subscriptionPolicies")]
	public required SubscriptionPolicies SubscriptionPolicies { get; set; }

	[JsonPropertyName("authorizationSource")]
	public required string AuthorizationSource { get; set; }

	[JsonPropertyName("managedByTenants")]
	public IReadOnlyCollection<SubscriptionTenantInfo>? ManagedByTenants { get; set; }

	[JsonPropertyName("tags")]
	public IReadOnlyDictionary<string, string>? Tags { get; set; }
}
