using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

public abstract class ErrorException : Exception
{
	public ErrorResponse ErrorResponse { get; }

	protected ErrorException(ErrorResponse errorResponse)
	{
		ErrorResponse = errorResponse;
	}

	protected ErrorException(string code)
	{
		ErrorResponse = new()
		{
			Error = new()
			{
				Code = code,
				Message = "The request is invalid."
			}
		};
	}

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

	protected ErrorException(string code, string message, Exception innerException) : base(message, innerException)
	{
		ErrorResponse = new()
		{
			Error = new()
			{
				Code = "BadRequest",
				Message = message
			}
		};
	}

}