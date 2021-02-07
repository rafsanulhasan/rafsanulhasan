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

			builder.Services.AddSingleton<IRuntimeService, AppSettingsService>();

			await builder.Build().RunAsync();
		}
	}
}
