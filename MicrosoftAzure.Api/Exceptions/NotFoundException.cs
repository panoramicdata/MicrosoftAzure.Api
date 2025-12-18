using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

public class NotFoundException : ErrorException
{
	private const string DefaultCode = "NotFound";

	public NotFoundException() : base()
	{
	}

	public NotFoundException(ErrorResponse errorResponse) : base(errorResponse)
	{
	}

	public NotFoundException(string message) : base(DefaultCode, message)
	{
	}

	public NotFoundException(string message, Exception innerException) : base(DefaultCode, message, innerException)
	{
	}
}
