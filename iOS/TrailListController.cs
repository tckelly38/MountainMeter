using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;

namespace MountainMeter.iOS
{
    public partial class TrailListController : UITableViewController
    {
        public TrailListController (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TrackingController.setToolBar(this);
			this.NavigationController.NavigationBarHidden = false;
			this.Title = "Trails";
			var items = new List<Trail>();
			items.Add(new Trail("Western Loop", 11064, 6875, "Western United States"));
			items.Add(new Trail("Continental Trail", 8700, 5400, "Eastern United States"));
			items.Add(new Trail("Florida Trail", 1600, 1000, "Florida"));
			items.Add(new Trail("Appalachian Trail", 3500, 2200, "Appalachian Mountains"));
			items = items.OrderByDescending((arg) => arg.miles).ToList();
			TableView.Source = new TrailSource(items, this);

		}
    }
}