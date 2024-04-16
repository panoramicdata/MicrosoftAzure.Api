using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Models;

public class AlertRulesResponse
{
	[JsonPropertyName("value")]
	public required IReadOnlyCollection<AlertRuleResponseValue> Value { get; set; }
}
