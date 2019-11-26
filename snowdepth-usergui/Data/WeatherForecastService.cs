using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace snowdepth_usergui.Data
{
	public class MeasurementService
	{
		private static readonly string[] Comments = new[]
		{
			"Hovsgatan", "Kungsgatan", "Kungsgatan, drivsnö", "Hovsgatan, fordon har passerat", "Torget, osäker mätning pga drivsnö"
		};

		public static async Task SetMeasurementsAsync(int depth)
		{
			var query = @"mutation {addSnowdepthMeasurement(input: {pos: {lon:17.946908, lat: 62.637526},depth: "+depth.ToString()+",}) {manual}}";

			var postData = new { Query = query };
			var stringContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

			var postUri = "http://iotsundsvall.northeurope.cloudapp.azure.com/api/graphql";

			var httpClient = new HttpClient();

			var res = await httpClient.PostAsync(postUri, stringContent);
			if (res.IsSuccessStatusCode)
			{
				var content = await res.Content.ReadAsStringAsync();
			}
			else
			{
				string error = $"Error occurred... Status code:{res.StatusCode}";
			}
		}

		public async Task<Measurement[]> GetMeasurementsAsync()
		{
			var query = @"{
				snowdepths {
				depth,
				when,
				from {
					pos {
					lat 
					lon
					}
				},
				manual
				}
				}";

			var postData = new { Query = query };
			var stringContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

			var postUri = "http://iotsundsvall.northeurope.cloudapp.azure.com/api/graphql";

			var httpClient = new HttpClient();

			SnowdepthData measurements = new SnowdepthData();

			var res = await httpClient.PostAsync(postUri, stringContent);
			if (res.IsSuccessStatusCode)
			{
				var content = await res.Content.ReadAsStringAsync();
				measurements = JsonConvert.DeserializeObject<SnowdepthData>(content);
			}
			else
			{
				string error = $"Error occurred... Status code:{res.StatusCode}";
			}

			List<Measurement> resultMeasurements = new List<Measurement>();
			foreach (Snowdepth snowdepth in measurements.Data.Snowdepths)
			{
				Measurement measurement = new Measurement();

				//TimeZoneInfo myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
				TimeZoneInfo myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Budapest");
				DateTime when = TimeZoneInfo.ConvertTimeFromUtc(snowdepth.When, myTimeZone);

				measurement.Date = when;
				measurement.TemperatureC = null;
				measurement.Comment = " - ";
				measurement.Snowdepth = int.Parse(Math.Round(snowdepth.Depth,0).ToString());
				measurement.Longitude = snowdepth.From.Pos.Lon;
				measurement.Latitude = snowdepth.From.Pos.Lat;

				resultMeasurements.Add(measurement);
			}

			return resultMeasurements.ToArray();
		}
	}
}
