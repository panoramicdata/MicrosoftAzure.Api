﻿using MicrosoftAzure.Api.Models;
using MicrosoftAzure.Api.Models.Resources;
using Refit;

namespace MicrosoftAzure.Api.Interfaces;

public interface IResourceGroups
{
	[Get("/subscriptions/{subscriptionId}/resourcegroups?api-version=2024-03-01")]
	Task<PlainResponse<ResourceGroup>> GetAsync(
		Guid subscriptionId,
		CancellationToken cancellationToken);
}
