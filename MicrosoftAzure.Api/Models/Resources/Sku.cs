using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

/// <summary>
/// Represents the sku.
/// </summary>
public class Sku
{
	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	/// <summary>
	/// Gets or sets the last sku update.
	/// </summary>
	[JsonPropertyName("lastSkuUpdate")]
	public DateTime? LastSkuUpdate { get; set; }
}
