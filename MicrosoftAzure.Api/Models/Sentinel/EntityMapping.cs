using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class EntityMapping
{
	[JsonPropertyName("entityType")]

	public required string EntityType { get; set; }

	[JsonPropertyName("fieldMappings")]
	public required IReadOnlyCollection<FieldMapping> FieldMappings { get; set; }
}
