using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

/// <summary>
/// Represents the resource properties.
/// </summary>
public class ResourceProperties
{
	/// <summary>
	/// Gets or sets the customer id.
	/// </summary>
	[JsonPropertyName("customerId")]
	public required Guid CustomerId { get; set; }

	/// <summary>
	/// Gets or sets the provisioning state.
	/// </summary>
	[JsonPropertyName("provisioningState")]
	public required string ProvisioningState { get; set; }

	/// <summary>
	/// Gets or sets the sku.
	/// </summary>
	[JsonPropertyName("sku")]
	public required Sku Sku { get; set; }

	/// <summary>
	/// Gets or sets the retention in days.
	/// </summary>
	[JsonPropertyName("retentionInDays")]
	public required int RetentionInDays { get; set; }

	/// <summary>
	/// Gets or sets the features.
	/// </summary>
	[JsonPropertyName("features")]
	public required Features Features { get; set; }

	/// <summary>
	/// Gets or sets the workspace capping.
	/// </summary>
	[JsonPropertyName("workspaceCapping")]
	public required WorkspaceCapping WorkspaceCapping { get; set; }

	/// <summary>
	/// Gets or sets the public network access for ingestion.
	/// </summary>
	[JsonPropertyName("publicNetworkAccessForIngestion")]
	public required string PublicNetworkAccessForIngestion { get; set; }

	/// <summary>
	/// Gets or sets the public network access for query.
	/// </summary>
	[JsonPropertyName("publicNetworkAccessForQuery")]
	public required string PublicNetworkAccessForQuery { get; set; }

	/// <summary>
	/// Gets or sets the created date.
	/// </summary>
	[JsonPropertyName("createdDate")]
	public required DateTime CreatedDate { get; set; }

	/// <summary>
	/// Gets or sets the modified date.
	/// </summary>
	[JsonPropertyName("modifiedDate")]
	public required DateTime ModifiedDate { get; set; }
}
