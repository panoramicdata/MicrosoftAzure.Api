using MicrosoftAzureSentinel.Api.Models;
using Refit;

namespace MicrosoftAzureSentinel.Api.Interfaces;

public interface IThreatIntelligenceIndicators
{
	[Get("/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/threatIntelligence/main/indicators?api-version=2024-03-01")]
	Task<Response<ThreatIntelligenceIndicator>> GetAsync(
		Guid subscriptionId,
		string resourceGroupName,
		string workspaceName,
		CancellationToken cancellationToken);
}
