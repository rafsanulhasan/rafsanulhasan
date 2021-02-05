using System;

namespace RH.Apps.Web.SPA.Shared
{
	public class AppSettingsService
		: IRuntimeService
	{
		public BlazorRuntimes GetRuntime()
			=> OperatingSystem.IsBrowser()
			 ? BlazorRuntimes.WebAssembly
			 : BlazorRuntimes.Server;
	}
}
