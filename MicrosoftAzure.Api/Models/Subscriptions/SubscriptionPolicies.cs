using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Subscriptions;

/// <summary>
/// Represents the subscription policies.
/// </summary>
public class SubscriptionPolicies
{
	/// <summary>
	/// Gets or sets the location placement id.
	/// </summary>
	[JsonPropertyName("locationPlacementId")]
	public required string LocationPlacementId { get; set; }

	/// <summary>
	/// Gets or sets the quota id.
	/// </summary>
	[JsonPropertyName("quotaId")]
	public required string QuotaId { get; set; }

	/// <summary>
	/// Gets or sets the spending limit.
	/// </summary>
	[JsonPropertyName("spendingLimit")]
	public required string SpendingLimit { get; set; }
}
