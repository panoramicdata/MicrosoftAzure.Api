using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

/// <summary>
/// Represents the not found exception.
/// </summary>
public class NotFoundException : ErrorException
{
	private const string DefaultCode = "NotFound";

	/// <summary>
	/// Initializes a new instance of the NotFoundException class.
	/// </summary>
	public NotFoundException() : base()
	{
	}

	/// <summary>
	/// Initializes a new instance of the NotFoundException class.
	/// </summary>
	public NotFoundException(ErrorResponse errorResponse) : base(errorResponse)
	{
	}

	/// <summary>
	/// Initializes a new instance of the NotFoundException class.
	/// </summary>
	public NotFoundException(string message) : base(DefaultCode, message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the NotFoundException class.
	/// </summary>
	public NotFoundException(string message, Exception innerException) : base(DefaultCode, message, innerException)
	{
	}
}
