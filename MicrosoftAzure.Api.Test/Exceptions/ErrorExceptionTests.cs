using MicrosoftAzure.Api.Exceptions;
using MicrosoftAzure.Api.Models.Responses;

namespace MicrosoftAzure.Api.Test.Exceptions;

public class ErrorExceptionTests
{
	private sealed class ProbeException : ErrorException
	{
		public ProbeException()
		{
		}

		public ProbeException(string message) : base(message)
		{
		}

		public ProbeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public ProbeException(string code, string message) : base(code, message)
		{
		}

		public ProbeException(string code, string message, Exception innerException) : base(code, message, innerException)
		{
		}
	}

	[Theory]
	[InlineData(typeof(BadRequestException), "BadRequest")]
	[InlineData(typeof(ForbiddenException), "Forbidden")]
	[InlineData(typeof(NotFoundException), "NotFound")]
	[InlineData(typeof(UnauthorizedException), "Unauthorized")]
	public void MessageConstructor_UsesTheStatusSpecificCode(Type type, string expectedCode)
	{
		var exception = (ErrorException)Activator.CreateInstance(type, "boom")!;

		exception.Message.Should().Be("boom");
		exception.ErrorResponse.Error.Code.Should().Be(expectedCode);
		exception.ErrorResponse.Error.Message.Should().Be("boom");
	}

	[Theory]
	[InlineData(typeof(BadRequestException), "BadRequest")]
	[InlineData(typeof(ForbiddenException), "Forbidden")]
	[InlineData(typeof(NotFoundException), "NotFound")]
	[InlineData(typeof(UnauthorizedException), "Unauthorized")]
	public void InnerExceptionConstructor_KeepsTheInnerExceptionAndCode(Type type, string expectedCode)
	{
		var inner = new InvalidOperationException("inner");

		var exception = (ErrorException)Activator.CreateInstance(type, "boom", inner)!;

		exception.InnerException.Should().BeSameAs(inner);
		exception.ErrorResponse.Error.Code.Should().Be(expectedCode);
	}

	[Theory]
	[InlineData(typeof(BadRequestException))]
	[InlineData(typeof(ForbiddenException))]
	[InlineData(typeof(NotFoundException))]
	[InlineData(typeof(UnauthorizedException))]
	public void ErrorResponseConstructor_KeepsTheSuppliedResponse(Type type)
	{
		var response = new ErrorResponse { Error = new ErrorResponseError { Code = "Custom", Message = "from the service" } };

		var exception = (ErrorException)Activator.CreateInstance(type, response)!;

		exception.ErrorResponse.Should().BeSameAs(response);
	}

	[Theory]
	[InlineData(typeof(BadRequestException))]
	[InlineData(typeof(ForbiddenException))]
	[InlineData(typeof(NotFoundException))]
	[InlineData(typeof(UnauthorizedException))]
	public void DefaultConstructor_ReportsAnUnknownError(Type type)
	{
		var exception = (ErrorException)Activator.CreateInstance(type)!;

		exception.ErrorResponse.Error.Code.Should().Be("Unknown");
		exception.ErrorResponse.Error.Message.Should().Be("An unknown error occurred.");
	}

	[Fact]
	public void BaseConstructors_AssignCodeAndMessage()
	{
		var inner = new InvalidOperationException("inner");

		new ProbeException().ErrorResponse.Error.Code.Should().Be("Unknown");
		new ProbeException("m").ErrorResponse.Error.Should().BeEquivalentTo(new { Code = "Unknown", Message = "m" });
		new ProbeException("m", inner).ErrorResponse.Error.Code.Should().Be("Unknown");
		new ProbeException("c", "m").ErrorResponse.Error.Should().BeEquivalentTo(new { Code = "c", Message = "m" });

		var full = new ProbeException("c", "m", inner);
		full.ErrorResponse.Error.Should().BeEquivalentTo(new { Code = "c", Message = "m" });
		full.InnerException.Should().BeSameAs(inner);
	}

	[Fact]
	public void ConfigurationException_Constructors_KeepMessageAndInnerException()
	{
		var inner = new InvalidOperationException("inner");

		new ConfigurationException().Message.Should().NotBeNullOrEmpty();
		new ConfigurationException("m").Message.Should().Be("m");
		new ConfigurationException("m", inner).InnerException.Should().BeSameAs(inner);
	}
}
