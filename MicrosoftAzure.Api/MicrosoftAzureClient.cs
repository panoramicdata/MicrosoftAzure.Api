using MicrosoftAzure.Api.Interfaces;
using Refit;

namespace MicrosoftAzure.Api;

public class MicrosoftAzureClient : IDisposable
{
	private readonly CustomHttpClientHandler _logAnalyticsHandler;
	private readonly HttpClient _logAnalyticsHttpClient;
	private readonly CustomHttpClientHandler _managementHandler;
	private readonly HttpClient _managementHttpClient;
	private readonly CustomHttpClientHandler _graphHandler;
	private readonly HttpClient _graphHttpClient;
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

		var graphBaseAddress = new Uri($"https://graph.microsoft.com/");
		_graphHandler = new CustomHttpClientHandler(options, graphBaseAddress);
		_graphHttpClient = new HttpClient(_graphHandler)
		{
			BaseAddress = graphBaseAddress
		};

		LogAnalytics = new LogAnalytics(_logAnalyticsHttpClient);
		Sentinel = RestService.For<ISentinel>(_managementHttpClient);
		Resources = RestService.For<IResources>(_managementHttpClient);
		Subscriptions = RestService.For<ISubscriptions>(_managementHttpClient);
		Tenants = RestService.For<ITenants>(_graphHttpClient);
	}

	public ILogAnalytics LogAnalytics { get; }

	public IResources Resources { get; }

	public ISentinel Sentinel { get; }

	public ISubscriptions Subscriptions { get; }

	public ITenants Tenants { get; }

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
				_graphHttpClient.Dispose();
				_graphHandler.Dispose();
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
