using System;
using UIKit;
using System.Collections.Generic;
using Foundation;
namespace MountainMeter.iOS
{
	public class MountainSource : UITableViewSource
	{
		List<Mountain> Mountains;
		string CellIdentifier = "TableCell";
		UIViewController owner;
		public MountainSource(List<Mountain> m, UIViewController o)
		{
			Mountains = m;
			owner = o;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
			Mountain m = Mountains[indexPath.Row];
			if (cell == null)
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);

			cell.TextLabel.Text = m.Name;
			cell.DetailTextLabel.Text = m.Meters + "m\t" + m.Feet + "ft\t" + m.Location;
			cell.DetailTextLabel.TextColor = UIColor.LightGray;
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			return cell;

		}
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			//hacky way to pass data back on the navigtion stack
			((TrackingController)owner.NavigationController.ViewControllers[0]).mountain = Mountains[indexPath.Row];
			owner.NavigationController.PopViewController(true);

		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return Mountains.Count;
		}
	}
}
