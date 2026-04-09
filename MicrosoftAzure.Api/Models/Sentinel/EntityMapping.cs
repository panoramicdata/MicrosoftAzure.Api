using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the entity mapping.
/// </summary>
public class EntityMapping
{
	/// <summary>
	/// Gets or sets the entity type.
	/// </summary>
	[JsonPropertyName("entityType")]

	public required string EntityType { get; set; }

	/// <summary>
	/// Gets or sets the field mappings.
	/// </summary>
	[JsonPropertyName("fieldMappings")]
	public required IReadOnlyCollection<FieldMapping> FieldMappings { get; set; }
}
