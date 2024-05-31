using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

public class ForbiddenException(ErrorResponse errorResponse) : ErrorException(errorResponse)
{
}