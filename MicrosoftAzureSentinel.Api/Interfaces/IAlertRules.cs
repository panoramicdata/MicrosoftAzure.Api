using MicrosoftAzureSentinel.Api.Models;
using Refit;

namespace MicrosoftAzureSentinel.Api.Interfaces;
public interface IAlertRules
{
	[Get("/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/alertRules?api-version=2024-03-01")]
	Task<Response<AlertRule>> GetAsync(
		Guid subscriptionId,
		string resourceGroupName,
		string workspaceName,
		CancellationToken cancellationToken);
}