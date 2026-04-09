using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

/// <summary>
/// Represents the forbidden exception.
/// </summary>
public class ForbiddenException : ErrorException
{
	private const string DefaultCode = "Forbidden";

	/// <summary>
	/// Initializes a new instance of the ForbiddenException class.
	/// </summary>
	public ForbiddenException() : base()
	{
	}

	/// <summary>
	/// Initializes a new instance of the ForbiddenException class.
	/// </summary>
	public ForbiddenException(ErrorResponse errorResponse) : base(errorResponse)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ForbiddenException class.
	/// </summary>
	public ForbiddenException(string message) : base(DefaultCode, message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the ForbiddenException class.
	/// </summary>
	public ForbiddenException(string message, Exception innerException) : base(DefaultCode, message, innerException)
	{
	}
}
