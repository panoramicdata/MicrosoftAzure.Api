# MicrosoftAzure.Api

[![Nuget](https://img.shields.io/nuget/v/MicrosoftAzure.Api)](https://www.nuget.org/packages/MicrosoftAzure.Api/)
[![Nuget](https://img.shields.io/nuget/dt/MicrosoftAzure.Api)](https://www.nuget.org/packages/MicrosoftAzure.Api/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/7c55bd140e544652a4a8ed1a0ed9e729)](https://www.codacy.com?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=panoramicdata/MicrosoftAzureSentinel.Api&amp;utm_campaign=Badge_Grade)

A simple Microsoft Azure Sentinel API nuget package.

Makes calls to endpoints set out here:
* LogAnalytics Query API:
	o https://learn.microsoft.com/en-us/rest/api/loganalytics/query/get?view=rest-loganalytics-2022-10-27-preview&tabs=HTTP
* SecurityInsights API:
	o https://learn.microsoft.com/en-us/rest/api/securityinsights/operation-groups?view=rest-securityinsights-2024-03-01

## Example usage

```csharp
using MicrosoftAzure.Api;

var subscriptionId = new Guid("your-subscription-id");
var resourceGroupName = "your-resource-group-name";
var workspaceName = "your-workspace-name";

var client = new MicrosoftAzureSentinelClient(new MicrosoftAzureSentinelClientOptions
{
	TenantId = "your-tenant-id",
	ClientId = "your-client-id",
	ClientSecret = "your-client-secret"
});

var signInLogs = await Client
	.SecurityInsights
	.QueryAsync(
		new QueryRequest
		{
			Query = "SigninLogs | take 5"
		},
		default
	)
	.ConfigureAwait(true);

var connectors = await Client
	.Sentinel
	.GetConnectorsAsync(
		subscriptionId,
		resourceGroupName,
		workspaceName,
		default
	)
	.ConfigureAwait(true);
```
