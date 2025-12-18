// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: CLSCompliant(false)]
[assembly: SuppressMessage(
	"Performance",
	"CA1848:Use the LoggerMessage delegates",
	Justification = "Development effort not worth the performance gains",
	Scope = "namespaceanddescendants",
	Target = "~N:MicrosoftAzure.Api")
]
[assembly: SuppressMessage(
	"Security",
	"CA5399:HttpClient is created without enabling CheckCertificateRevocationList",
	Justification = "CheckCertificateRevocationList is enabled in CustomHttpClientHandler constructor",
	Scope = "member",
	Target = "~M:MicrosoftAzure.Api.MicrosoftAzureClient.#ctor(MicrosoftAzure.Api.MicrosoftAzureClientOptions)")
]
