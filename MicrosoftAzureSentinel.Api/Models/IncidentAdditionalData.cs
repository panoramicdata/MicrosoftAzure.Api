using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class IncidentAdditionalData
{
	[JsonPropertyName("alertsCount")]
	public required int AlertsCount { get; set; }

	[JsonPropertyName("bookmarksCount")]
	public required int BookmarksCount { get; set; }

	[JsonPropertyName("commentsCount")]
	public required int CommentsCount { get; set; }

	[JsonPropertyName("alertProductNames")]
	public required IReadOnlyCollection<string> AlertProductNames { get; set; }

	[JsonPropertyName("tactics")]
	public required IReadOnlyCollection<string> Tactics { get; set; }

	[JsonPropertyName("techniques")]
	public required IReadOnlyCollection<string> Techniques { get; set; }

	[JsonPropertyName("providerIncidentUrl")]
	public Uri? ProviderIncidentUrl { get; set; }
}
