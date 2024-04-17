using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class Incident
{
	[JsonPropertyName("title")]
	public required string Title { get; set; }

	[JsonPropertyName("description")]
	public string? Description { get; set; }

	[JsonPropertyName("severity")]
	public required string Severity { get; set; }

	[JsonPropertyName("status")]
	public required string Status { get; set; }

	[JsonPropertyName("owner")]
	public required IncidentOwner Owner { get; set; }

	[JsonPropertyName("labels")]
	public required IReadOnlyCollection<string> Labels { get; set; }

	[JsonPropertyName("firstActivityTimeUtc")]
	public required DateTime FirstActivityTimeUtc { get; set; }

	[JsonPropertyName("lastActivityTimeUtc")]
	public required DateTime LastActivityTimeUtc { get; set; }

	[JsonPropertyName("lastModifiedTimeUtc")]
	public required DateTime LastModifiedTimeUtc { get; set; }

	[JsonPropertyName("createdTimeUtc")]
	public required DateTime CreatedTimeUtc { get; set; }

	[JsonPropertyName("incidentNumber")]
	public required int IncidentNumber { get; set; }

	[JsonPropertyName("additionalData")]
	public required IncidentAdditionalData AdditionalData { get; set; }

	[JsonPropertyName("relatedAnalyticRuleIds")]
	public required IReadOnlyCollection<string> RelatedAnalyticRuleIds { get; set; }

	[JsonPropertyName("incidentUrl")]
	public required Uri IncidentUrl { get; set; }

	[JsonPropertyName("providerName")]
	public required string ProviderName { get; set; }

	[JsonPropertyName("providerIncidentId")]
	public required string ProviderIncidentId { get; set; }
}