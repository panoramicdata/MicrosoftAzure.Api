using MicrosoftAzure.Api.Interfaces;
using MicrosoftAzure.Api.Test.Support;
using Refit;
using System.Net;

namespace MicrosoftAzure.Api.Test.Requests;

public class RefitRequestTests
{
	private const string Management = "https://management.azure.com/";
	private const string Subscription = "55555555-5555-5555-5555-555555555555";
	private const string Workspace = $"/subscriptions/{Subscription}/resourceGroups/rg/providers/Microsoft.OperationalInsights/workspaces";
	private const string SecurityInsights = $"{Workspace}/ws/providers/Microsoft.SecurityInsights";
	private static readonly Guid SubscriptionId = new(Subscription);

	private static async Task<RecordedRequest> SendAsync<T>(string baseAddress, string responseJson, Func<T, Task> call)
	{
		using var handler = StubHttpHandler.Always(HttpStatusCode.OK, responseJson);
		using var httpClient = new HttpClient(handler) { BaseAddress = new Uri(baseAddress) };

		await call(RestService.For<T>(httpClient)).ConfigureAwait(false);

		return handler.Requests.Should().ContainSingle().Subject;
	}

	private static Task<RecordedRequest> SendListAsync<T>(string baseAddress, Func<T, Task> call)
		=> SendAsync(baseAddress, """{"value":[]}""", call);

	private static CancellationToken Token => TestContext.Current.CancellationToken;

	private static async Task AssertGetAsync<T>(string baseAddress, string expectedPathAndQuery, Func<T, Task> call)
	{
		var request = await SendListAsync(baseAddress, call).ConfigureAwait(false);

		request.Method.Should().Be(HttpMethod.Get);
		request.Uri.PathAndQuery.Should().Be(expectedPathAndQuery);
	}

	[Fact]
	public Task Subscriptions_GetAsync_RequestsSubscriptionList()
		=> AssertGetAsync<ISubscriptions>(Management, "/subscriptions?api-version=2016-06-01", c => c.GetAsync(Token));

	[Fact]
	public Task Tenants_GetTenantsAsync_RequestsTenantList()
		=> AssertGetAsync<ITenants>(Management, "/tenants?api-version=2022-12-01", c => c.GetTenantsAsync(Token));

	[Fact]
	public Task ManagedTenants_GetAsync_RequestsGraphManagedTenants()
		=> AssertGetAsync<IManagedTenants>(
			"https://graph.microsoft.com/",
			"/beta/tenantRelationships/managedTenants/tenants",
			c => c.GetAsync(Token));

	[Fact]
	public Task ResourceGroups_GetAsync_RequestsResourceGroupsOfTheSubscription()
		=> AssertGetAsync<IResourceGroups>(
			Management,
			$"/subscriptions/{Subscription}/resourcegroups?api-version=2024-03-01",
			c => c.GetAsync(SubscriptionId, Token));

	[Fact]
	public Task Resources_GetAsync_RequestsResourcesOfTheSubscription()
		=> AssertGetAsync<IResources>(
			Management,
			$"/subscriptions/{Subscription}/resources?api-version=2024-03-01",
			c => c.GetAsync(SubscriptionId, Token));

	[Fact]
	public Task Resources_GetAsync_WithFilter_AddsTheFilterQuery()
		=> AssertGetAsync<IResources>(
			Management,
			$"/subscriptions/{Subscription}/resources?api-version=2024-03-01&%24filter=type%20eq%20%27x%27",
			c => c.GetAsync(SubscriptionId, "type eq 'x'", Token));

	[Fact]
	public Task Resources_GetAsync_WithAllOptions_AddsEachQueryParameter()
		=> AssertGetAsync<IResources>(
			Management,
			$"/subscriptions/{Subscription}/resources?api-version=2024-03-01&%24filter=f&%24expand=e&%24skip=2&%24take=3",
			c => c.GetAsync(SubscriptionId, "f", "e", 2, 3, Token));

	[Fact]
	public Task Resources_GetAsync_WithOmittedOptions_SendsNoOptionalParameters()
		=> AssertGetAsync<IResources>(
			Management,
			$"/subscriptions/{Subscription}/resources?api-version=2024-03-01",
			c => c.GetAsync(SubscriptionId, null, null, null, null, Token));

	[Fact]
	public async Task Resources_GetPropertiesAsync_RequestsTheWorkspaceResource()
	{
		const string json = """
			{"properties":{"customerId":"66666666-6666-6666-6666-666666666666","provisioningState":"Succeeded",
			"sku":{"name":"PerGB2018"},"retentionInDays":30,"features":{},"workspaceCapping":{"dailyQuotaGb":1.5,"quotaNextResetTime":"2024-01-03T00:00:00Z","dataIngestionStatus":"RespectQuota"},
			"publicNetworkAccessForIngestion":"Enabled","publicNetworkAccessForQuery":"Enabled","createdDate":"2024-01-02T03:04:05Z","modifiedDate":"2024-01-02T03:04:05Z"},
			"location":"uksouth","id":"i","name":"ws","type":"t","etag":"e"}
			""";

		var request = await SendAsync<IResources>(
			Management,
			json,
			c => c.GetPropertiesAsync(SubscriptionId, "rg", "Microsoft.OperationalInsights", "ws", Token)).ConfigureAwait(true);

		request.Uri.PathAndQuery.Should().Be($"{Workspace}/ws?api-version=2023-09-01");
	}

	[Fact]
	public async Task Sentinel_Endpoints_RequestTheSecurityInsightsResources()
	{
		var calls = new (string PathAndQuery, Func<ISentinel, Task> Call)[]
		{
			($"{SecurityInsights}/dataConnectors?api-version=2024-03-01", c => c.GetDataConnectorsAsync(SubscriptionId, "rg", "ws", Token)),
			($"{SecurityInsights}/alertRules?api-version=2024-03-01", c => c.GetAlertRulesAsync(SubscriptionId, "rg", "ws", Token)),
			($"{SecurityInsights}/incidents?api-version=2024-03-01", c => c.GetIncidentsAsync(SubscriptionId, "rg", "ws", Token)),
			($"{SecurityInsights}/threatIntelligence/main/indicators?api-version=2024-03-01", c => c.GetThreatIndicatorsAsync(SubscriptionId, "rg", "ws", Token)),
			($"{SecurityInsights}/threatIntelligence/main/metrics?api-version=2024-03-01", c => c.GetThreatIndicatorMetricsAsync(SubscriptionId, "rg", "ws", Token)),
			($"{Workspace}?api-version=2024-03-01", c => c.GetWorkspacesAsync(SubscriptionId, "rg", Token)),
		};

		foreach (var (pathAndQuery, call) in calls)
		{
			await AssertGetAsync(Management, pathAndQuery, call).ConfigureAwait(true);
		}
	}
}
