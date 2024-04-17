using MicrosoftAzure.Api.Models.Sentinel;
using MicrosoftAzureSentinel.Api.Models;
using Refit;

namespace MicrosoftAzureSentinel.Api.Interfaces;

public interface ISentinel
{
	[Get("/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/dataConnectors?api-version=2024-03-01")]
	Task<Response<DataConnection>> GetConnectorsAsync(
		Guid subscriptionId,
		string resourceGroupName,
		string workspaceName,
		CancellationToken cancellationToken);

	[Get("/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/alertRules?api-version=2024-03-01")]
	Task<Response<AlertRule>> GetAlertRulesAsync(
		Guid subscriptionId,
		string resourceGroupName,
		string workspaceName,
		CancellationToken cancellationToken);

	[Get("/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/incidents?api-version=2024-03-01")]
	Task<Response<Incident>> GetIncidentsAsync(
		Guid subscriptionId,
		string resourceGroupName,
		string workspaceName,
		CancellationToken cancellationToken);

	[Get("/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/threatIntelligence/main/indicators?api-version=2024-03-01")]
	Task<Response<ThreatIntelligenceIndicator>> GetThreatIndicatorsAsync(
		Guid subscriptionId,
		string resourceGroupName,
		string workspaceName,
		CancellationToken cancellationToken);

	[Get("/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/threatIntelligence/main/metrics?api-version=2024-03-01")]
	Task<SimpleResponse<ThreatIntelligenceMetric>> GetThreatIndicatorMetricsAsync(
		Guid subscriptionId,
		string resourceGroupName,
		string workspaceName,
		CancellationToken cancellationToken);
}