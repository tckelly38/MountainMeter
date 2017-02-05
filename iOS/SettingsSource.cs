using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
namespace MountainMeter.iOS
{
	public class SettingsSource:UITableViewSource
	{
		List<string> options;
		string Identifier = "cell";
		UIViewController owner;
		public SettingsSource(UIViewController controller, List<string> options)
		{
			owner = controller;
			this.options = options;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(Identifier);
			if (cell == null)
				cell = new UITableViewCell(UITableViewCellStyle.Default, Identifier);
			cell.TextLabel.Text = options[indexPath.Row];
			return cell;
		}
		public override nint NumberOfSections(UITableView tableView)
		{
			return options.Count;
		}
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return 1;
		}
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			switch (options[indexPath.Row]) {
				case "Reset Progress":
					var Alert = UIAlertController.Create("Attention", "Are you sure you want to reset your progress?", UIAlertControllerStyle.Alert);
					Alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
					Alert.AddAction(UIAlertAction.Create("Yes", UIAlertActionStyle.Default, (obj) => { 
						var plist = NSUserDefaults.StandardUserDefaults;
						plist.SetString(DateTime.Now.ToString(), "installDate");
						plist.SetInt(0, "highIntensity");
						plist.Synchronize();

					
					}));
					owner.PresentViewController(Alert, animated: true, completionHandler: null);

					break;
			}
			tableView.DeselectRow(indexPath, true);
		}
	}
}
