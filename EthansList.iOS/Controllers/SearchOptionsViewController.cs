using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using EthansList.Shared;
using EthansList.Models;

namespace ethanslist.ios
{
	partial class SearchOptionsViewController : UIViewController
	{
        UIBarButtonItem saveButton;

        public string MinBedrooms { get; set; }
        public string MinBathrooms { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string SearchTerms { get; set; }
        public int MaxListings 
        { 
            get { return maxListings; } 
            set { maxListings = value; }
        }
        private int maxListings = 25;
        public int? WeeksOld { get; set; }

        public Location Location { get; set; }

		public SearchOptionsViewController (IntPtr handle) : base (handle)
		{
		}

        public override void LoadView()
        {
            base.LoadView();

            this.View.Layer.BackgroundColor = ColorScheme.Clouds.CGColor;

            SearchButton.Layer.BackgroundColor = ColorScheme.MidnightBlue.CGColor;
            SearchButton.SetTitleColor(ColorScheme.Clouds, UIControlState.Normal);
            SearchButton.Layer.CornerRadius = 10;
            SearchButton.ClipsToBounds = true;

            SearchTableView.Layer.BackgroundColor = ColorScheme.Clouds.CGColor;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            AddLayoutConstraints();

            this.Title = "Options";
            SearchCityLabel.Text = String.Format("Search {0} for:", Location.SiteName);

            SearchTableView.Source = new SearchOptionsTableSource(GetTableSetup(), this);

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            View.AddGestureRecognizer(g);

            saveButton = new UIBarButtonItem (
                UIImage.FromFile ("save.png"),
                UIBarButtonItemStyle.Plain,
                async (sender, e) => {
                    await AppDelegate.databaseConnection.AddNewSearchAsync(Location.Url, Location.SiteName, MinPrice, MaxPrice, 
                    MinBedrooms, MinBathrooms, SearchTerms, WeeksOld, MaxListings);
                    Console.WriteLine(AppDelegate.databaseConnection.StatusMessage);

                    if (AppDelegate.databaseConnection.StatusCode == codes.ok)
                    {
                        UIAlertView alert = new UIAlertView();
                        alert.Message = "Search Saved!";
                        alert.AddButton("OK");
                        alert.Show();

                        this.NavigationItem.RightBarButtonItem.Enabled = false;
                    }
                    else
                    {
                        UIAlertView alert = new UIAlertView();
                        alert.Message = String.Format("Oops, something went wrong{0}Please try again...", Environment.NewLine);
                        alert.AddButton("OK");
                        alert.Show();

                        this.NavigationItem.RightBarButtonItem.Enabled = true;
                    }
                }
            );

            NavigationItem.RightBarButtonItem = saveButton;

            SearchButton.TouchUpInside += (sender, e) => {
                QueryGeneration queryHelper = new QueryGeneration();
                var query = queryHelper.Generate(Location.Url, new Dictionary<string, string>()
                    {
                        {"min_price", MinPrice},
                        {"max_price", MaxPrice},
                        {"bedrooms", MinBedrooms},
                        {"bathrooms", MinBathrooms},
                        {"query", SearchTerms}
                    }
                );
                Console.WriteLine (query);

                var storyboard = UIStoryboard.FromName("Main", null);
                var feedViewController = (FeedResultsTableViewController)storyboard.InstantiateViewController("FeedResultsTableViewController");

                feedViewController.Query = query;
                feedViewController.MaxListings = MaxListings;
                feedViewController.WeeksOld = WeeksOld;

                this.ShowViewController(feedViewController, this);
            };
        }

        private List<TableItemGroup> GetTableSetup()
        {
            List<TableItemGroup> tableItems = new List<TableItemGroup>();

            TableItemGroup searchterms = new TableItemGroup()
                { Name = "Search Terms"};
            searchterms.Items.Add(new TableItem() { 
                Heading = "Search Terms",
                CellType = "SearchTermsCell",
            });
            searchterms.Items.Add(new TableItem() {
                Heading = "Price",
                CellType = "PriceSelectorCell",
                PickerOptions = new List<PickerOptions> ()
                    {
                        new PickerOptions(){Options = new Dictionary<object, object>()
                            {
                                {0, "Any"},
                                {1, "400"},
                                {2, "600"},
                                {3, "800"},
                                {4, "1000"},
                                {5, "1200"},
                                {6, "1400"},
                                {7, "1600"},
                                {8, "1800"},
                                {9, "2000"},
                                {10, "2200"},
                                {11, "2400"},
                                {12, "2600"},
                            }},
                        new PickerOptions(){Options = new Dictionary<object, object>()
                            {
                                {0, "Any"},
                                {1, "1000"},
                                {2, "1400"},
                                {3, "1600"},
                                {4, "1800"},
                                {5, "1000"},
                                {6, "2200"},
                                {7, "2400"},
                                {8, "2600"},
                                {9, "2800"},
                                {10, "3000"},
                                {11, "3200"},
                                {12, "3400"},
                            }},
                    },
            });

            TableItemGroup options = new TableItemGroup()
                { 
                    Name = "Options",
                };
            options.Items.Add(new TableItem() {
                Heading = "Min Bedrooms",
                CellType = "ActionSheetCell",
                ActionOptions = new Dictionary<string, object>() 
                {
                    {"Any", "Any"},
                    {"Studio", "0"},
                    {"1+", "1"},
                    {"2+", "2"},
                    {"3+", "3"},
                    {"4+", "4"},
                }
            });
            options.Items.Add(new TableItem() {
                Heading = "Min Bathrooms",
                CellType = "ActionSheetCell",
                ActionOptions = new Dictionary<string, object>() 
                {
                    {"Any", "Any"},
                    {"1+", "1"},
                    {"2+", "2"},
                    {"3+", "3"},
                }
            });
            options.Items.Add(new TableItem() {
                Heading = "Posted Date",
                CellType = "ActionSheetCell",
                ActionOptions = new Dictionary<string, object>()
                {
                    {"Any", null},
                    {"Today", -1},
                    {"1 Week Old", 1},
                    {"2 Weeks Old", 2},
                    {"3 Weeks Old", 3},
                    {"4 Weeks Old", 4},
                }
            });
            options.Items.Add(new TableItem() {
                Heading = "Max Listings",
                CellType = "ActionSheetCell",
                SubHeading = "25",
                ActionOptions = new Dictionary<string, object>() 
                {
                    {"25", "25"},
                    {"50", "50"},
                    {"75", "75"},
                    {"100", "100"},
                }
            });

            tableItems.Add(searchterms);
            tableItems.Add(options);

            return tableItems;
        }

        void AddLayoutConstraints()
        {
            scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
            SearchCityLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            SearchButton.TranslatesAutoresizingMaskIntoConstraints = false;
            SearchTableView.TranslatesAutoresizingMaskIntoConstraints = false;
            scrollView.ContentSize = new CoreGraphics.CGSize(this.View.Bounds.Width, this.View.Bounds.Height + 280f);
            scrollView.Frame = this.View.Frame;

            //Seach CL Label Constraints
            this.View.AddConstraints(new NSLayoutConstraint[] {
                NSLayoutConstraint.Create(SearchCityLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Width, .90f, 0),
                NSLayoutConstraint.Create(SearchCityLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.CenterX, 1, 0),
                NSLayoutConstraint.Create(SearchCityLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.TopMargin, 1, 79),
            });

            //Seach Button Constraints
            this.View.AddConstraints(new NSLayoutConstraint[] {
                NSLayoutConstraint.Create(SearchButton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Width, .90f, 0),
                NSLayoutConstraint.Create(SearchButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.CenterX, 1, 0),
                NSLayoutConstraint.Create(SearchButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, SearchCityLabel, NSLayoutAttribute.Bottom, 1, 15),
            });

            //Seach Table Constraints
            this.View.AddConstraints(new NSLayoutConstraint[] {
                NSLayoutConstraint.Create(SearchTableView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Width, 1, 0),
                NSLayoutConstraint.Create(SearchTableView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.CenterX, 1, 0),
                NSLayoutConstraint.Create(SearchTableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, SearchButton, NSLayoutAttribute.Bottom, 1, 15),
            });
        }
	}
}