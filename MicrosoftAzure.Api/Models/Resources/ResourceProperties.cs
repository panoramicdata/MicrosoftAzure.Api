using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

public class ResourceProperties
{
	[JsonPropertyName("customerId")]
	public required string CustomerId { get; set; }

	[JsonPropertyName("provisioningState")]
	public required string ProvisioningState { get; set; }

	[JsonPropertyName("sku")]
	public required Sku Sku { get; set; }

	[JsonPropertyName("retentionInDays")]
	public required int RetentionInDays { get; set; }

	[JsonPropertyName("features")]
	public required Features Features { get; set; }

	[JsonPropertyName("workspaceCapping")]
	public required WorkspaceCapping WorkspaceCapping { get; set; }

	[JsonPropertyName("publicNetworkAccessForIngestion")]
	public required string PublicNetworkAccessForIngestion { get; set; }

	[JsonPropertyName("publicNetworkAccessForQuery")]
	public required string PublicNetworkAccessForQuery { get; set; }

	[JsonPropertyName("createdDate")]
	public required DateTime CreatedDate { get; set; }

	[JsonPropertyName("modifiedDate")]
	public required DateTime ModifiedDate { get; set; }
}
