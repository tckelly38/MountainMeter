using System;

namespace MountainMeter
{
	public class Mountain
	{
		public Mountain(string Name, int Meters, int Feet, string Location = "", string Range = "", string URL = "")
		{
			this.Name = Name;
			this.Meters = Meters;
			this.Feet = Feet;
			this.Location = Location;
			this.Range = Range;
			this.URL = URL;
		}


		public string Name { get; set; }

		public int Meters { get; set; }

		public int Feet { get; set; }

		public string Location { get; set; }

		public string Range { get; set; }

		public string URL { get; set; }


	}
}
