﻿using System;

using Azure.Identity;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace RH.Apps.Web.SPA.Server
{
	public static class Program
	{
		public static void Main(string[] args) 
			=> CreateHostBuilder(args).Build().Run();

		public static IHostBuilder CreateHostBuilder(string[] args) =>
		     Host
				.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((_, config) =>
				{
					var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
						config.AddAzureKeyVault(
						keyVaultEndpoint,
					new DefaultAzureCredential());
				})
				.ConfigureWebHostDefaults(webBuilder => _ = webBuilder.UseStartup<Startup>());
	}
}