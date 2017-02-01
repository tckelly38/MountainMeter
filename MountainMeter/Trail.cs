using System;
namespace MountainMeter
{
	public class Trail
	{
		public Trail(string Name, int km, int miles, string Region = "", string endpoint1 = "", string endpoint2 = "", string URL = "")
		{
			this.Name = Name;
			this.km = km;
			this.miles = miles;
			this.Region = Region;
			this.endpoint1 = endpoint1;
			this.endpoint2 = endpoint2;
			this.URL = URL;
		}


		public string Name { get; set; }

		public int km { get; set; }

		public int miles { get; set; }

		public string Region { get; set; }

		public string endpoint1 { get; set; }

		public string endpoint2 { get; set; }

		public string URL { get; set; }
	}
}
