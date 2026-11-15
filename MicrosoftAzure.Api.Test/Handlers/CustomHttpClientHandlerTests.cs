using MicrosoftAzure.Api.Exceptions;
using MicrosoftAzure.Api.Test.Support;
using System.Net;
using System.Text;

namespace MicrosoftAzure.Api.Test.Handlers;

public class CustomHttpClientHandlerTests
{
	private const string SecretToken = "tok-secret-value";
	private const string ErrorBody = """{"error":{"code":"Boom","message":"bad thing"}}""";
	private static readonly Uri BaseAddress = new("https://management.azure.com/");

	private static string TokenJson(string expiresIn = "3600")
		=> $$"""{"token_type":"Bearer","resource":"r","expires_in":"{{expiresIn}}","expires_on":"1","not_before":"1","ext_expires_in":"3600","access_token":"{{SecretToken}}"}""";

	private static Uri Relative(string path) => new(path, UriKind.Relative);

	private sealed class Harness : IDisposable
	{
		private readonly CustomHttpClientHandler _handler;

		public Harness(
			HttpStatusCode apiStatus,
			string apiBody,
			ILogger? logger = null,
			HttpStatusCode tokenStatus = HttpStatusCode.OK,
			string? tokenBody = null)
		{
			Inner = StubHttpHandler.Always(apiStatus, apiBody);
			Auth = StubHttpHandler.Always(tokenStatus, tokenBody ?? TokenJson());
			_handler = new CustomHttpClientHandler(TestOptions.Create(logger), BaseAddress, () => Inner, () => Auth);
			Client = new HttpClient(_handler) { BaseAddress = BaseAddress };
		}

		public StubHttpHandler Inner { get; }

		public StubHttpHandler Auth { get; }

		public HttpClient Client { get; }

		public void Dispose()
		{
			Client.Dispose();
			_handler.Dispose();
			Inner.Dispose();
			Auth.Dispose();
		}
	}

	[Fact]
	public async Task SendAsync_AddsBearerTokenToTheRequest()
	{
		using var harness = new Harness(HttpStatusCode.OK, "{}");

		using var response = await harness.Client.GetAsync(Relative("subscriptions"), TestContext.Current.CancellationToken).ConfigureAwait(true);

		response.StatusCode.Should().Be(HttpStatusCode.OK);
		harness.Inner.Requests.Should().ContainSingle().Which.Authorization.Should().Be($"Bearer {SecretToken}");
	}

	[Fact]
	public async Task SendAsync_RequestsTokenForTenantAndResource()
	{
		using var harness = new Harness(HttpStatusCode.OK, "{}");

		using var response = await harness.Client.GetAsync(Relative("subscriptions"), TestContext.Current.CancellationToken).ConfigureAwait(true);

		var tokenRequest = harness.Auth.Requests.Should().ContainSingle().Subject;
		tokenRequest.Method.Should().Be(HttpMethod.Post);
		tokenRequest.Uri.AbsoluteUri.Should().Be($"https://login.microsoftonline.com/{TestOptions.TenantId}/oauth2/token");
		tokenRequest.Body.Should().Contain("grant_type=client_credentials")
			.And.Contain($"client_id={TestOptions.ClientId}")
			.And.Contain("resource=https://management.azure.com/");
	}

	[Theory]
	[InlineData("3600", 1)]
	[InlineData("5", 2)]
	public async Task SendAsync_ReusesUnexpiredTokenOnly(string expiresIn, int expectedTokenRequests)
	{
		using var harness = new Harness(HttpStatusCode.OK, "{}", tokenBody: TokenJson(expiresIn));

		using var first = await harness.Client.GetAsync(Relative("a"), TestContext.Current.CancellationToken).ConfigureAwait(true);
		using var second = await harness.Client.GetAsync(Relative("b"), TestContext.Current.CancellationToken).ConfigureAwait(true);

		harness.Auth.Requests.Should().HaveCount(expectedTokenRequests);
		harness.Inner.Requests.Should().HaveCount(2);
	}

