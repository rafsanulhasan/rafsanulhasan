﻿using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RH.Apps.Web.SPA.Lite.Pages
{
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	[IgnoreAntiforgeryToken]
	public class ErrorModel : PageModel
	{
		public string RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

		private readonly ILogger<ErrorModel> _logger;

		public ErrorModel(ILogger<ErrorModel> logger)
			=> _logger = logger;

		public void OnGet()
		{
			_logger.LogInformation("");
			RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
		}
	}
}
