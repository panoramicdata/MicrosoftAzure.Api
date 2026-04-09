using MicrosoftAzure.Api.Models.SecurityInsights;

namespace MicrosoftAzure.Api.Interfaces;

/// <summary>
/// Defines the contract for log analytics operations.
/// </summary>
public interface ILogAnalytics
{
	/// <summary>
	/// Executes the query operation.
	/// </summary>
	Task<QueryResponse> QueryAsync(
		Guid workspaceId,
		QueryRequest queryRequest,
		CancellationToken cancellationToken);
}
