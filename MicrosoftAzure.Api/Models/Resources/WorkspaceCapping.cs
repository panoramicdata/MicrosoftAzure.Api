using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Resources;

public class WorkspaceCapping
{
	[JsonPropertyName("dailyQuotaGb")]
	public required float DailyQuotaGb { get; set; }

	[JsonPropertyName("quotaNextResetTime")]
	public required DateTime QuotaNextResetTime { get; set; }

	[JsonPropertyName("dataIngestionStatus")]
	public required string DataIngestionStatus { get; set; }
}
