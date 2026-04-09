using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the alert rule.
/// </summary>
public class AlertRule
{
	/// <summary>
	/// Gets or sets the query frequency.
	/// </summary>
	[JsonPropertyName("queryFrequency")]
	public string? QueryFrequency { get; set; }

	/// <summary>
	/// Gets or sets the query period.
	/// </summary>
	[JsonPropertyName("queryPeriod")]
	public string? QueryPeriod { get; set; }

	/// <summary>
	/// Gets or sets the trigger operator.
	/// </summary>
	[JsonPropertyName("triggerOperator")]
	public string? TriggerOperator { get; set; }

	/// <summary>
	/// Gets or sets the trigger threshold.
	/// </summary>
	[JsonPropertyName("triggerThreshold")]
	public int? TriggerThreshold { get; set; }

	/// <summary>
	/// Gets or sets the event grouping settings.
	/// </summary>
	[JsonPropertyName("eventGroupingSettings")]
	public Settings? EventGroupingSettings { get; set; }

	/// <summary>
	/// Gets or sets the incident configuration.
	/// </summary>
	[JsonPropertyName("incidentConfiguration")]
	public AlertRuleIncidentConfig? IncidentConfiguration { get; set; }

	/// <summary>
	/// Gets or sets the entity mappings.
	/// </summary>
	[JsonPropertyName("entityMappings")]
	public IReadOnlyCollection<EntityMapping>? EntityMappings { get; set; }

	/// <summary>
	/// Gets or sets the severity.
	/// </summary>
	[JsonPropertyName("severity")]
	public string? Severity { get; set; }

	/// <summary>
	/// Gets or sets the query.
	/// </summary>
	[JsonPropertyName("query")]
	public string? Query { get; set; }

	/// <summary>
	/// Gets or sets the suppression duration.
	/// </summary>
	[JsonPropertyName("suppressionDuration")]
	public string? SuppressionDuration { get; set; }

	/// <summary>
	/// Gets or sets the suppression enabled.
	/// </summary>
	[JsonPropertyName("suppressionEnabled")]
	public bool? SuppressionEnabled { get; set; }

	/// <summary>
	/// Gets or sets the tactics.
	/// </summary>
	[JsonPropertyName("tactics")]
	public IReadOnlyCollection<string>? Tactics { get; set; }

	/// <summary>
	/// Gets or sets the techniques.
	/// </summary>
	[JsonPropertyName("techniques")]
	public IReadOnlyCollection<string>? Techniques { get; set; }

	/// <summary>
	/// Gets or sets the display name.
	/// </summary>
	[JsonPropertyName("displayName")]
	public required string DisplayName { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether d is enabled.
	/// </summary>
	[JsonPropertyName("enabled")]
	public required bool Enabled { get; set; }

	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	[JsonPropertyName("description")]
	public required string Description { get; set; }

	/// <summary>
	/// Gets or sets the alert rule template name.
	/// </summary>
	[JsonPropertyName("alertRuleTemplateName")]
	public required string AlertRuleTemplateName { get; set; }

	/// <summary>
	/// Gets or sets the last modified utc.
	/// </summary>
	[JsonPropertyName("lastModifiedUtc")]
	public required DateTime LastModifiedUtc { get; set; }

	/// <summary>
	/// Gets or sets the template version.
	/// </summary>
	[JsonPropertyName("templateVersion")]
	public string? TemplateVersion { get; set; }

	/// <summary>
	/// Gets or sets the custom details.
	/// </summary>
	[JsonPropertyName("customDetails")]
	public AlertRuleCustomDetails? CustomDetails { get; set; }
}
