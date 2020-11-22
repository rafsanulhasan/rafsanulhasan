
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RH.Apps.Web.SPA.Lite.Data;

namespace RH.Apps.Web.SPA.Lite
{
	public class Startup
	{
		private readonly IHostEnvironment _hostEnvironment;
		public IConfiguration Configuration { get; }

		public Startup(
			IConfiguration configuration,
			IHostEnvironment hostEnvironment
		)
		{
			Configuration = configuration;
			_hostEnvironment = hostEnvironment;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			services.AddServerSideBlazor();
			if (!_hostEnvironment.IsDevelopment())
			{
				services
					.AddSignalR()
					.AddAzureSignalR(
						opt => Configuration.Bind("Azure:SignalR", opt)
					);
			}
			services.AddSingleton<WeatherForecastService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			var httpsSection = Configuration.GetSection("HttpServer:Endpoints:Https");
			if (httpsSection.Exists())
			{
				var httpsEndpoint = new Extensions.EndpointConfiguration();
				httpsSection.Bind(httpsEndpoint);
				app.UseRewriter(
					new RewriteOptions()
						.AddRedirectToHttps(
							statusCode: env.IsDevelopment()
							          ? StatusCodes.Status302Found
									: StatusCodes.Status301MovedPermanently,
							sslPort: httpsEndpoint.Port
						)
				);
			}

			//app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}
