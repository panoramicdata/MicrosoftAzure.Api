using MicrosoftAzureSentinel.Api.Models;

namespace MicrosoftAzureSentinel.Api;

public class MicrosoftAzureSentinelClient : IDisposable
{
	private readonly SentinelHttpClient _logAnalyticsHttpClient;
	private readonly SentinelHttpClient _managementHttpClient;
	private bool disposedValue;

	public MicrosoftAzureSentinelClient(MicrosoftAzureSentinelClientOptions options)
	{
		ArgumentNullException.ThrowIfNull(options, nameof(options));
		options.Validate();

		_logAnalyticsHttpClient = new SentinelHttpClient(
			new Uri($"https://api.loganalytics.io/v1/workspaces/{options.WorkspaceId}/"),
			options);

		_managementHttpClient = new SentinelHttpClient(
			new Uri($"https://management.azure.com/"),
			options);
	}

	public async Task<DataConnectionsResponse> GetConnectorsAsync(
		Guid subscriptionId,
		string resourceGroupName,
		string workspaceName,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(resourceGroupName, nameof(resourceGroupName));
		ArgumentNullException.ThrowIfNull(workspaceName, nameof(workspaceName));

		return await GetDataAsync<DataConnectionsResponse>(subscriptionId, resourceGroupName, workspaceName, "dataConnectors", cancellationToken).ConfigureAwait(false);
	}

	public async Task<AlertRulesResponse> GetAlertRulesAsync(
		Guid subscriptionId,
		string resourceGroupName,
		string workspaceName,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(resourceGroupName, nameof(resourceGroupName));
		ArgumentNullException.ThrowIfNull(workspaceName, nameof(workspaceName));

		return await GetDataAsync<AlertRulesResponse>(subscriptionId, resourceGroupName, workspaceName, "alertRules", cancellationToken).ConfigureAwait(false);
	}

	private Task<T> GetDataAsync<T>(
		Guid subscriptionId,
		string resourceGroupName,
		string workspaceName,
		string dataType,
		CancellationToken cancellationToken)
		=> _managementHttpClient.SendAsync<T>(
			HttpMethod.Get,
			$"subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/{dataType}?api-version=2024-03-01",
			null,
			cancellationToken);

	public async Task<QueryResponse> QueryAsync(
		QueryRequest queryRequest,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(queryRequest, nameof(queryRequest));

		var response = await _logAnalyticsHttpClient.SendAsync<QueryResponse>(
			HttpMethod.Get,
			$"query?query={queryRequest.Query}",
			null,
			cancellationToken)
			.ConfigureAwait(false);

		response.Sanitize();

		return response;
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				_logAnalyticsHttpClient.Dispose();
				_managementHttpClient.Dispose();
			}

			disposedValue = true;
		}
	}

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
