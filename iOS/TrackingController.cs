using System;

using UIKit;
using CoreMotion;
using Foundation;

namespace MountainMeter.iOS
{
	public partial class TrackingController : UIViewController
	{
		CMPedometer pedometer;
		int total;
		const int mt_Eve_height = 8848;
		public TrackingController(IntPtr handle) : base(handle)
		{

		}
		public void setToolBar()
		{
			SetToolbarItems(new UIBarButtonItem[] {
				new UIBarButtonItem(UIImage.FromBundle("Images/ic_filter_hdr"), UIBarButtonItemStyle.Plain, (sender, e) => {
					//open mountain stuff
					MountainController mc = Storyboard.InstantiateViewController("MountainController") as MountainController;
					if(mc != null){
						NavigationController.PushViewController(mc, true);

					}

				})}, false);
			NavigationController.ToolbarHidden = false;
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();
			setToolBar();


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
		void UpdatePedometerData(CMPedometerData data, NSError error)
		{
			//one floor equals 3 meters
			if (error == null)
			{
				InvokeOnMainThread(() =>
				{
					total = ((int)data.FloorsAscended + (int)data.FloorsDescended) * 3;
					TravelLabel.Text = string.Format("You have climbed {0} meters", total);
					ProgressLabel.Text = string.Format("That's {0}% of Mt. Everest's {1}m height", ((float)total / mt_Eve_height).ToString("0.00"), mt_Eve_height);

				});
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}