using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

public class ResourceGroupProperties
{
	[JsonPropertyName("provisioningState")]
	public required string ProvisioningState { get; set; }
}
