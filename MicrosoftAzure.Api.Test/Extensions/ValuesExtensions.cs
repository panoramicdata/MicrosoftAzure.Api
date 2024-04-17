using MicrosoftAzure.Api.Models;

namespace MicrosoftAzure.Api.Test.Extensions;
internal static class ValuesExtensions
{
	internal static void CheckValues<T>(this Response<T> response)
	{
		response.Should().NotBeNull();
		response.Values.Should().NotBeNull();
		response.Values.Should().NotBeNullOrEmpty();
		response.Values.Should().NotContainNulls();
		response.Values.Should().OnlyContain(x => x.Id != null);
		response.Values.Should().OnlyContain(x => x.Name != null);
		response.Values.Should().OnlyContain(x => x.Etag != null);
		response.Values.Should().OnlyContain(x => x.Type != null);
		response.Values.Should().OnlyContain(x => x.Properties != null);
	}
}
