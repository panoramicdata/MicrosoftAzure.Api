using MicrosoftAzure.Api.Interfaces;
using MicrosoftAzure.Api.Models.SecurityInsights;
using Refit;
using System.Net.Http.Json;

namespace MicrosoftAzure.Api;

public class MicrosoftAzureClient : IDisposable
{
	private readonly CustomHttpClientHandler _logAnalyticsHandler;
	private readonly HttpClient _logAnalyticsHttpClient;
	private readonly CustomHttpClientHandler _managementHandler;
	private readonly HttpClient _managementHttpClient;
	private bool disposedValue;

	public MicrosoftAzureClient(MicrosoftAzureClientOptions options)
	{
		ArgumentNullException.ThrowIfNull(options, nameof(options));
		options.Validate();

		var logAnalyticsBaseAddress = new Uri($"https://api.loganalytics.io/v1/workspaces/{options.WorkspaceId}/");
		_logAnalyticsHandler = new CustomHttpClientHandler(options, logAnalyticsBaseAddress);
		_logAnalyticsHttpClient = new HttpClient(_logAnalyticsHandler)
		{
			BaseAddress = logAnalyticsBaseAddress
		};

		var managementBaseAddress = new Uri($"https://management.azure.com/");
		_managementHandler = new CustomHttpClientHandler(options, managementBaseAddress);
		_managementHttpClient = new HttpClient(_managementHandler)
		{
			BaseAddress = managementBaseAddress
		};

		Sentinel = RestService.For<ISentinel>(_managementHttpClient);
		Resources = RestService.For<IResources>(_managementHttpClient);
		Subscriptions = RestService.For<ISubscriptions>(_managementHttpClient);
	}

	public async Task<QueryResponse> QueryAsync(
		QueryRequest queryRequest,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(queryRequest, nameof(queryRequest));

		var response = (await _logAnalyticsHttpClient.GetFromJsonAsync<QueryResponse>(
			$"query?query={queryRequest.Query}",
			cancellationToken)
			.ConfigureAwait(false))
			?? throw new FormatException($"Could not deserialise {typeof(QueryResponse).Name}.");

		response.Sanitize();

		return response;
	}
	public IResources Resources { get; }

	public ISentinel Sentinel { get; }

	public ISubscriptions Subscriptions { get; }

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				_logAnalyticsHttpClient.Dispose();
				_logAnalyticsHandler.Dispose();
				_managementHttpClient.Dispose();
				_managementHandler.Dispose();
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
