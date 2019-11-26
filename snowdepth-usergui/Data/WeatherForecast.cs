using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace snowdepth_usergui.Data
{
	public class Measurement
	{
		public DateTime Date { get; set; }
		public int? TemperatureC { get; set; }
		[Required]
		public int? Snowdepth { get; set; }
		[MaxLength(20)]
		public string Comment { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
	}

	public class Pos
	{
		public double Lat { get; set; }
		public double Lon { get; set; }
	}

	public class From
	{
		public Pos Pos { get; set; }
	}

	public class Snowdepth
	{
		public double Depth { get; set; }
		public DateTime When { get; set; }
		public bool Manual { get; set; }
		public From From { get; set; }
	}

	public class Data
	{
		public List<Snowdepth> Snowdepths { get; set; }
	}

	public class SnowdepthData
	{
		public Data Data { get; set; }
	}
}
