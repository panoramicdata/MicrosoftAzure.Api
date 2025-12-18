using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

public class UnauthorizedException : ErrorException
{
	private const string DefaultCode = "Unauthorized";

	public UnauthorizedException() : base()
	{
	}

	public UnauthorizedException(ErrorResponse errorResponse) : base(errorResponse)
	{
	}

	public UnauthorizedException(string message) : base(DefaultCode, message)
	{
	}

	public UnauthorizedException(string message, Exception innerException) : base(DefaultCode, message, innerException)
	{
	}
}