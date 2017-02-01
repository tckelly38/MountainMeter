using System;

using UIKit;
using CoreMotion;
using Foundation;
using System.Linq;
namespace MountainMeter.iOS
{
	public partial class TrackingController : UIViewController
	{
		CMPedometer pedometer;
		int total;
		public Mountain mountain;
		public TrackingController(IntPtr handle) : base(handle)
		{
			if (mountain == null)
				mountain = new Mountain("Mount Everest", 8898, 29029);
		}

		public override async void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			this.NavigationController.NavigationBarHidden = true;

			var plist = NSUserDefaults.StandardUserDefaults;

			if (CMPedometer.IsFloorCountingAvailable)
			{
				pedometer = new CMPedometer();
				pedometer.StartPedometerUpdates(new NSDate(), UpdatePedometerData);

				var data = await pedometer.QueryPedometerDataAsync((NSDate)DateTime.SpecifyKind(Convert.ToDateTime(plist.StringForKey("installDate")), DateTimeKind.Utc), (NSDate)DateTime.Now);
				UpdatePedometerData(data, null);

			}
			else
				TravelLabel.Text = "Floor counting not enabled on this device";
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


			setToolBar(this);
			TravelLabel.Font = TravelLabel.Font.WithSize(34f);
			ProgressLabel.Font = ProgressLabel.Font.WithSize(28f);

			this.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Images/mountain.jpg").Scale(View.Bounds.Size));
		}
		void UpdatePedometerData(CMPedometerData data, NSError error)
		{
			//one floor equals 3 meters
			if (error == null)
			{
				InvokeOnMainThread(() =>
				{
					total = ((int)data.FloorsAscended + (int)data.FloorsDescended) * 3;
					TravelLabel.Text = string.Format("{0}/{1}m", total, mountain.Meters);
					ProgressLabel.Text = string.Format("{0}% of {1}", Math.Round((float)total / mountain.Meters, 2) * 100, mountain.Name);

				});
			}
		}
		public static void setToolBar(UIViewController owner)
		{
			var settingsButton = new UIBarButtonItem(UIImage.FromBundle("Images/ic_settings_18pt"), UIBarButtonItemStyle.Plain, (sender, e) =>
			{
				//open mountain stuff
				SettingsController settings = owner.Storyboard.InstantiateViewController("SettingsController") as SettingsController;
				if (settings != null)
				{
					if (owner is TrackingController)
					{
						owner.NavigationController.PushViewController(settings, true);
					}
					else {
						//var viewControllers = owner.NavigationController.ViewControllers;
						//viewControllers = viewControllers.Take(viewControllers.Length - 1).ToArray();
						//owner.NavigationController.SetViewControllers(viewControllers, false);
						UINavigationController nav = owner.NavigationController;
						nav.PopToRootViewController(false);
						nav.PushViewController(settings, false);
					}

				}
			});
			var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) { Width = 50 };
			var spacer25= new UIBarButtonItem(UIBarButtonSystemItem.FixedSpace) { Width = 25 };

			var mountainButton = new UIBarButtonItem(UIImage.FromBundle("Images/ic_filter_hdr"), UIBarButtonItemStyle.Plain, (sender, e) =>
			{
				//open mountain stuff
				MountainController mc = owner.Storyboard.InstantiateViewController("MountainController") as MountainController;
				if (mc != null)
				{
					if (owner is TrackingController)
						owner.NavigationController.PushViewController(mc, true);
					else
					{
						//var viewControllers = owner.NavigationController.ViewControllers;
						//viewControllers = viewControllers.Take(viewControllers.Length - 1).ToArray();
						//owner.NavigationController.SetViewControllers(viewControllers, false);
						UINavigationController nav = owner.NavigationController;
						nav.PopToRootViewController(false);
						nav.PushViewController(mc, false);
					}
				}
			});
			var trailButton = new UIBarButtonItem(UIImage.FromBundle("Images/ic_directions_run_18pt"), UIBarButtonItemStyle.Plain, (sender, e) =>
			{
				//open mountain stuff
				TrailListController trail_list = owner.Storyboard.InstantiateViewController("TrailListController") as TrailListController;
				if (trail_list != null)
				{
					if (owner is TrackingController)
						owner.NavigationController.PushViewController(trail_list, true);
					else
					{
						//var viewControllers = owner.NavigationController.ViewControllers;
						//viewControllers = viewControllers.Take(viewControllers.Length - 1).ToArray();
						//owner.NavigationController.SetViewControllers(viewControllers, false);
						UINavigationController nav = owner.NavigationController;
						nav.PopToRootViewController(false);
						nav.PushViewController(trail_list, false);
					}
				}
			});

			owner.SetToolbarItems(new UIBarButtonItem[] {
				settingsButton, spacer, trailButton, spacer25, mountainButton
			}, false);
			if (owner is MountainController)
				mountainButton.Enabled = false;
			else if (owner is SettingsController)
				settingsButton.Enabled = false;

			owner.NavigationController.ToolbarHidden = false;
			owner.NavigationController.NavigationBarHidden = false;
		}
		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
