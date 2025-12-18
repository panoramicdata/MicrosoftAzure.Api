using MicrosoftAzure.Api.Extensions;
using MicrosoftAzure.Api.Models.Responses;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;

namespace MicrosoftAzure.Api;

internal sealed class CustomHttpClientHandler : HttpClientHandler
{
	private readonly MicrosoftAzureClientOptions _options;
	private readonly Uri _resource;

	private BearerToken? _bearerToken;

	public CustomHttpClientHandler(MicrosoftAzureClientOptions options, Uri baseAddress)
	{
		_options = options;
		_resource = new(baseAddress.GetLeftPart(UriPartial.Authority));
		CheckCertificateRevocationList = true;
	}

	protected override async Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken)
	{
		var bearerToken = await GetBearerTokenAsync(cancellationToken)
			.ConfigureAwait(false);

		request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken.AccessToken);
		request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		if (_options.Logger.IsEnabled(LogLevel.Debug))
		{
			_options.Logger.LogDebug(
			"Sending {RequestMethod} {RequestUri}\n{Headers}\n{Content}",
			request.Method,
			request.RequestUri,
			request.Headers.ToDebugString(),
			await (request.Content?.ToDebugStringAsync() ?? Task.FromResult(string.Empty)).ConfigureAwait(false)
			);
		}

		var responseMessage = await base
			.SendAsync(request, cancellationToken)
			.ConfigureAwait(false);

		// Handle bad requests
		if (!responseMessage.IsSuccessStatusCode)
		{
			await HandleNonSuccessStatusCodesAsync(responseMessage, cancellationToken)
				.ConfigureAwait(false);
		}

		await LogResponseAsync(_options, responseMessage)
			.ConfigureAwait(false);

		return responseMessage;
	}

	private static async Task HandleNonSuccessStatusCodesAsync(
		HttpResponseMessage responseMessage,
		CancellationToken cancellationToken)
	{
		switch (responseMessage.StatusCode)
		{
			case HttpStatusCode.Unauthorized:
				throw new UnauthorizedException(await GetErrorResponseAsync(responseMessage, cancellationToken).ConfigureAwait(false));
			case HttpStatusCode.BadRequest:
				throw new BadRequestException(await GetErrorResponseAsync(responseMessage, cancellationToken).ConfigureAwait(false));
			case HttpStatusCode.Forbidden:
				throw new ForbiddenException(await GetErrorResponseAsync(responseMessage, cancellationToken).ConfigureAwait(false));
			case HttpStatusCode.NotFound:
				throw new NotFoundException(await GetErrorResponseAsync(responseMessage, cancellationToken).ConfigureAwait(false));
		}
	}

	private static async Task<ErrorResponse> GetErrorResponseAsync(HttpResponseMessage responseMessage, CancellationToken cancellationToken)
		=> (await responseMessage
			.Content
			   .ReadFromJsonAsync<ErrorResponse>(cancellationToken).ConfigureAwait(false)
		) ?? throw new FormatException("API returned an invalid error response.");

	private async Task<BearerToken> GetBearerTokenAsync(CancellationToken cancellationToken)
	{
		if (_bearerToken is not null && !_bearerToken.IsExpired)
		{
			return _bearerToken;
		}

		using var authHandler = new HttpClientHandler
		{
			CheckCertificateRevocationList = true
		};
		using var authHttpClient = new HttpClient(authHandler)
		{
			BaseAddress = new Uri($"https://login.microsoftonline.com/{_options.TenantId}/oauth2/token")
		};

		using var authRequest = new HttpRequestMessage(HttpMethod.Post, "")
		{

			Content = new StringContent(
				$"grant_type=client_credentials&client_id={_options.ClientId}&client_secret={_options.ClientSecret}&resource={_resource}",
				Encoding.UTF8,
				"application/x-www-form-urlencoded")
		};
		var response = await authHttpClient
			.SendAsync(authRequest, cancellationToken)
			.ConfigureAwait(false);

		if (!response.IsSuccessStatusCode)
		{
			throw new InvalidOperationException($"Unable to fetch the access token. {response.StatusCode} {await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false)}");
		}

		var bearerTokenResponse = await response
			.Content
			.ReadFromJsonAsync<BearerTokenResponse>(cancellationToken)
			.ConfigureAwait(false)
			?? throw new InvalidOperationException("Unable to fetch the access token.");

		return _bearerToken = new BearerToken
		{
			AccessToken = bearerTokenResponse.AccessToken,
			ExpiryDateTimeUtc = DateTime.UtcNow + TimeSpan.FromSeconds(Math.Max(0, int.Parse(bearerTokenResponse.ExpiresIn, CultureInfo.InvariantCulture) - 10))
		};
	}


	private static async Task LogResponseAsync(
		MicrosoftAzureClientOptions options,
		HttpResponseMessage responseMessage)
	{
		if (!options.Logger.IsEnabled(LogLevel.Debug))
		{
			return;
		}

		options.Logger.LogDebug(
			"Received {StatusCode}\n{Headers}\n{ResponseBody}",
			responseMessage.StatusCode,
			responseMessage.Headers.ToDebugString(),
			await responseMessage
				.Content
				.ToDebugStringAsync()
				.ConfigureAwait(false)
			);
	}
}