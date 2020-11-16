
using System.Runtime.InteropServices;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
			=> Configuration = configuration;

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
				   Configuration.GetConnectionString("DefaultConnection")
				)
			);

			services.AddDatabaseDeveloperPageExceptionFilter();

			services
				.AddDefaultIdentity<User>(
					options => options.SignIn.RequireConfirmedAccount = true
				)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddIdentityServer()
			    .AddApiAuthorization<User, ApplicationDbContext>();

			services.AddAuthentication()
			    .AddIdentityServerJwt();

			services.AddControllersWithViews();
			services.AddRazorPages();
			//services.AddServerSideBlazor(o => o.DetailedErrors = true);
			//services.AddSignalR();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			var isWASM = RuntimeInformation.IsOSPlatform(OSPlatform.Create("WEBASSEMBLY"));
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
				//if (isWASM)
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			//if (isWASM)
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseIdentityServer();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				//endpoints.MapBlazorHub();

				endpoints.MapRazorPages();
				endpoints.MapControllers();

				endpoints.MapFallbackToFile("index.html");
				//endpoints.MapFallbackToPage("/lite", "/_Host");
			});
		}
	}
}
