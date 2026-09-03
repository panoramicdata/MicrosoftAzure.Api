using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

/// <summary>
/// Represents the resource group.
/// </summary>
public class ResourceGroup : ResourceBase
{
	/// <summary>
	/// Gets or sets the properties.
	/// </summary>
	[JsonPropertyName("properties")]
	public required ResourceGroupProperties Properties { get; set; }
}
