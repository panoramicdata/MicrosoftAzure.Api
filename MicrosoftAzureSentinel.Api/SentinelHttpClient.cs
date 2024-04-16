
using MicrosoftAzureSentinel.Api.Extensions;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

namespace MicrosoftAzureSentinel.Api;

internal class SentinelHttpClient : HttpClient
{
	private readonly Uri _resource;
	private readonly MicrosoftAzureSentinelClientOptions _options;
	private BearerToken? _bearerToken;

	internal SentinelHttpClient(Uri baseAddress, MicrosoftAzureSentinelClientOptions options)
	{
		BaseAddress = baseAddress;

		// Get the root url from the Base address, e.g. for https://api.loganalytics.io/v1/workspaces/123456/ it will be https://api.loganalytics.io
		_resource = new Uri(baseAddress.GetLeftPart(UriPartial.Authority));
		_options = options;
	}

	internal async Task<T> SendAsync<T>(
		HttpMethod httpMethod,
		string path,
		object? entity,
		CancellationToken cancellationToken)
	{
		using var request = new HttpRequestMessage(httpMethod, path)
		{
			Content = entity is null
				? null
				: new StringContent(
					JsonSerializer.Serialize(entity),
					Encoding.UTF8,
					"application/json")
		};

		var bearerToken = await GetBearerTokenAsync(cancellationToken)
			.ConfigureAwait(false);

		request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken.AccessToken);

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

		request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		var responseMessage = await base
			.SendAsync(request, cancellationToken)
			.ConfigureAwait(false);

		await LogResponseAsync(_options, responseMessage)
			.ConfigureAwait(false);

		if (!responseMessage.IsSuccessStatusCode)
		{
			var responseBody = await responseMessage
				.Content
				.ReadAsStringAsync(cancellationToken)
				.ConfigureAwait(false);

			throw new InvalidOperationException(
				$"Path: {BaseAddress!}/{path} {responseMessage.StatusCode}\n" +
				$"Request Headers: {request.Headers}\n" +
				$"Request Body: {(entity is null ? null : JsonSerializer.Serialize(entity))}\n" +
				$"Response Headers: {responseMessage.Headers}\n" +
				$"Response Body: {responseBody}"
				);
		}

		responseMessage.EnsureSuccessStatusCode();

		var result = await responseMessage
			.Content
			.ReadFromJsonAsync<T>(cancellationToken)
			.ConfigureAwait(false) ?? throw new FormatException("Invalid response format");

		return result;
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
		MicrosoftAzureSentinelClientOptions options,
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