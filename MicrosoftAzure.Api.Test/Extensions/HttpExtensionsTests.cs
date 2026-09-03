using MicrosoftAzure.Api.Extensions;
using System.Net.Http.Headers;

namespace MicrosoftAzure.Api.Test.Extensions;

/// <summary>
/// Tests for header redaction in diagnostic output.
///
/// <para>
/// CustomHttpClientHandler sets an Authorization header carrying a bearer token on every request and
/// then logs the request headers at Debug. Any code path that renders headers into a log message
/// therefore writes a usable access token wherever those messages end up. These tests pin the
/// redaction that prevents it.
/// </para>
///
/// <para>
/// These are pure unit tests. They construct headers directly and require no credentials, no
/// configuration and no live subscription.
/// </para>
/// </summary>
public class HttpExtensionsTests
{
	/// <summary>
	/// Shaped like a real JWT so that a partial-redaction bug would be visible, but not a real token.
	/// </summary>
	private static readonly string FakeJwt = string.Join(
		'.',
		"eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9",
		"eyJzdWIiOiJ0ZXN0Iiwibm90IjoicmVhbCJ9",
		"c2lnbmF0dXJlLW5vdC1yZWFs");

	/// <summary>
	/// Renders a request carrying a single strongly typed Authorization header.
	/// </summary>
	private static string RequestDebugString(string scheme, string parameter)
	{
		using var request = new HttpRequestMessage();
		request.Headers.Authorization = new AuthenticationHeaderValue(scheme, parameter);

		return request.Headers.ToDebugString();
	}

	/// <summary>
	/// Renders a request carrying headers added without validation, so that the casing and the exact
	/// value reach the helper as the caller wrote them.
	/// </summary>
	private static string RequestDebugString(params (string Name, string Value)[] headers)
	{
		using var request = new HttpRequestMessage();
		foreach (var (name, value) in headers)
		{
			request.Headers.TryAddWithoutValidation(name, value);
		}

		return request.Headers.ToDebugString();
	}

	[Fact]
	public void ToDebugString_BearerToken_DoesNotLeakTheCredential()
	{
		var debugString = RequestDebugString("Bearer", FakeJwt);

		debugString.Should().NotContain(FakeJwt);
		debugString.Should().NotContain("c2lnbmF0dXJlLW5vdC1yZWFs");
		debugString.Should().NotContain("eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9");
	}

	[Fact]
	public void ToDebugString_BearerToken_KeepsTheSchemeAndLength()
		=> RequestDebugString("Bearer", FakeJwt)
			.Should().Be($"Authorization=Bearer <redacted, length {FakeJwt.Length}>");

	[Fact]
	public void ToDebugString_BasicScheme_KeepsTheSchemeAndRedactsTheCredential()
	{
		var debugString = RequestDebugString("Basic", "dXNlcjpwYXNzd29yZA==");

		debugString.Should().Be("Authorization=Basic <redacted, length 20>");
		debugString.Should().NotContain("dXNlcjpwYXNzd29yZA==");
	}

	/// <summary>
	/// A header added without validation keeps whatever casing the caller used, so redaction must not
	/// depend on the header name being canonically cased.
	/// </summary>
	[Theory]
	[InlineData("authorization")]
	[InlineData("AUTHORIZATION")]
	[InlineData("AuThOrIzAtIoN")]
	public void ToDebugString_AuthorizationHeader_IsRedactedWhateverTheCasing(string headerName)
	{
		// HttpHeaders canonicalises the name of a known header, so the rendered name is always
		// "Authorization" here whatever casing went in; only the redaction is under test.
		var debugString = RequestDebugString((headerName, $"Bearer {FakeJwt}"));

		debugString.Should().NotContain(FakeJwt);
		debugString.Should().Be($"Authorization=Bearer <redacted, length {FakeJwt.Length}>");
	}

	/// <summary>
	/// A vendor may prefix the standard header name rather than using it directly. An exact-match list
	/// alone would render such a header verbatim, so the suffix is matched too.
	/// </summary>
	[Theory]
	[InlineData("X-Samanage-Authorization")]
	[InlineData("X-Vendor-Authorization")]
	public void ToDebugString_VendorPrefixedAuthorizationHeader_IsRedacted(string headerName)
		=> RequestDebugString((headerName, $"Bearer {FakeJwt}"))
			.Should().Be($"{headerName}=Bearer <redacted, length {FakeJwt.Length}>");

	[Theory]
	[InlineData("Proxy-Authorization")]
	[InlineData("Cookie")]
	[InlineData("X-API-Key")]
	[InlineData("Api-Key")]
	[InlineData("X-Api-Token")]
	[InlineData("X-Auth-Token")]
	public void ToDebugString_OtherCredentialHeaders_AreRedacted(string headerName)
	{
		const string secret = "s3cr3t-value-that-must-not-be-logged";

		var debugString = RequestDebugString((headerName, secret));

		debugString.Should().NotContain(secret);
		debugString.Should().Contain("<redacted");
	}

	/// <summary>
	/// A credential with no scheme prefix has nothing safe to preserve, so all of it goes.
	/// </summary>
	[Fact]
	public void ToDebugString_CredentialWithoutAScheme_IsRedactedEntirely()
		=> RequestDebugString(("X-API-Key", "abcdef123456"))
			.Should().Be("X-API-Key=<redacted, length 12>");

	[Fact]
	public void ToDebugString_NonSensitiveHeader_IsUnchanged()
		=> RequestDebugString(("traceparent", "00-abc123-def456-00"))
			.Should().Be("traceparent=00-abc123-def456-00");

	/// <summary>
	/// Redaction must be surgical: the diagnostically useful headers alongside the credential are what
	/// make a log message worth reading, so they must survive intact.
	/// </summary>
	[Fact]
	public void ToDebugString_RedactsOnlyTheSensitiveHeader()
	{
		var debugString = RequestDebugString(
			("Authorization", $"Bearer {FakeJwt}"),
			("traceparent", "00-abc123-def456-00"),
			("Request-Id", "|abc.def."));

		debugString.Should().NotContain(FakeJwt);
		debugString.Should().Contain("traceparent=00-abc123-def456-00");
		debugString.Should().Contain("Request-Id=|abc.def.");
	}

	[Fact]
	public void ToDebugString_NoHeaders_IsEmpty()
		=> RequestDebugString().Should().BeEmpty();

	/// <summary>
	/// Response headers go through the same helper, so Set-Cookie is covered too.
	/// </summary>
	[Fact]
	public void ToDebugString_ResponseSetCookie_IsRedacted()
	{
		using var response = new HttpResponseMessage();
		response.Headers.TryAddWithoutValidation("Set-Cookie", "session=abc123def456; HttpOnly");

		var debugString = response.Headers.ToDebugString();

		debugString.Should().NotContain("abc123def456");
		debugString.Should().Contain("<redacted");
	}

	/// <summary>
	/// A cookie value also contains a space, so treating the text before the first space as a scheme
	/// would preserve the very value being redacted. Only Authorization style headers keep a scheme.
	/// </summary>
	[Fact]
	public void ToDebugString_CookieValueContainingASpace_IsRedactedWhole()
	{
		const string cookie = "session=abc123def456; HttpOnly";

		var debugString = RequestDebugString(("Cookie", cookie));

		debugString.Should().Be($"Cookie=<redacted, length {cookie.Length}>");
		debugString.Should().NotContain("session");
	}
}
