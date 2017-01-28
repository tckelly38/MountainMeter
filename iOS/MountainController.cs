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

			this.TableView.Source = new MountainSource(items);

		}
	}

}