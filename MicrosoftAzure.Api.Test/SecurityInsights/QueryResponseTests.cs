using MicrosoftAzure.Api.Models.SecurityInsights;
using MicrosoftAzure.Api.Test.Support;
using System.Net;
using System.Text.Json;

namespace MicrosoftAzure.Api.Test.SecurityInsights;

public class QueryResponseTests
{
	private const string SampleJson = """
		{"tables":[{"name":"PrimaryResult",
		"columns":[
			{"name":"c0","type":"datetime"},{"name":"c1","type":"datetimeoffset"},{"name":"c2","type":"guid"},
			{"name":"c3","type":"string"},{"name":"c4","type":"dynamic"},{"name":"c5","type":"long"},
			{"name":"c6","type":"int"},{"name":"c7","type":"short"},{"name":"c8","type":"byte"},
			{"name":"c9","type":"float"},{"name":"c10","type":"real"},{"name":"c11","type":"bool"},
			{"name":"c12","type":"bool"},{"name":"c13","type":"dynamic"},{"name":"c14","type":"dynamic"},
			{"name":"c15","type":"string"}],
		"rows":[[
			"2024-01-02T03:04:05Z","2024-01-02T03:04:05+01:00","33333333-3333-3333-3333-333333333333",
			"text","other",5000000000,7,8,9,1.5,2.25,true,false,{"a":1},[1,2],null]]}]}
		""";

	private static QueryResponse Deserialize(string json)
		=> JsonSerializer.Deserialize<QueryResponse>(json)!;

	[Fact]
	public void Sanitize_ConvertsEachJsonValueToItsColumnType()
	{
		var response = Deserialize(SampleJson);

		response.Sanitize();

		var row = response.Tables.Single().Rows.Single();
		row[0].Should().Be(new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc));
		row[1].Should().Be(new DateTimeOffset(2024, 1, 2, 3, 4, 5, TimeSpan.FromHours(1)));
		row[2].Should().Be(new Guid("33333333-3333-3333-3333-333333333333"));
		row[3].Should().Be("text");
		row[4].Should().Be("other");
		row[5].Should().Be(5_000_000_000L);
		row[6].Should().Be(7);
		row[7].Should().Be((short)8);
		row[8].Should().Be((byte)9);
		row[9].Should().Be(1.5f);
		row[10].Should().Be(2.25d);
		row[11].Should().Be(true);
		row[12].Should().Be(false);
		row[13].Should().BeOfType<JsonElement>().Which.GetProperty("a").GetInt32().Should().Be(1);
		row[14].Should().BeOfType<JsonElement>().Which.GetArrayLength().Should().Be(2);
		row[15].Should().BeNull();
	}

	[Fact]
	public void Sanitize_WithRawNonJsonValue_Throws()
	{
		var response = new QueryResponse
		{
			Tables =
			[
				new Table
				{
					Name = "t",
					Columns = [new Column { Name = "c", Type = "string" }],
					Rows = [new List<object?> { "already a string" }],
				},
			],
		};

		response.Invoking(r => r.Sanitize()).Should().Throw<InvalidOperationException>();
	}

	[Fact]
	public async Task LogAnalytics_QueryAsync_RequestsQueryEndpointAndSanitizesTheResult()
	{
		var workspaceId = new Guid("44444444-4444-4444-4444-444444444444");
		using var handler = StubHttpHandler.Always(HttpStatusCode.OK, SampleJson);
		using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://api.loganalytics.io/v1/workspaces/") };
		var logAnalytics = new LogAnalytics(httpClient);

		var response = await logAnalytics.QueryAsync(workspaceId, new QueryRequest { Query = "Heartbeat" }, TestContext.Current.CancellationToken).ConfigureAwait(true);

		var request = handler.Requests.Should().ContainSingle().Subject;
		request.Method.Should().Be(HttpMethod.Get);
		request.Uri.AbsoluteUri.Should().Be($"https://api.loganalytics.io/v1/workspaces/{workspaceId}/query?query=Heartbeat");
		response.Tables.Single().Rows.Single()[6].Should().Be(7);
	}

	[Fact]
	public async Task LogAnalytics_QueryAsync_WithEmptyBody_ThrowsFormatException()
	{
		using var handler = StubHttpHandler.Always(HttpStatusCode.OK, "null");
		using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://api.loganalytics.io/v1/workspaces/") };
		var logAnalytics = new LogAnalytics(httpClient);

		var act = () => logAnalytics.QueryAsync(Guid.NewGuid(), new QueryRequest { Query = "Heartbeat" }, TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<FormatException>().WithMessage("Could not deserialize QueryResponse.").ConfigureAwait(true);
	}

	[Fact]
	public async Task LogAnalytics_QueryAsync_WithNullRequest_Throws()
	{
		using var handler = StubHttpHandler.Always(HttpStatusCode.OK, "{}");
		using var httpClient = new HttpClient(handler);
		var logAnalytics = new LogAnalytics(httpClient);

		var act = () => logAnalytics.QueryAsync(Guid.NewGuid(), null!, TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(true);
	}
}
