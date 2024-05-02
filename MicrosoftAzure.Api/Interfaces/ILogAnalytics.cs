using MicrosoftAzure.Api.Models.SecurityInsights;

namespace MicrosoftAzure.Api.Interfaces;

public interface ILogAnalytics
{
	Task<QueryResponse> QueryAsync(
		QueryRequest queryRequest,
		CancellationToken cancellationToken);
}
