using MicrosoftAzureSentinel.Api.Models;

namespace MicrosoftAzureSentinel.Api.Test;

public class IncidentTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task QueryAsync_SimpleQuery_Succeeds()
	{
		var result = await Client
			.QueryAsync(
				"query=union * | where TimeGenerated > ago(1h) | summarize count() by Type, TenantId",
				default
			)
			.ConfigureAwait(true);
		result.Should().BeOfType<QueryResponse>();
	}

	[Fact]
	public async Task QueryAsync_FullQuery_Succeeds()
	{
		var result = await Client
			.QueryAsync(
				"query=SigninLogs | take 5",
				default
			)
			.ConfigureAwait(true);
		result.Should().BeOfType<QueryResponse>();
	}
}