	[Theory]
	[InlineData(HttpStatusCode.Unauthorized, typeof(UnauthorizedException))]
	[InlineData(HttpStatusCode.BadRequest, typeof(BadRequestException))]
	[InlineData(HttpStatusCode.Forbidden, typeof(ForbiddenException))]
	[InlineData(HttpStatusCode.NotFound, typeof(NotFoundException))]
	public async Task SendAsync_MapsErrorStatusToTypedException(HttpStatusCode statusCode, Type expectedType)
	{
		using var harness = new Harness(statusCode, ErrorBody);

		var act = () => harness.Client.GetAsync(Relative("a"), TestContext.Current.CancellationToken);

		var exception = (await act.Should().ThrowAsync<ErrorException>().ConfigureAwait(true)).Which;
		exception.GetType().Should().Be(expectedType);
		exception.ErrorResponse.Error.Code.Should().Be("Boom");
		exception.ErrorResponse.Error.Message.Should().Be("bad thing");
	}

	[Theory]
	[InlineData(HttpStatusCode.InternalServerError)]
	[InlineData(HttpStatusCode.TooManyRequests)]
	public async Task SendAsync_LeavesOtherErrorStatusesToTheCaller(HttpStatusCode statusCode)
	{
		using var harness = new Harness(statusCode, ErrorBody);

		using var response = await harness.Client.GetAsync(Relative("a"), TestContext.Current.CancellationToken).ConfigureAwait(true);

		response.StatusCode.Should().Be(statusCode);
	}

	[Fact]
	public async Task SendAsync_WithUnreadableErrorBody_ThrowsFormatException()
	{
		using var harness = new Harness(HttpStatusCode.NotFound, "null");

		var act = () => harness.Client.GetAsync(Relative("a"), TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<FormatException>().WithMessage("API returned an invalid error response.").ConfigureAwait(true);
	}

	[Theory]
	[InlineData(HttpStatusCode.InternalServerError, "oops", "Unable to fetch the access token. InternalServerError oops")]
	[InlineData(HttpStatusCode.OK, "null", "Unable to fetch the access token.")]
	public async Task SendAsync_WhenTokenCannotBeFetched_Throws(HttpStatusCode tokenStatus, string tokenBody, string expectedMessage)
	{
		using var harness = new Harness(HttpStatusCode.OK, "{}", tokenStatus: tokenStatus, tokenBody: tokenBody);

		var act = () => harness.Client.GetAsync(Relative("a"), TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<InvalidOperationException>().WithMessage(expectedMessage).ConfigureAwait(true);
		harness.Inner.Requests.Should().BeEmpty();
	}

	[Fact]
	public async Task SendAsync_WithDebugLogging_LogsRequestAndResponseWithoutLeakingTheToken()
	{
		var logger = new ListLogger();
		using var harness = new Harness(HttpStatusCode.OK, """{"value":[]}""", logger);
		using var content = new StringContent("""{"query":"x"}""", Encoding.UTF8, "application/json");

		using var response = await harness.Client.PostAsync(Relative("query"), content, TestContext.Current.CancellationToken).ConfigureAwait(true);

		logger.Messages.Should().Contain(m => m.Contains("Sending POST https://management.azure.com/query", StringComparison.Ordinal));
		logger.Messages.Should().Contain(m => m.Contains("Received OK", StringComparison.Ordinal));
		logger.Messages.Should().NotContain(m => m.Contains(SecretToken, StringComparison.Ordinal));
	}

	[Fact]
	public void Constructor_WithDefaultTransport_VerifiesCertificateRevocation()
	{
		using var handler = new CustomHttpClientHandler(TestOptions.Create(), BaseAddress);

		handler.InnerHandler.Should().BeOfType<HttpClientHandler>()
			.Which.CheckCertificateRevocationList.Should().BeTrue();
	}
}
