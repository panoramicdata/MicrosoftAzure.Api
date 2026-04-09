using System.Text.Json.Serialization;

namespace MicrosoftAzure.Api.Models.Sentinel;

/// <summary>
/// Represents the incident owner.
/// </summary>
public class IncidentOwner
{
	/// <summary>
	/// Gets or sets the object id.
	/// </summary>
	[JsonPropertyName("objectId")]
	public required object ObjectId { get; set; }

	/// <summary>
	/// Gets or sets the email.
	/// </summary>
	[JsonPropertyName("email")]
	public required object Email { get; set; }

	/// <summary>
	/// Gets or sets the assigned to.
	/// </summary>
	[JsonPropertyName("assignedTo")]
	public required object AssignedTo { get; set; }

	/// <summary>
	/// Gets or sets the user principal name.
	/// </summary>
	[JsonPropertyName("userPrincipalName")]
	public required object UserPrincipalName { get; set; }

	/// <summary>
	/// Gets or sets the owner type.
	/// </summary>
	[JsonPropertyName("ownerType")]
	public required object OwnerType { get; set; }
}
