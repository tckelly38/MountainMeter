using Foundation;
using System;
using UIKit;
using CoreGraphics;
namespace MountainMeter.iOS
{
	public partial class ContainerController : UIViewController
	{
		public Mountain mountain;
		public Trail trail;
		public ContainerController(IntPtr handle) : base(handle)
		{
			if (mountain == null)
				mountain = new Mountain("Mount Everest", 8898, 29029);
			if (trail == null)
				trail = new Trail("Great Western Loop", 11064, 6875);
		}
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			TrackingController.setToolBar(this);

			TrackingController track_c = Storyboard.InstantiateViewController("TrackingController") as TrackingController;
			track_c.mountain = mountain;
			this.AddChildViewController(track_c);
			this.ScrollView.AddSubview(track_c.View);
			track_c.DidMoveToParentViewController(this);

			TrailController trail_c = Storyboard.InstantiateViewController("TrailController") as TrailController;
			trail_c.trail = trail;
			CGRect trail_frame = trail_c.View.Frame;
			trail_frame.X = this.View.Frame.Width;
			trail_c.View.Frame = trail_frame;

			this.AddChildViewController(trail_c);
			this.ScrollView.AddSubview(trail_c.View);
			trail_c.DidMoveToParentViewController(this);
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.Title = "Home";

			this.ScrollView.ContentSize = new CGSize(this.View.Frame.Width * 2, ScrollView.Frame.Size.Height);
			this.ScrollView.PagingEnabled = true;
			this.AutomaticallyAdjustsScrollViewInsets = false;
		}
	}
}