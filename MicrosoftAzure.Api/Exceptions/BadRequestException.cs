using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

/// <summary>
/// Represents the bad request exception.
/// </summary>
public class BadRequestException : ErrorException
{
	private const string DefaultCode = "BadRequest";

	/// <summary>
	/// Initializes a new instance of the BadRequestException class.
	/// </summary>
	public BadRequestException() : base()
	{
	}

	/// <summary>
	/// Initializes a new instance of the BadRequestException class.
	/// </summary>
	public BadRequestException(ErrorResponse errorResponse) : base(errorResponse)
	{
	}

	/// <summary>
	/// Initializes a new instance of the BadRequestException class.
	/// </summary>
	public BadRequestException(string message) : base(DefaultCode, message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the BadRequestException class.
	/// </summary>
	public BadRequestException(string message, Exception innerException) : base(DefaultCode, message, innerException)
	{
	}
}
