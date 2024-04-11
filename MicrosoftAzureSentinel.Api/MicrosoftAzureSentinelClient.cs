using MicrosoftAzureSentinel.Api.Extensions;
using MicrosoftAzureSentinel.Api.Models;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

namespace MicrosoftAzureSentinel.Api;

public class MicrosoftAzureSentinelClient : IDisposable
{
	private readonly HttpClient _httpClient;
	private bool disposedValue;
	private readonly MicrosoftAzureSentinelClientOptions _options;
	private readonly int _optionsHash;
	private BearerToken? _bearerToken;

	public MicrosoftAzureSentinelClient(MicrosoftAzureSentinelClientOptions options)
	{
		ArgumentNullException.ThrowIfNull(options, nameof(options));
		options.Validate();

		_options = options;
		_optionsHash = options.GetHashCode();

		_httpClient = new HttpClient
		{
			BaseAddress = new Uri($"https://api.loganalytics.io/v1/workspaces/{options.WorkspaceId}/")
		};
	}

	private async Task<HttpResponseMessage> SendAsync(
		HttpMethod httpMethod,
		string path,
		object? entity,
		CancellationToken cancellationToken)
	{
		var content = entity is null ? null : JsonSerializer.Serialize(entity);
		using var request = new HttpRequestMessage(httpMethod, path)
		{
			Content = entity is null
				? null
				: new StringContent(
					content,
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

		var responseMessage = await _httpClient
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
				$"Path: {_httpClient.BaseAddress!}/{path} {responseMessage.StatusCode}\n" +
				$"Request Headers: {request.Headers}\n" +
				$"Request Body: {content}\n" +
				$"Response Headers: {responseMessage.Headers}\n" +
				$"Response Body: {responseBody}"
				);
		}

		return responseMessage;
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
				$"grant_type=client_credentials&client_id={_options.ClientId}&client_secret={_options.ClientSecret}&resource=https://api.loganalytics.io",
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

	public async Task<QueryResponse> QueryAsync(
		QueryRequest queryRequest,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(queryRequest, nameof(queryRequest));

		var response = await SendAsync(HttpMethod.Get, $"query?query={queryRequest.Query}", null, cancellationToken)
			.ConfigureAwait(false);
		response.EnsureSuccessStatusCode();

		var queryResponse = await response
			.Content
			.ReadFromJsonAsync<QueryResponse>(cancellationToken)
			.ConfigureAwait(false) ?? throw new FormatException("Invalid response format");

		queryResponse.Sanitize();

		return queryResponse;
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				_httpClient.Dispose();
			}

			disposedValue = true;
		}
	}

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
