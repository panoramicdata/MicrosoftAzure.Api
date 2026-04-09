using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

/// <summary>
/// Represents the workspace capping.
/// </summary>
public class WorkspaceCapping
{
	/// <summary>
	/// Gets or sets the daily quota gb.
	/// </summary>
	[JsonPropertyName("dailyQuotaGb")]
	public required float DailyQuotaGb { get; set; }

	/// <summary>
	/// Gets or sets the quota next reset time.
	/// </summary>
	[JsonPropertyName("quotaNextResetTime")]
	public required DateTime QuotaNextResetTime { get; set; }

	/// <summary>
	/// Gets or sets the data ingestion status.
	/// </summary>
	[JsonPropertyName("dataIngestionStatus")]
	public required string DataIngestionStatus { get; set; }
}
