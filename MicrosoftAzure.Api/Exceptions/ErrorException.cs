using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

/// <summary>
/// Represents the error exception.
/// </summary>
public abstract class ErrorException : Exception
{
	/// <summary>
	/// Gets or sets the error response.
	/// </summary>
	public ErrorResponse ErrorResponse { get; }

	/// <summary>
	/// Initializes a new instance of the ErrorException class.
	/// </summary>
	protected ErrorException() : base()
	{
		ErrorResponse = new()
		{
			Error = new()
			{
				Code = "Unknown",
				Message = "An unknown error occurred."
			}
		};
	}

	/// <summary>
	/// Initializes a new instance of the ErrorException class.
	/// </summary>
	protected ErrorException(string message) : base(message)
	{
		ErrorResponse = new()
		{
			Error = new()
			{
				Code = "Unknown",
				Message = message
			}
		};
	}

	/// <summary>
	/// Initializes a new instance of the ErrorException class.
	/// </summary>
	protected ErrorException(string message, Exception innerException) : base(message, innerException)
	{
		ErrorResponse = new()
		{
			Error = new()
			{
				Code = "Unknown",
				Message = message
			}
		};
	}

	/// <summary>
	/// Initializes a new instance of the ErrorException class.
	/// </summary>
	protected ErrorException(ErrorResponse errorResponse)
	{
		ErrorResponse = errorResponse;
	}

	/// <summary>
	/// Initializes a new instance of the ErrorException class.
	/// </summary>
	protected ErrorException(string code, string message) : base(message)
	{
		ErrorResponse = new()
		{
			Error = new()
			{
				Code = code,
				Message = message
			}
		};
	}

	/// <summary>
	/// Initializes a new instance of the ErrorException class.
	/// </summary>
	protected ErrorException(string code, string message, Exception innerException) : base(message, innerException)
	{
		ErrorResponse = new()
		{
			Error = new()
			{
				Code = code,
				Message = message
			}
		};
	}

}
