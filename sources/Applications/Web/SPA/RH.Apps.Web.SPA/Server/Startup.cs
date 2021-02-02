using System;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RH.Apps.Web.SPA.Server.Data;
using RH.Apps.Web.SPA.Server.Models;

namespace RH.Apps.Web.SPA.Server
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
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			var assembly = typeof(Startup).Assembly;
			//services
			//	.AddDbContextPool<ApplicationDbContext>(options =>
			//	{
			//		//if (!_hostEnvironment.IsProduction())
			//		options.UseSqlServer(
			//			connectionString,
			//			builder =>
			//			{
			//				builder.MigrationsAssembly(assembly.FullName);
			//				builder.MigrationsHistoryTable("Migrations", "dbo");
			//				builder.EnableRetryOnFailure(5);
			//			}
			//		);
			//		//else
			//		//	options.UseMySql(
			//		//		Configuration.GetConnectionString("DefaultConnection"),
			//		//		serverVersion: ServerVersion.AutoDetect(connectionString),
			//		//		builder =>
			//		//		{
			//		//			builder.MigrationsAssembly(typeof(Startup).FullName);
			//		//			builder.MigrationsHistoryTable("Migrations", "dbo");
			//		//			builder.EnableRetryOnFailure(5);
			//		//		}
			//		//	);
			//	});

			//services.AddDatabaseDeveloperPageExceptionFilter();

			//services
			//	.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
			//	.AddEntityFrameworkStores<ApplicationDbContext>();

			//services
			//	.AddIdentityServer()
			//	.AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

			//services
			//	.AddAuthentication()
			//	.AddIdentityServerJwt();

			services.AddRazorPages();
			services.AddControllersWithViews();

			var runtime = Configuration.GetSection("Runtime").Get<string>();

			if (runtime == "Server")
			{
				services.AddServerSideBlazor();
				var signalR = services.AddSignalR();
				//if (_hostEnvironment.IsProduction())
				//	signalR.AddAzureSignalR();
				//else
				//	services
				//		.AddReverseProxy()
				//		.LoadFromConfig(
				//			Configuration.GetSection("ReverseProxy")
				//		);
			}
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				//app.UseHsts();
			}

			//app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			//app.UseIdentityServer();
			//app.UseAuthentication();
			//app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				//if (!_hostEnvironment.IsProduction())
				//	endpoints.MapReverseProxy();
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToPage("/_Host");
				//endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
