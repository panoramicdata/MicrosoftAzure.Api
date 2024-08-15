namespace MicrosoftAzure.Api;

/// <summary>
/// Options
/// </summary>
public class MicrosoftAzureClientOptions
{
	/// <summary>
	/// The tenant id
	/// </summary>
	public required Guid TenantId { get; set; }

	/// <summary>
	/// The authentication Client ID
	/// </summary>
	public required Guid ClientId { get; set; }

	/// <summary>
	/// The authentication Client Secret
	/// </summary>
	public required string ClientSecret { get; set; }

	/// <summary>
	/// An optional logger
	/// </summary>
	public ILogger Logger { get; set; } = NullLogger.Instance;

	/// <summary>
	/// Validate the properties
	/// </summary>
	/// <exception cref="ConfigurationException">Thrown if the properties are invalid.</exception>
	public void Validate()
	{
		if (TenantId == Guid.Empty)
		{
			throw new ConfigurationException($"{nameof(TenantId)} must be set.");
		}

		if (ClientId == Guid.Empty)
		{
			throw new ConfigurationException($"{nameof(ClientId)} must be set.");
		}

		if (string.IsNullOrWhiteSpace(ClientSecret))
		{
			throw new ConfigurationException($"{nameof(ClientSecret)} must be set.");
		}
	}
}
