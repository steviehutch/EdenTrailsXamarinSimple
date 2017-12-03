using System;

using Foundation;
using UIKit;

using Wikitude.Architect;


namespace WikitudeSDKExample
{
	public partial class WTSDKInformationViewController : UITableViewController
	{
		public WTSDKInformationViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			UITableViewCell versionNumberCell = this.TableView.CellAt(NSIndexPath.FromRowSection(0, 0));
			versionNumberCell.DetailTextLabel.TextColor = UIColor.DarkGray;
            versionNumberCell.DetailTextLabel.Text = "1.0.3"; //WTArchitectView.SDKVersion;

			UITableViewCell buildDateCell = this.TableView.CellAt(NSIndexPath.FromRowSection(0, 1));
			buildDateCell.DetailTextLabel.TextColor = UIColor.DarkGray;
            buildDateCell.DetailTextLabel.Text = "Eden Project: " + DateTime.Now.Date.ToString();//WTArchitectView.SDKBuildInformation.BuildDate;

			UITableViewCell buildNumberCell = this.TableView.CellAt(NSIndexPath.FromRowSection(1, 1));
			buildNumberCell.DetailTextLabel.TextColor = UIColor.DarkGray;
            buildNumberCell.DetailTextLabel.Text = "Build_Version_Robert"; //WTArchitectView.SDKBuildInformation.BuildNumber;

			UITableViewCell buildConfigurationCell = this.TableView.CellAt(NSIndexPath.FromRowSection(2, 1));
			buildConfigurationCell.DetailTextLabel.TextColor = UIColor.DarkGray;
			buildConfigurationCell.DetailTextLabel.Text = WTArchitectView.SDKBuildInformation.BuildConfiguration;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
