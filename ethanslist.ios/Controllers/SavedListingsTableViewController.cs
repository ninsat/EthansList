using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using EthansList.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ethanslist.ios
{
	partial class SavedListingsTableViewController : UITableViewController
	{
        SavedListingsTableViewSource tableSource;
        List<Listing> savedListings;

		public SavedListingsTableViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Console.WriteLine(AppDelegate.databaseConnection.GetAllListingsAsync().Result.Count);

            savedListings = AppDelegate.databaseConnection.GetAllListingsAsync().Result;
            tableSource = new SavedListingsTableViewSource(this, savedListings);
            TableView.Source = tableSource;

            tableSource.ItemDeleted += OnItemDeleted;
        }

        void OnItemDeleted(object sender, EventArgs e)
        {
            savedListings = AppDelegate.databaseConnection.GetAllListingsAsync().Result;
            tableSource = new SavedListingsTableViewSource(this, savedListings);
            TableView.Source = tableSource;

            tableSource.ItemDeleted += OnItemDeleted;
        }
	}
}
