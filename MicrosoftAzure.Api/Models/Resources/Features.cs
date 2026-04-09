namespace MicrosoftAzure.Api.Models.Resources;

/// <summary>
/// Represents the features.
/// </summary>
public class Features
{
	/// <summary>
	/// Gets or sets the legacy.
	/// </summary>
	public int legacy { get; set; }
	/// <summary>
	/// Gets or sets the search version.
	/// </summary>
	public int searchVersion { get; set; }
	/// <summary>
	/// Gets or sets a value indicating whether log access using only resource permissions is enabled.
	/// </summary>
	public bool enableLogAccessUsingOnlyResourcePermissions { get; set; }
}
