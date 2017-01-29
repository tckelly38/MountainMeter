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
		public Mountain mountain;
		public TrackingController(IntPtr handle) : base(handle)
		{
			if (mountain == null)
				mountain = new Mountain("Mount Everest", 8898, 29029);
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
			setToolBar();
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

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
