using MicrosoftAzure.Api.Interfaces;
using Refit;

namespace MicrosoftAzure.Api;

/// <summary>
/// Represents the microsoft azure client.
/// </summary>
public class MicrosoftAzureClient : IDisposable
{
	private readonly CustomHttpClientHandler _logAnalyticsHandler;
	private readonly HttpClient _logAnalyticsHttpClient;
	private readonly CustomHttpClientHandler _managementHandler;
	private readonly HttpClient _managementHttpClient;
	private readonly CustomHttpClientHandler _graphHandler;
	private readonly HttpClient _graphHttpClient;
	private bool disposedValue;

	/// <summary>
	/// Initializes a new instance of the MicrosoftAzureClient class.
	/// </summary>
	public MicrosoftAzureClient(MicrosoftAzureClientOptions options)
	{
		ArgumentNullException.ThrowIfNull(options, nameof(options));
		options.Validate();

		var logAnalyticsBaseAddress = new Uri($"https://api.loganalytics.io/v1/workspaces/");
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
		ResourceGroups = RestService.For<IResourceGroups>(_managementHttpClient);
		Subscriptions = RestService.For<ISubscriptions>(_managementHttpClient);
		ManagedTenants = RestService.For<IManagedTenants>(_graphHttpClient);
		Tenants = RestService.For<ITenants>(_managementHttpClient);
	}

	// Can be null if WorkspaceId is not set
	/// <summary>
	/// Gets or sets the log analytics.
	/// </summary>
	public ILogAnalytics LogAnalytics { get; }

	/// <summary>
	/// Gets or sets the managed tenants.
	/// </summary>
	public IManagedTenants ManagedTenants { get; }

	/// <summary>
	/// Gets or sets the resource groups.
	/// </summary>
	public IResourceGroups ResourceGroups { get; }

	/// <summary>
	/// Gets or sets the resources.
	/// </summary>
	public IResources Resources { get; }

	/// <summary>
	/// Gets or sets the sentinel.
	/// </summary>
	public ISentinel Sentinel { get; }

	/// <summary>
	/// Gets or sets the subscriptions.
	/// </summary>
	public ISubscriptions Subscriptions { get; }

	/// <summary>
	/// Gets or sets the tenants.
	/// </summary>
	public ITenants Tenants { get; }

	/// <summary>
	/// Executes the dispose operation.
	/// </summary>
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

	/// <summary>
	/// Executes the dispose operation.
	/// </summary>
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
