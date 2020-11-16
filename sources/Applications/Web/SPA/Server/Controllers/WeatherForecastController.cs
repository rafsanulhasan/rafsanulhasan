using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using RH.Apps.Web.SPA.Shared;

namespace RH.Apps.Web.SPA.Server.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] SUMMARIES = new[]
		{
		  "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
			=> _logger = logger;

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			_logger.Log(LogLevel.Information, "");
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = SUMMARIES[rng.Next(SUMMARIES.Length)]
			})
			.ToArray();
		}
	}
}
