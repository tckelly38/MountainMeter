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
    [Register ("SettingsController")]
    partial class SettingsController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView SettingsTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (SettingsTable != null) {
                SettingsTable.Dispose ();
                SettingsTable = null;
            }
        }
    }
}