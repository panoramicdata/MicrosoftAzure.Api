

using MicrosoftAzure.Api.Extensions;
using System.Globalization;
using System.Net.Http.Json;

namespace MicrosoftAzure.Api;

internal class CustomHttpClientHandler(MicrosoftAzureClientOptions options, Uri baseAddress) : HttpClientHandler
{
	private readonly MicrosoftAzureClientOptions _options = options;
	private readonly Uri _resource = new(baseAddress.GetLeftPart(UriPartial.Authority));

	private BearerToken? _bearerToken;

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

		await LogResponseAsync(_options, responseMessage)
			.ConfigureAwait(false);

		return responseMessage;
	}

	private async Task<BearerToken> GetBearerTokenAsync(CancellationToken cancellationToken)
	{
		if (_bearerToken is not null && !_bearerToken.IsExpired)
		{
			return _bearerToken;
		}

		using var authHttpClient = new HttpClient
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