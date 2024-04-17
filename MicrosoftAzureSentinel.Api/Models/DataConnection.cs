using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class DataConnection
{
	[JsonPropertyName("dataTypes")]
	public required IReadOnlyDictionary<string, StateObject> DataTypes { get; set; }

	[JsonPropertyName("tenantId")]
	public string? TenantId { get; set; }

	[JsonPropertyName("subscriptionId")]
	public string? SubscriptionId { get; set; }

	[JsonPropertyName("tipLookbackPeriod")]
	public DateTime? TipLookbackPeriod { get; set; }
}

