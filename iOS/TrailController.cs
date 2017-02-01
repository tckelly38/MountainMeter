using Foundation;
using System;
using UIKit;
using CoreMotion;

namespace MountainMeter.iOS
{
    public partial class TrailController : UIViewController
    {
		public Trail trail;
		CMPedometer pedometer;
		int total;
        public TrailController (IntPtr handle) : base (handle)
        {
        }
			public override async void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			this.NavigationController.NavigationBarHidden = true;

			var plist = NSUserDefaults.StandardUserDefaults;

			if (CMPedometer.IsDistanceAvailable)
			{
				pedometer = new CMPedometer();
				pedometer.StartPedometerUpdates(new NSDate(), UpdatePedometerData);

				var data = await pedometer.QueryPedometerDataAsync((NSDate)DateTime.SpecifyKind(Convert.ToDateTime(plist.StringForKey("installDate")), DateTimeKind.Utc), (NSDate)DateTime.Now);
				UpdatePedometerData(data, null);

			}
			else
				TravelLabel.Text = "Distance travelled is not enabled on this device";
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


			TravelLabel.Font = TravelLabel.Font.WithSize(34f);
			ProgressLabel.Font = ProgressLabel.Font.WithSize(28f);

			this.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Images/trail.jpg").Scale(View.Bounds.Size));
		}
		void UpdatePedometerData(CMPedometerData data, NSError error)
		{
			if (error == null)
			{
				InvokeOnMainThread(() =>
				{
					TravelLabel.Text = string.Format("{0}/{1}km", (int)data.Distance, trail.km);
					ProgressLabel.Text = string.Format("{0}% of {1}", Math.Round((float)data.Distance / trail.km, 2) * 100, trail.Name);

				});
			}
		}
    }
}