using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RH.Apps.Web.SPA.Client
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			_ = builder.Build();

			var config = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
			var apiUrl = config["https://localhost:5050"];

			builder
				.Services.AddHttpClient(
					"RH.Apps.Web.SPA.ServerAPI",
					client => client.BaseAddress = new Uri(
											apiUrl ?? builder.HostEnvironment.BaseAddress
						))
			    .AddHttpMessageHandler<ApiAddressAuthorizationMessageHandler>();

			// Supply HttpClient instances that include access tokens when making requests to the server project
			builder.Services.AddScoped(sp
				=> sp
					.GetRequiredService<IHttpClientFactory>()
					.CreateClient("RH.Apps.Web.SPA.ServerAPI")
			);

			builder.Services.AddApiAuthorization(_ => { });

			var host = builder.Build();

			await host.RunAsync().ConfigureAwait(false);
		}
	}
}
