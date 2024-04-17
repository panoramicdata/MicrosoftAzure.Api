using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Subscriptions;

public class SubscriptionTenantInfo
{
	[JsonPropertyName("tenantId")]
	public required string Id { get; set; }
}
