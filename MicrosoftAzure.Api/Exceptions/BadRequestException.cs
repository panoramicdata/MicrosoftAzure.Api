using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

public class BadRequestException : ErrorException
{
	private const string DefaultCode = "BadRequest";

	public BadRequestException() : base()
	{
	}

	public BadRequestException(ErrorResponse errorResponse) : base(errorResponse)
	{
	}

	public BadRequestException(string message) : base(DefaultCode, message)
	{
	}

	public BadRequestException(string message, Exception innerException) : base(DefaultCode, message, innerException)
	{
	}
}
