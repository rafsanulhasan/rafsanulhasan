﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using System.Diagnostics;

namespace RH.Apps.Web.SPA.Server.Pages
{
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	[IgnoreAntiforgeryToken]
	public class ErrorModel : PageModel
	{
		public string RequestId { get; set; } = string.Empty;

		public bool ShowRequestId 
			=> !string.IsNullOrEmpty(RequestId);

		private readonly ILogger<ErrorModel> _logger;

		public ErrorModel(ILogger<ErrorModel> logger) 
			=> _logger = logger;

		public void OnGet()
		{
			RequestId = Activity.Current?.Id 
				    ?? HttpContext.TraceIdentifier;
			_logger.LogInformation($"{RequestId}");
		}
	}
}
