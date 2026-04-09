using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Subscriptions;

/// <summary>
/// Represents the subscription.
/// </summary>
public class Subscription
{
	/// <summary>
	/// Gets or sets the id.
	/// </summary>
	[JsonPropertyName("id")]
	public required string Id { get; set; }

	/// <summary>
	/// Gets or sets the subscription id.
	/// </summary>
	[JsonPropertyName("subscriptionId")]
	public required string SubscriptionId { get; set; }

	/// <summary>
	/// Gets or sets the tenant id.
	/// </summary>
	[JsonPropertyName("tenantId")]
	public string? TenantId { get; set; }

	/// <summary>
	/// Gets or sets the display name.
	/// </summary>
	[JsonPropertyName("displayName")]
	public required string DisplayName { get; set; }

	/// <summary>
	/// Gets or sets the state.
	/// </summary>
	[JsonPropertyName("state")]
	public required string State { get; set; }

	/// <summary>
	/// Gets or sets the subscription policies.
	/// </summary>
	[JsonPropertyName("subscriptionPolicies")]
	public required SubscriptionPolicies SubscriptionPolicies { get; set; }

	/// <summary>
	/// Gets or sets the authorization source.
	/// </summary>
	[JsonPropertyName("authorizationSource")]
	public required string AuthorizationSource { get; set; }

	/// <summary>
	/// Gets or sets the managed by tenants.
	/// </summary>
	[JsonPropertyName("managedByTenants")]
	public IReadOnlyCollection<SubscriptionTenantInfo>? ManagedByTenants { get; set; }

	/// <summary>
	/// Gets or sets the tags.
	/// </summary>
	[JsonPropertyName("tags")]
	public IReadOnlyDictionary<string, string>? Tags { get; set; }
}
