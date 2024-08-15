
using MicrosoftAzure.Api.Interfaces;
using MicrosoftAzure.Api.Models.SecurityInsights;
using System.Net.Http.Json;

namespace MicrosoftAzure.Api;

internal class LogAnalytics(HttpClient logAnalyticsHttpClient) : ILogAnalytics
{
	public async Task<QueryResponse> QueryAsync(
	Guid workspaceId,
	QueryRequest queryRequest,
	CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(queryRequest, nameof(queryRequest));

		var response = (await logAnalyticsHttpClient.GetFromJsonAsync<QueryResponse>(
			$"{workspaceId}/query?query={queryRequest.Query}",
			cancellationToken)
			.ConfigureAwait(false))
			?? throw new FormatException($"Could not deserialize {typeof(QueryResponse).Name}.");

		response.Sanitize();

		return response;
	}
}