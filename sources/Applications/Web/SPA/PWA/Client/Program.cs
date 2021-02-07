using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RH.Apps.Web.SPA.Shared;

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

			// Supply HttpClient instances that include access tokens when making requests to the server project
			builder.Services.AddSingleton<IRuntimeService, AppSettingsService>();

			//builder.Services.AddApiAuthorization();

			await builder.Build().RunAsync();
		}
	}
}
