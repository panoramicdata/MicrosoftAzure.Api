using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the data connector.
/// </summary>
public class DataConnector
{
	/// <summary>
	/// Gets or sets the data types.
	/// </summary>
	[JsonPropertyName("dataTypes")]
	public required IReadOnlyDictionary<string, StateObject> DataTypes { get; set; }

	/// <summary>
	/// Gets or sets the tenant id.
	/// </summary>
	[JsonPropertyName("tenantId")]
	public string? TenantId { get; set; }

	/// <summary>
	/// Gets or sets the subscription id.
	/// </summary>
	[JsonPropertyName("subscriptionId")]
	public string? SubscriptionId { get; set; }

	/// <summary>
	/// Gets or sets the tip lookback period.
	/// </summary>
	[JsonPropertyName("tipLookbackPeriod")]
	public DateTime? TipLookbackPeriod { get; set; }
}

