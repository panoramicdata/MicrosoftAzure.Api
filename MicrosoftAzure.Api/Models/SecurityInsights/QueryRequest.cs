namespace MicrosoftAzure.Api.Models.SecurityInsights;

/// <summary>
/// Represents the query request.
/// </summary>
public class QueryRequest
{
	/// <summary>
	/// Gets or sets the query.
	/// </summary>
	public required string Query { get; set; }
}
