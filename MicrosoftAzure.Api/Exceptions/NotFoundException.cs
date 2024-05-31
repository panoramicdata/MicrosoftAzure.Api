using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

public class NotFoundException(ErrorResponse errorResponse) : ErrorException(errorResponse)
{
}
