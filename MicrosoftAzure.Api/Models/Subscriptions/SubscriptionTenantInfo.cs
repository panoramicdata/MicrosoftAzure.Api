using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Subscriptions;

/// <summary>
/// Represents the subscription tenant info.
/// </summary>
public class SubscriptionTenantInfo
{
	/// <summary>
	/// Gets or sets the id.
	/// </summary>
	[JsonPropertyName("tenantId")]
	public required string Id { get; set; }
}
