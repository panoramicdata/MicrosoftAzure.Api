using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

public class SystemData
{
	[JsonPropertyName("createdBy")]
	public required string CreatedBy { get; set; }

	[JsonPropertyName("createdByType")]
	public required string CreatedByType { get; set; }

	[JsonPropertyName("createdAt")]
	public required DateTime CreatedAt { get; set; }

	[JsonPropertyName("lastModifiedBy")]
	public required string LastModifiedBy { get; set; }

	[JsonPropertyName("lastModifiedByType")]
	public required string LastModifiedByType { get; set; }

	[JsonPropertyName("lastModifiedAt")]
	public required DateTime LastModifiedAt { get; set; }
}
