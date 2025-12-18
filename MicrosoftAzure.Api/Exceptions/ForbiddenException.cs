using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

public class ForbiddenException : ErrorException
{
	private const string DefaultCode = "Forbidden";

	public ForbiddenException() : base()
	{
	}

	public ForbiddenException(ErrorResponse errorResponse) : base(errorResponse)
	{
	}

	public ForbiddenException(string message) : base(DefaultCode, message)
	{
	}

	public ForbiddenException(string message, Exception innerException) : base(DefaultCode, message, innerException)
	{
	}
}