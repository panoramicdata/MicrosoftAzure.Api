using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the incident additional data.
/// </summary>
public class IncidentAdditionalData
{
	/// <summary>
	/// Gets or sets the alerts count.
	/// </summary>
	[JsonPropertyName("alertsCount")]
	public required int AlertsCount { get; set; }

	/// <summary>
	/// Gets or sets the bookmarks count.
	/// </summary>
	[JsonPropertyName("bookmarksCount")]
	public required int BookmarksCount { get; set; }

	/// <summary>
	/// Gets or sets the comments count.
	/// </summary>
	[JsonPropertyName("commentsCount")]
	public required int CommentsCount { get; set; }

	/// <summary>
	/// Gets or sets the alert product names.
	/// </summary>
	[JsonPropertyName("alertProductNames")]
	public required IReadOnlyCollection<string> AlertProductNames { get; set; }

	/// <summary>
	/// Gets or sets the tactics.
	/// </summary>
	[JsonPropertyName("tactics")]
	public required IReadOnlyCollection<string> Tactics { get; set; }

	/// <summary>
	/// Gets or sets the techniques.
	/// </summary>
	[JsonPropertyName("techniques")]
	public required IReadOnlyCollection<string> Techniques { get; set; }

	/// <summary>
	/// Gets or sets the provider incident url.
	/// </summary>
	[JsonPropertyName("providerIncidentUrl")]
	public Uri? ProviderIncidentUrl { get; set; }
}
