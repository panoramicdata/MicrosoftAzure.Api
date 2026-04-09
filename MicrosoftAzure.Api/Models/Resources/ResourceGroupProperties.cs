using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

/// <summary>
/// Represents the resource group properties.
/// </summary>
public class ResourceGroupProperties
{
	/// <summary>
	/// Gets or sets the provisioning state.
	/// </summary>
	[JsonPropertyName("provisioningState")]
	public required string ProvisioningState { get; set; }
}
