using MicrosoftAzureSentinel.Api.Extensions;
using MicrosoftAzureSentinel.Api.Models;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

namespace MicrosoftAzureSentinel.Api;

public class MicrosoftAzureSentinelClient : IDisposable
{
	private static DateTime? _accessTokenExpiryDateTimeUtc;
	private readonly HttpClient _httpClient;
	private static string? _accessToken;
	private bool disposedValue;
	private readonly MicrosoftAzureSentinelClientOptions _options;

	public MicrosoftAzureSentinelClient(MicrosoftAzureSentinelClientOptions options)
	{
		ArgumentNullException.ThrowIfNull(options, nameof(options));
		options.Validate();

		_options = options;

		_httpClient = new HttpClient
		{
			BaseAddress = new Uri($"https://api.loganalytics.io/v1/workspaces/{options.WorkspaceId}/")
		};
	}

	/// <summary>
	/// Ensure the client has an access token, which can then be used in normal HttpClient requests i.e. not using the client
	/// </summary>
	/// <returns>The access token</returns>
	/// <exception cref="HttpRequestException"></exception>
	public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
	{
		if (_accessToken is null ||
			_accessTokenExpiryDateTimeUtc is not null && _accessTokenExpiryDateTimeUtc < DateTime.UtcNow
		)
		{
			await EnsureAccessTokenUpdatedAsync(cancellationToken).ConfigureAwait(false);
			return _accessToken ?? throw new HttpRequestException("Unable to fetch the access token.");
		}

		return _accessToken;
	}

	/// <summary>
	/// This permits updates using @odata.bind.   You will have to add a parameter for the namespace, like:
	/// - "@odata.type": "#Microsoft.Dynamics.CRM.incident"
	/// </summary>
	/// <param name="path"></param>
	/// <param name="entity"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>The resulting body, interpreted as a JObject</returns>
	public async Task<Guid> PostAsync(
		string path,
		object entity,
		CancellationToken cancellationToken)
	{
		var responseMessage = await SendAsync(HttpMethod.Post, path, entity, cancellationToken).ConfigureAwait(false);
		var createdEntityHeader = responseMessage
			.Headers
			.GetValues("OData-EntityId")
			.Single();
		var guidString = createdEntityHeader
			.Split('(')
			.Last()
			.TrimEnd(')');
		return new Guid(guidString);
	}

	/// <summary>
	/// This permits updates using @odata.bind.   You will have to add a parameter for the namespace, like:
	/// - "@odata.type": "#Microsoft.Dynamics.CRM.incident"
	/// </summary>
	/// <param name="path"></param>
	/// <param name="entity"></param>
	/// <param name="cancellationToken"></param>
	public async Task PatchAsync(
		string path,
		object entity,
		CancellationToken cancellationToken)
		=> _ = await SendAsync(new HttpMethod("PATCH"), path, entity, cancellationToken).ConfigureAwait(false);

	private async Task<HttpResponseMessage> SendAsync(
		HttpMethod httpMethod,
		string path,
		object entity,
		CancellationToken cancellationToken)
	{
		// Serialize the entity
		var requestBody = JsonSerializer.Serialize(entity);
		using var request = new HttpRequestMessage(httpMethod, path)
		{
			Content = new StringContent(requestBody,
				Encoding.UTF8,
				"application/json")
		};

		await UpdateRequestHeadersAndLog(request, cancellationToken)
			.ConfigureAwait(false);

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
				$"Request Body: {requestBody}\n" +
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
		if (options.Logger.IsEnabled(LogLevel.Debug))
		{
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

	private async Task UpdateRequestHeadersAndLog(
		HttpRequestMessage request,
		CancellationToken cancellationToken)
	{
		if (
			_accessToken is null
			|| _accessTokenExpiryDateTimeUtc is not null && _accessTokenExpiryDateTimeUtc < DateTime.UtcNow
		)
		{
			await EnsureAccessTokenUpdatedAsync(cancellationToken)
				.ConfigureAwait(false);
		}

		request.Headers.Add("Authorization", $"Bearer {_accessToken}");
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
	}

	private async Task EnsureAccessTokenUpdatedAsync(CancellationToken cancellationToken)
	{
		if (_accessTokenExpiryDateTimeUtc is not null && _accessTokenExpiryDateTimeUtc > DateTime.UtcNow)
		{
			return;
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

		var responseText = await response
			.Content
			.ReadAsStringAsync(cancellationToken)
			.ConfigureAwait(false);

		var bearerTokenResponse = JsonSerializer.Deserialize<BearerTokenResponse>(responseText)
			?? throw new InvalidOperationException("Unable to fetch the access token.");

		_accessToken = bearerTokenResponse.AccessToken;
		_accessTokenExpiryDateTimeUtc = DateTime.UtcNow + TimeSpan.FromSeconds(Math.Max(0, int.Parse(bearerTokenResponse.ExpiresIn, CultureInfo.InvariantCulture) - 10));

		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
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

	public async Task<QueryResponse> QueryAsync(string query, CancellationToken cancellationToken)
	{
		await EnsureAccessTokenUpdatedAsync(cancellationToken)
			.ConfigureAwait(false);

		var response = await _httpClient.GetAsync(new Uri($"query?{query}", UriKind.Relative), cancellationToken)
			.ConfigureAwait(false);
		response.EnsureSuccessStatusCode();
		return await response
			.Content
			.ReadFromJsonAsync<QueryResponse>(cancellationToken)
			.ConfigureAwait(false) ?? throw new FormatException("Invalid response format");
	}
}
