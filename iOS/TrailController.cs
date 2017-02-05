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
					var distance = (int)data.Distance / 1000;
					TravelLabel.Text = string.Format("{0}/{1}km", distance, trail.km);
					ProgressLabel.Text = string.Format("{0}% of {1}", Math.Round(((float)distance / trail.km) * 100, 2), trail.Name);

				});
			}
		}
    }
}