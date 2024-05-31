using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

public class UnauthorizedException(ErrorResponse errorResponse) : ErrorException(errorResponse)
{
}