using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Subscriptions;

public class SubscriptionPolicies
{
	[JsonPropertyName("locationPlacementId")]
	public required string LocationPlacementId { get; set; }

	[JsonPropertyName("quotaId")]
	public required string QuotaId { get; set; }

	[JsonPropertyName("spendingLimit")]
	public required string SpendingLimit { get; set; }
}
