using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace MountainMeter.iOS
{
	public partial class MountainController : UITableViewController
	{
		public MountainController(IntPtr handle) : base(handle)
		{
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			var items = new List<Mountain>();
			items.Add(new Mountain("Mount Everest", 8848, 29029, "Nepal", "Himalayas", "https://en.wikipedia.org/wiki/Mount_Everest"));
			items.Add(new Mountain("Nanda Devi", 7816, 25643, "India", "Himalayas", "https://en.wikipedia.org/wiki/Nanda_Devi"));
			items.Add(new Mountain("Machapuchare", 6993, 22943, "Nepal", "Himalayas", "https://en.wikipedia.org/wiki/Machapuchare"));
			items.Add(new Mountain("Mount Logan", 5959, 19551, "Canada", "Saint Elias Mountains", "https://en.wikipedia.org/wiki/Mount_Logan"));
			items.Add(new Mountain("Mount Kilimanjaro", 5895, 19341, "Tanzania", "", "https://en.wikipedia.org/wiki/Mount_Kilimanjaro"));
			items.Add(new Mountain("Truchas Peak", 3994, 13104, "New Mexico", "Sangre de Cristo Mountains", "https://en.wikipedia.org/wiki/Truchas_Peak"));
			items.Add(new Mountain("Britton Hill", 105, 344, "Florida", "", "https://en.wikipedia.org/wiki/Britton_Hill"));

			this.TableView.Source = new MountainSource(items, this);

		}


	}

}