﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RH.Apps.Web.SPA.Lite.Extensions
{
	public static class KestrelServerOptionsExtensions
	{
		public static void ConfigureEndpoints(this KestrelServerOptions options)
		{
			if (options is null)
			{
				return;
			}

			var configuration = options.ApplicationServices.GetRequiredService<IConfiguration>();
			var environment = options.ApplicationServices.GetRequiredService<IHostEnvironment>();

			var endpoints = configuration.GetSection("HttpServer:Endpoints")
			    .GetChildren()
			    .ToDictionary(
			    		section => section.Key,
			    		section =>
			    		{
			    			var endpoint = new EndpointConfiguration();
			    			section.Bind(endpoint);
			    			return endpoint;
			    		}
				);

			foreach (var endpoint in endpoints)
			{
				var config = endpoint.Value;
				var port = config.Port ?? (config.Scheme == "https" ? 443 : 80);

				var ipAddresses = new List<IPAddress>();
				if (config.Host == "localhost")
				{
					ipAddresses.Add(IPAddress.IPv6Loopback);
					ipAddresses.Add(IPAddress.Loopback);
				}
				else if (IPAddress.TryParse(config.Host, out var address))
				{
					ipAddresses.Add(address);
				}
				else
				{
					ipAddresses.Add(IPAddress.IPv6Any);
				}

				foreach (var address in ipAddresses)
				{
					options.Listen(
						address,
						port,
						listenOptions =>
						{
							if (config.Scheme == "https")
							{
								var certificate = LoadCertificate(config);
								listenOptions.UseHttps(certificate);
							}
						});
				}
			}
		}

		private static X509Certificate2 LoadCertificate(EndpointConfiguration config)
		{
			if (config.StoreName is not null && config.StoreLocation is not null)
			{
				using (var store = new X509Store(
					config!.StoreName,
					Enum.Parse<StoreLocation>(config.StoreLocation))
				)
				{
					store.Open(OpenFlags.ReadOnly);
					var certificate = store.Certificates.Find(
						X509FindType.FindBySubjectName,
						config.Host,
						validOnly: true // !environment.IsDevelopment()
					);

					return certificate.Count == 0
						? new X509Certificate2(config.FilePath, config.Password)
						: certificate[0];
				}
			}

			return config.FilePath is not null && config.Password is not null
			    ? new X509Certificate2(config.FilePath, config.Password)
			    : throw new InvalidOperationException("No valid certificate configuration found for the current endpoint.");
		}
	}
}
