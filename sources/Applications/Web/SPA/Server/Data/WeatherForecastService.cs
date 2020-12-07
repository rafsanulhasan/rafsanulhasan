using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RH.Apps.Web.SPA.Lite.Data
{
	public class WeatherForecastService
	{
		private readonly string[] _summeries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
		{
			var rng = RandomNumberGenerator.Create();
			return Task.FromResult(
				Enumerable
					.Range(1, 5)
					.Select(index => {
						var tempData = new byte[16];
						rng.GetBytes(tempData);
						var temp = int.Parse(BitConverter.ToString(tempData));
						var summaryNumber = RandomNumberGenerator.GetInt32(0, _summeries.Length);
						return new WeatherForecast
						{
							Date = startDate.AddDays(index),
							TemperatureC = temp,
							Summary = _summeries[summaryNumber]
						};
					})
				.ToArray()
			);
		}
	}
}
