using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

public class Sku
{
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	[JsonPropertyName("lastSkuUpdate")]
	public DateTime? LastSkuUpdate { get; set; }
}
