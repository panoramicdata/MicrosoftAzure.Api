using System.Text;
namespace MicrosoftAzure.Api.Test.Support;

internal sealed record RecordedRequest(HttpMethod Method, Uri Uri, string? Authorization, string? Body);

/// <summary>
/// Terminal handler double: records each request and answers with the supplied responder.
/// </summary>
internal sealed class StubHttpHandler(Func<RecordedRequest, HttpResponseMessage> responder) : HttpMessageHandler
{
	public List<RecordedRequest> Requests { get; } = [];

	public static StubHttpHandler Always(System.Net.HttpStatusCode statusCode, string json)
		=> new(_ => Json(statusCode, json));

	public static HttpResponseMessage Json(System.Net.HttpStatusCode statusCode, string json)
		=> new(statusCode) { Content = new StringContent(json, Encoding.UTF8, "application/json") };

	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var body = request.Content is null
			? null
			: await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
		var recorded = new RecordedRequest(request.Method, request.RequestUri!, request.Headers.Authorization?.ToString(), body);
		Requests.Add(recorded);
		return responder(recorded);
	}
}
