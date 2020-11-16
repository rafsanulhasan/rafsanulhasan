using System;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;

namespace RH.Apps.Web.SPA.Client
{
	//
	// Summary:
	//     A System.Net.Http.DelegatingHandler that attaches access tokens to outgoing System.Net.Http.HttpResponseMessage
	//     instances. Access tokens will only be added when the request URI is within the
	//     api URI.
	public class ApiAddressAuthorizationMessageHandler : BaseAddressAuthorizationMessageHandler
	{
		//
		// Summary:
		//     Initializes a new instance of Microsoft.AspNetCore.Components.WebAssembly.Authentication.BaseAddressAuthorizationMessageHandler.
		//
		// Parameters:
		//   provider:
		//     The Microsoft.AspNetCore.Components.WebAssembly.Authentication.IAccessTokenProvider
		//     to use for requesting tokens.
		//
		//   navigationManager:
		//     The Microsoft.AspNetCore.Components.NavigationManager used to compute the base
		//     address.
		public ApiAddressAuthorizationMessageHandler(
			IAccessTokenProvider provider,
			NavigationManager navigationManager,
			IConfiguration configuration
		)
			: base(provider, navigationManager)
			=> ConfigureHandler(new string[1]
			{
				new Uri(configuration["ApiUrl"]).ToString()
			});
	}
}
