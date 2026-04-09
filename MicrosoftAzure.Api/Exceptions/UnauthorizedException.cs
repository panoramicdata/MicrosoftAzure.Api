using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

/// <summary>
/// Represents the unauthorized exception.
/// </summary>
public class UnauthorizedException : ErrorException
{
	private const string DefaultCode = "Unauthorized";

	/// <summary>
	/// Initializes a new instance of the UnauthorizedException class.
	/// </summary>
	public UnauthorizedException() : base()
	{
	}

	/// <summary>
	/// Initializes a new instance of the UnauthorizedException class.
	/// </summary>
	public UnauthorizedException(ErrorResponse errorResponse) : base(errorResponse)
	{
	}

	/// <summary>
	/// Initializes a new instance of the UnauthorizedException class.
	/// </summary>
	public UnauthorizedException(string message) : base(DefaultCode, message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the UnauthorizedException class.
	/// </summary>
	public UnauthorizedException(string message, Exception innerException) : base(DefaultCode, message, innerException)
	{
	}
}
