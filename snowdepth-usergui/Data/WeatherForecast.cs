using System;
using System.ComponentModel.DataAnnotations;

namespace snowdepth_usergui.Data
{
	public class Measurement
	{
		public DateTime Date { get; set; }
		public int? TemperatureC { get; set; }
		[Required]
		public int? Snowdepth { get; set; }
		[Required]
		[MaxLength(20)]
		public string Comment { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
	}
}
