using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

public class IncidentOwner
{
	[JsonPropertyName("objectId")]
	public required object ObjectId { get; set; }

	[JsonPropertyName("email")]
	public required object Email { get; set; }

	[JsonPropertyName("assignedTo")]
	public required object AssignedTo { get; set; }

	[JsonPropertyName("userPrincipalName")]
	public required object UserPrincipalName { get; set; }

	[JsonPropertyName("ownerType")]
	public required object OwnerType { get; set; }
}
