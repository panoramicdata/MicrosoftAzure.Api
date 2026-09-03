using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

/// <summary>
/// Represents the resource.
/// </summary>
public class Resource : ResourceBase
{
	/// <summary>
	/// Gets or sets the properties.
	/// </summary>
	[JsonPropertyName("properties")]
	public IReadOnlyDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

	/// <summary>
	/// Gets or sets the system data.
	/// </summary>
	[JsonPropertyName("systemData")]
	public SystemData? SystemData { get; set; }
}
