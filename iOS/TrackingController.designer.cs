// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MountainMeter.iOS
{
    [Register ("TrackingController")]
    partial class TrackingController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ProgressLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TravelLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ProgressLabel != null) {
                ProgressLabel.Dispose ();
                ProgressLabel = null;
            }

            if (TravelLabel != null) {
                TravelLabel.Dispose ();
                TravelLabel = null;
            }
        }
    }
}