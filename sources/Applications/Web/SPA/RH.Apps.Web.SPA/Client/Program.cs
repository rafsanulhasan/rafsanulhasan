using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RH.Apps.Web.SPA.Client
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			//builder.RootComponents.Add<App>("#app");

			builder.Services
				.AddHttpClient("RH.Apps.Web.SPA.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
				//.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
				;

			// Supply HttpClient instances that include access tokens when making requests to the server project
			builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("RH.Apps.Web.SPA.ServerAPI"));

			//builder.Services.AddApiAuthorization();

			await builder.Build().RunAsync();
		}
	}
}
