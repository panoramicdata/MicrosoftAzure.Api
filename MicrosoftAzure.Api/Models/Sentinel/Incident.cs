using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the incident.
/// </summary>
public class Incident
{
	/// <summary>
	/// Gets or sets the title.
	/// </summary>
	[JsonPropertyName("title")]
	public required string Title { get; set; }

	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets the severity.
	/// </summary>
	[JsonPropertyName("severity")]
	public required string Severity { get; set; }

	/// <summary>
	/// Gets or sets the status.
	/// </summary>
	[JsonPropertyName("status")]
	public required string Status { get; set; }

	/// <summary>
	/// Gets or sets the owner.
	/// </summary>
	[JsonPropertyName("owner")]
	public required IncidentOwner Owner { get; set; }

	/// <summary>
	/// Gets or sets the labels.
	/// </summary>
	[JsonPropertyName("labels")]
	public required IReadOnlyCollection<Label> Labels { get; set; }

	/// <summary>
	/// Gets or sets the first activity time utc.
	/// </summary>
	[JsonPropertyName("firstActivityTimeUtc")]
	public required DateTime FirstActivityTimeUtc { get; set; }

	/// <summary>
	/// Gets or sets the last activity time utc.
	/// </summary>
	[JsonPropertyName("lastActivityTimeUtc")]
	public required DateTime LastActivityTimeUtc { get; set; }

	/// <summary>
	/// Gets or sets the last modified time utc.
	/// </summary>
	[JsonPropertyName("lastModifiedTimeUtc")]
	public required DateTime LastModifiedTimeUtc { get; set; }

	/// <summary>
	/// Gets or sets the created time utc.
	/// </summary>
	[JsonPropertyName("createdTimeUtc")]
	public required DateTime CreatedTimeUtc { get; set; }

	/// <summary>
	/// Gets or sets the incident number.
	/// </summary>
	[JsonPropertyName("incidentNumber")]
	public required int IncidentNumber { get; set; }

	/// <summary>
	/// Gets or sets the additional data.
	/// </summary>
	[JsonPropertyName("additionalData")]
	public required IncidentAdditionalData AdditionalData { get; set; }

	/// <summary>
	/// Gets or sets the related analytic rule ids.
	/// </summary>
	[JsonPropertyName("relatedAnalyticRuleIds")]
	public required IReadOnlyCollection<string> RelatedAnalyticRuleIds { get; set; }

	/// <summary>
	/// Gets or sets the incident url.
	/// </summary>
	[JsonPropertyName("incidentUrl")]
	public required Uri IncidentUrl { get; set; }

	/// <summary>
	/// Gets or sets the provider name.
	/// </summary>
	[JsonPropertyName("providerName")]
	public required string ProviderName { get; set; }

	/// <summary>
	/// Gets or sets the provider incident id.
	/// </summary>
	[JsonPropertyName("providerIncidentId")]
	public required string ProviderIncidentId { get; set; }
}
