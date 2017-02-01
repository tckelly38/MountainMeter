using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
namespace MountainMeter.iOS
{
    public partial class SettingsController : UIViewController
    {
        public SettingsController (IntPtr handle) : base (handle)
        {
        }
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			TrackingController.setToolBar(this);
			Title = "Settings";
			List<string> options = new List<string>();
			options.Add("Reset Progress");
			this.View.BackgroundColor = UIColor.FromRGB(238, 239, 244);
			SettingsTable.BackgroundColor = UIColor.FromRGB(239, 239, 244);
			SettingsTable.Source = new SettingsSource(this, options);
			SettingsTable.TableFooterView = new UIView();
			SettingsTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;

		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.NavigationItem.HidesBackButton = false;

		}
    }
}