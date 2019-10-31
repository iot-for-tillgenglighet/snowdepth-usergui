using System;
using System.Linq;
using System.Threading.Tasks;

namespace snowdepth_usergui.Data
{
	public class MeasurementService
	{
		private static readonly string[] Comments = new[]
		{
			"Hovsgatan", "Kungsgatan", "Kungsgatan, drivsn�", "Hovsgatan, fordon har passerat", "Torget, os�ker m�tning pga drivsn�"
		};

		public Task<Measurement[]> GetMeasurementsAsync(DateTime startDate)
		{
			var rng = new Random();
			return Task.FromResult(Enumerable.Range(1, 9).Select(index => new Measurement
			{
				Date = startDate.AddDays(index).AddMinutes(rng.Next(1,180)),
				TemperatureC = rng.Next(-20, 5),
				Snowdepth = rng.Next(0, 100),
				Comment = Comments[rng.Next(Comments.Length)],
				Longitude = 18.068580800000063,
				Latitude = 59.32932349
			}).ToArray());
		}
	}
}
