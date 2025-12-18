using MicrosoftAzure.Api.Models.SecurityInsights;

namespace MicrosoftAzure.Api.Test.SecurityInsights;

public class QueryTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task QueryAsync_SimpleQuery_Succeeds()
	{
		var result = await Client
			.LogAnalytics
			.QueryAsync(
				TestConfig.WorkspaceId,
				new QueryRequest
				{
					Query = "union * | where TimeGenerated > ago(1h) | summarize count() by Type, TenantId"
				},
				CancellationToken);
		result.Should().BeOfType<QueryResponse>();
	}

	[Fact]
	public async Task QueryAsync_FullQuery_Succeeds()
	{
		var result = await Client
			.LogAnalytics
			.QueryAsync(
				TestConfig.WorkspaceId,
				new QueryRequest
				{
					Query = "SigninLogs | take 5"
				},
				CancellationToken);
		result.Should().BeOfType<QueryResponse>();
	}
}
