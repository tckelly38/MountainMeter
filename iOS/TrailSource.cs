using System;
using UIKit;
using System.Collections.Generic;
using Foundation;
namespace MountainMeter.iOS
{
	public class TrailSource : UITableViewSource
	{
		List<Trail> Trails;
		string CellIdentifier = "TableCell";
		UIViewController owner;
		public TrailSource(List<Trail> m, UIViewController o)
		{
			Trails = m;
			owner = o;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
			Trail m = Trails[indexPath.Row];
			if (cell == null)
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);

			cell.TextLabel.Text = m.Name;
			cell.DetailTextLabel.Text = m.km + "km\t" + m.miles + "miles\t" + m.Region;
			cell.DetailTextLabel.TextColor = UIColor.LightGray;
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			return cell;

		}
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			//hacky way to pass data back on the navigtion stack
			((ContainerController)owner.NavigationController.ViewControllers[0]).trail = Trails[indexPath.Row];
			owner.NavigationController.PopViewController(true);

		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return Trails.Count;
		}
	}
}
