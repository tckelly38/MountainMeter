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

		public MountainSource(List<Mountain> m)
		{
			Mountains = m;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);

			if (cell == null)
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);

			cell.TextLabel.Text = Mountains[indexPath.Row].Name;
			cell.DetailTextLabel.Text = Mountains[indexPath.Row].Meters.ToString();
			return cell;

		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return Mountains.Count;
		}
	}
}
