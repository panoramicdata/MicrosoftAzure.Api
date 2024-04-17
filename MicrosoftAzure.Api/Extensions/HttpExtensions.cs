using System.Text.Json;
using System.Text.Json.Serialization;

namespace MicrosoftAzureSentinel.Api.Extensions;

internal static class HttpExtensions
{
	internal static string ToDebugString(this HttpHeaders headers)
		=> string.Join("\n", headers.Select(h => $"{h.Key}={string.Join(", ", h.Value)}"));

	internal static async Task<string> ToDebugStringAsync(this HttpContent content)
	{
		if (content is null)
		{
			return "No content";
		}

		var contentString = await content
			.ReadAsStringAsync()
			.ConfigureAwait(false);

		return contentString.StartsWith('{')
			? FormatJson(contentString)
			: contentString;
	}

	private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
	{
		WriteIndented = true,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
	};

	private static string FormatJson(string json)
	{
		// Return formatted JSON using System.Text.Json
		var doc = JsonDocument.Parse(json);
		return JsonSerializer.Serialize(doc.RootElement, _jsonSerializerOptions);
	}
}
