using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Exceptions;

public class BadRequestException(ErrorResponse errorResponse) : ErrorException(errorResponse)
{
}
