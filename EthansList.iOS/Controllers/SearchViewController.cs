﻿using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using EthansList.Shared;
using EthansList.Models;
using System.Drawing;
using CoreGraphics;
using Newtonsoft.Json;
using iAd;

namespace ethanslist.ios
{
    public class SearchViewController : UIViewController
    {
        UIBarButtonItem saveButton;
        SearchOptionsTableSource tableSource;
        UIView holderView;
        UIButton SearchButton;
        UITableView SearchTableView;
        UIScrollView scrollView;
        UILabel SearchCityLabel;
        ADBannerView ads;
        NSLayoutConstraint searchTableBottom;

        private float scroll_amount = 0.0f;    // amount to scroll 
        private float bottom = 0.0f;           // bottom point
        private float offset = 40.0f;          // extra offset
        private bool moveViewUp = false;       // which direction are we moving
        private bool keyboardSet = false;

        #region GeneratedFromSearchOptions
        public int MaxListings
        {
            get { return maxListings; }
            set { maxListings = value; }
        }
        private int maxListings = 25;
        public int? WeeksOld { get; set; }
        public KeyValuePair<object, object> SubCategory { get; set; }
        public Dictionary<string, string> SearchItems { get; set; }
        public Dictionary<object, KeyValuePair<object, object>> Conditions { get; set; }
        #endregion

        public Location Location { get; set; }
        public KeyValuePair<string, string> Category { get; set; }
        public UIView FieldSelected { get; set; }
        public CGRect KeyboardBounds { get; set; }

        public SearchViewController()
        {
        }

        public override void LoadView()
        {
            base.LoadView();

            this.View.Layer.BackgroundColor = ColorScheme.Clouds.CGColor;

            SearchButton = new UIButton();
            holderView = new UIView(this.View.Frame);
            SearchTableView = new UITableView(new CGRect(), UITableViewStyle.Grouped);
            scrollView = new UIScrollView(this.View.Frame);
            SearchCityLabel = new UILabel { TextAlignment = UITextAlignment.Center };
            SearchCityLabel.AttributedText = new NSAttributedString(String.Format("Search {0} for:", Location.SiteName), Constants.LabelAttributes);

            ads = new ADBannerView();

            SearchButton.Layer.BackgroundColor = ColorScheme.MidnightBlue.CGColor;
            SearchButton.Layer.CornerRadius = 10;
            SearchButton.ClipsToBounds = true;
            SearchButton.SetAttributedTitle(new NSAttributedString("Search", Constants.ButtonAttributes), UIControlState.Normal);

            SearchTableView.Layer.BackgroundColor = ColorScheme.Clouds.CGColor;

            holderView.AddSubviews(new UIView[] { SearchButton, SearchCityLabel, SearchTableView, ads });
            scrollView.AddSubview(holderView);
            this.View.AddSubview(scrollView);

            AddLayoutConstraints();

            saveButton = new UIBarButtonItem(
                UIImage.FromFile("save.png"),
                UIBarButtonItemStyle.Plain,
                SaveButtonClicked);

            NavigationItem.RightBarButtonItem = saveButton;
        }

        NSObject keyboardUpToken, keyboardDownToken;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            // Keyboard popup
            keyboardUpToken = NSNotificationCenter.DefaultCenter.AddObserver
                                (UIKeyboard.DidShowNotification, KeyBoardUpNotification);

            // Keyboard Down
            keyboardDownToken = NSNotificationCenter.DefaultCenter.AddObserver
                                (UIKeyboard.WillHideNotification, KeyBoardDownNotification);

            ads.AdLoaded += AddAdBanner_Event;

            SearchButton.TouchUpInside += SearchButton_Clicked;

            SearchItems = new Dictionary<string, string>();
            Conditions = new Dictionary<object, KeyValuePair<object, object>>();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            keyboardUpToken?.Dispose();
            keyboardDownToken?.Dispose();
            saveButton?.Dispose();

            ads.AdLoaded -= AddAdBanner_Event;

            saveButton.Dispose();

            SearchButton.TouchUpInside -= SearchButton_Clicked;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            tableSource = new SearchOptionsTableSource(GetTableSetup(), this);
            SearchTableView.Source = tableSource;

            this.Title = "Options";

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            View.AddGestureRecognizer(g);
        }

        void SearchButton_Clicked(object sender, EventArgs e)
        {
            QueryGeneration queryHelper = new QueryGeneration();
            var feedViewController = new SearchResultsViewController();

            SearchObject searchObject = new SearchObject();
            searchObject.SearchLocation = Location;
            searchObject.Category = SubCategory.Value != null ? new KeyValuePair<object, object>(SubCategory.Value, SubCategory.Key) : new KeyValuePair<object, object>(Category.Key, Category.Value);
            searchObject.SearchItems = this.SearchItems;
            searchObject.Conditions = this.Conditions;

            var query = queryHelper.Generate(searchObject);

            feedViewController.Query = query;
            feedViewController.MaxListings = MaxListings;
            feedViewController.WeeksOld = WeeksOld;

            this.ShowViewController(feedViewController, this);
        }

        async void SaveButtonClicked(object sender, EventArgs e)
        {
            SearchObject searchObject = new SearchObject();
            searchObject.SearchLocation = Location;
            searchObject.Category = SubCategory.Value != null ? new KeyValuePair<object, object>(SubCategory.Value, SubCategory.Key) : new KeyValuePair<object, object>(Category.Key, Category.Value);
            searchObject.SearchItems = this.SearchItems;
            searchObject.Conditions = this.Conditions;
            searchObject.MaxListings = this.MaxListings;
            searchObject.PostedDate = this.WeeksOld;

            string serialized = JsonConvert.SerializeObject(searchObject);
            await AppDelegate.databaseConnection.AddNewSearchAsync(Location.Url, serialized);
            serialized = null;

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
                alert.Message = string.Format("Oops, something went wrong{0}Please try again...", Environment.NewLine);
                alert.AddButton("OK");
                alert.Show();

                this.NavigationItem.RightBarButtonItem.Enabled = true;
            }

            searchObject = null;
        }


        void AddAdBanner_Event(object sender, EventArgs e)
        {
            AddAdBanner(true);
        }


        void AddAdBanner(bool show)
        {
            if (searchTableBottom != null)
                View.RemoveConstraint(searchTableBottom);

            if (show)
            {
                ads.Hidden = false;

                //Ads Constraints
                this.View.AddConstraints(new NSLayoutConstraint[] {
                    NSLayoutConstraint.Create(ads, NSLayoutAttribute.Width, NSLayoutRelation.Equal, holderView, NSLayoutAttribute.Width, 1, 0),
                    NSLayoutConstraint.Create(ads, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, holderView, NSLayoutAttribute.CenterX, 1, 0),
                    NSLayoutConstraint.Create(ads, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, holderView, NSLayoutAttribute.Bottom, 1, 0),
                    //NSLayoutConstraint.Create(ads, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 50),
                });

                searchTableBottom = NSLayoutConstraint.Create(SearchTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ads, NSLayoutAttribute.Bottom, 1, 0);
            }
            else
            {
                ads.Hidden = true;
                searchTableBottom = NSLayoutConstraint.Create(SearchTableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, holderView, NSLayoutAttribute.Bottom, 1, 0);
            }
            View.AddConstraint(searchTableBottom);
            this.View.LayoutIfNeeded();
        }

        void AddLayoutConstraints()
        {
            holderView.TranslatesAutoresizingMaskIntoConstraints = false;
            SearchCityLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            SearchButton.TranslatesAutoresizingMaskIntoConstraints = false;
            SearchTableView.TranslatesAutoresizingMaskIntoConstraints = false;
            ads.TranslatesAutoresizingMaskIntoConstraints = false;

            SearchTableView.ScrollEnabled = true;
            SearchTableView.Bounces = false;

            //Scrollview Constraints
            this.View.AddConstraints(new NSLayoutConstraint[] {
                NSLayoutConstraint.Create(holderView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Width, 1, 0),
                NSLayoutConstraint.Create(holderView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(holderView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Top, 1, 64),
                NSLayoutConstraint.Create(holderView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Left, 1, 0),
            });
            //Search Table Constraints
            this.View.AddConstraints(new NSLayoutConstraint[] {
                NSLayoutConstraint.Create (SearchTableView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, holderView, NSLayoutAttribute.Width, 1, 0),
                NSLayoutConstraint.Create (SearchTableView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, holderView, NSLayoutAttribute.CenterX, 1, 0),
                NSLayoutConstraint.Create (SearchTableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, SearchButton, NSLayoutAttribute.Bottom, 1, 15),
            });
            //Search CL Label Constraints
            this.View.AddConstraints(new NSLayoutConstraint[] {
                NSLayoutConstraint.Create(SearchCityLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, holderView, NSLayoutAttribute.Width, 0.9f, 0),
                NSLayoutConstraint.Create(SearchCityLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, holderView, NSLayoutAttribute.CenterX, 1, 0),
                NSLayoutConstraint.Create(SearchCityLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, holderView, NSLayoutAttribute.Top, 1, 15),
            });

            //Search Button Constraints
            this.View.AddConstraints(new NSLayoutConstraint[] {
                NSLayoutConstraint.Create(SearchButton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, holderView, NSLayoutAttribute.Width, .90f, 0),
                NSLayoutConstraint.Create(SearchButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, holderView, NSLayoutAttribute.CenterX, 1, 0),
                NSLayoutConstraint.Create(SearchButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, SearchCityLabel, NSLayoutAttribute.Bottom, 1, 15),
                NSLayoutConstraint.Create(SearchButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, Constants.ButtonHeight),
            });

            AddAdBanner(false);

            this.View.LayoutIfNeeded();
        }


        private void KeyBoardUpNotification(NSNotification notification)
        {
            if (!keyboardSet)
            {
                var val = (NSValue)notification.UserInfo.ValueForKey(UIKeyboard.FrameEndUserInfoKey);
                KeyboardBounds = val.CGRectValue;
                keyboardSet = true;
            }
            if (FieldSelected == null)
                return;

            // Bottom of the controller = initial position + height + offset      
            bottom = (float)(FieldSelected.Frame.Y + FieldSelected.Frame.Height + offset);
            //Added 180 for toolbar, navbar, constraints and padding height, the content offset for amount of scrolled down
            scroll_amount = (float)(KeyboardBounds.Height + 180 - SearchTableView.ContentOffset.Y - (View.Frame.Size.Height - bottom));

            // Perform the scrolling
            if (scroll_amount > 0)
            {
                moveViewUp = true;
                ScrollTheView(moveViewUp);
            }
            else {
                moveViewUp = false;
            }
        }

        private void KeyBoardDownNotification(NSNotification notification)
        {
            if (moveViewUp) { ScrollTheView(false); }
        }

        private void ScrollTheView(bool move)
        {
            // scroll the view up or down
            UIView.BeginAnimations(string.Empty, IntPtr.Zero);
            UIView.SetAnimationDuration(0.2);

            CGRect frame = View.Frame;

            if (move)
            {
                frame.Y -= scroll_amount;
            }
            else {
                frame.Y += scroll_amount;
                scroll_amount = 0;
            }

            View.Frame = frame;
            UIView.CommitAnimations();
        }

        private List<TableItemGroup> GetTableSetup()
        {
            List<TableItemGroup> tableItems = new List<TableItemGroup>();

            TableItemGroup searchterms = new TableItemGroup { Name = "Search Terms" };
            TableItemGroup options = new TableItemGroup { Name = "Options" };

            searchterms.Items.Add(new TableItem
            {
                Heading = "Search Terms",
                CellType = "SearchTermsCell",
            });
            if (Categories.Autos.Contains(Category.Key))
            {
                searchterms.Items.Add(new TableItem
                {
                    Heading = "Make/Model",
                    SubHeading = "make / model",
                    CellType = "MakeModelCell"
                });
                searchterms.Items.Add(new TableItem
                {
                    Heading = "Year",
                    CellType = "MinMaxCell"
                });
                searchterms.Items.Add(new TableItem
                {
                    Heading = "Odometer",
                    CellType = "MinMaxCell"
                });
            }

            if (Categories.Groups.Find(x => x.Name == "Housing").Items.Contains(Category) || Categories.Groups.Find(x => x.Name == "For Sale").Items.Contains(Category))
            {
                searchterms.Items.Add(new TableItem
                {
                    Heading = "Price",
                    CellType = "PriceInputCell",
                });
            }

            if (Categories.Groups.Find(x => x.Name == "Housing").Items.Contains(Category))
            {
                searchterms.Items.Add(new TableItem
                {
                    Heading = "Sq Feet",
                    CellType = "MinMaxCell"
                });
            }

            if (Categories.SubCategories.ContainsKey(Category.Key))
            {
                options.Items.Add(new TableItem
                {
                    Heading = "Sub Category",
                    CellType = "PickerSelectorCell",
                    PickerOptions = new List<PickerOptions>
                            {
                                new PickerOptions {PickerWheelOptions = Categories.SubCategories[Category.Key]}
                            },
                });
            }

            if (Categories.Groups.Find(x => x.Name == "For Sale").Items.Contains(Category) || Categories.Autos.Contains(Category.Key))
            {
                options.Items.Add(new TableItem
                {
                    Heading = "Condition",
                    CellType = "ComboTableCell",
                    SubHeading = "condition",
                    PickerOptions = new List<PickerOptions> { new PickerOptions { PickerWheelOptions = Categories.ComboOptions["condition"] } },
                });
            }

            if (Categories.Groups.Find(x => x.Name == "Jobs").Items.Contains(Category))
            {
                options.Items.Add(new TableItem
                {
                    Heading = "Job Type",
                    CellType = "ComboTableCell",
                    SubHeading = "employment_type",
                    PickerOptions = new List<PickerOptions> { new PickerOptions { PickerWheelOptions = Categories.ComboOptions["employment_type"] } },
                });
            }

            if (Categories.Groups.Find(x => x.Name == "Gigs").Items.Contains(Category))
            {
                options.Items.Add(new TableItem
                {
                    Heading = "Paid",
                    CellType = "ComboTableCell",
                    SubHeading = "is_paid",
                    PickerOptions = new List<PickerOptions> { new PickerOptions { PickerWheelOptions = Categories.ComboOptions["is_paid"] } },
                });
            }

            if (Categories.Autos.Contains(Category.Key))
            {
                options.Items.Add(new TableItem
                {
                    Heading = "Cylinders",
                    CellType = "ComboTableCell",
                    SubHeading = "auto_cylinders",
                    PickerOptions = new List<PickerOptions> { new PickerOptions { PickerWheelOptions = Categories.ComboOptions["auto_cylinders"] } },
                });
                options.Items.Add(new TableItem
                {
                    Heading = "Drive",
                    CellType = "ComboTableCell",
                    SubHeading = "auto_drivetrain",
                    PickerOptions = new List<PickerOptions> { new PickerOptions { PickerWheelOptions = Categories.ComboOptions["auto_drivetrain"] } },
                });
                options.Items.Add(new TableItem
                {
                    Heading = "Fuel",
                    CellType = "ComboTableCell",
                    SubHeading = "auto_fuel_type",
                    PickerOptions = new List<PickerOptions> { new PickerOptions { PickerWheelOptions = Categories.ComboOptions["auto_fuel_type"] } },
                });
                options.Items.Add(new TableItem
                {
                    Heading = "Paint Color",
                    CellType = "ComboTableCell",
                    SubHeading = "auto_paint",
                    PickerOptions = new List<PickerOptions> { new PickerOptions { PickerWheelOptions = Categories.ComboOptions["auto_paint"] } },
                });
                options.Items.Add(new TableItem
                {
                    Heading = "Title Status",
                    CellType = "ComboTableCell",
                    SubHeading = "auto_title_status",
                    PickerOptions = new List<PickerOptions> { new PickerOptions { PickerWheelOptions = Categories.ComboOptions["auto_title_status"] } },
                });
                options.Items.Add(new TableItem
                {
                    Heading = "Transmission",
                    CellType = "ComboTableCell",
                    SubHeading = "auto_transmission",
                    PickerOptions = new List<PickerOptions> { new PickerOptions { PickerWheelOptions = Categories.ComboOptions["auto_transmission"] } },
                });
            }

            if (Categories.Housing.Contains(Category.Key))
            {
                options.Items.Add(new TableItem
                {
                    Heading = "Min Bedrooms",
                    CellType = "PickerSelectorCell",
                    PickerOptions = new List<PickerOptions>
                            {
                                new PickerOptions
                                {PickerWheelOptions = new List<KeyValuePair<object, object>>
                                    {
                                        new KeyValuePair<object, object>("Any", null),
                                        new KeyValuePair<object, object>("1+", "1"),
                                        new KeyValuePair<object, object>("2+", "2"),
                                        new KeyValuePair<object, object>("3+", "3"),
                                        new KeyValuePair<object, object>("4+", "4"),
                                    }
                                }
                            },
                });
                options.Items.Add(new TableItem
                {
                    Heading = "Min Bathrooms",
                    CellType = "PickerSelectorCell",
                    PickerOptions = new List<PickerOptions>
                            {
                                new PickerOptions
                                {PickerWheelOptions = new List<KeyValuePair<object, object>>
                                    {
                                        new KeyValuePair<object, object>("Any", null),
                                        new KeyValuePair<object, object>("1+", "1"),
                                        new KeyValuePair<object, object>("2+", "2"),
                                        new KeyValuePair<object, object>("3+", "3"),
                                        new KeyValuePair<object, object>("4+", "4"),
                                    }
                                }
                            },
                });
            }
            options.Items.Add(new TableItem
            {
                Heading = "Posted Date",
                CellType = "PickerSelectorCell",
                PickerOptions = new List<PickerOptions>
                        {
                            new PickerOptions
                            {PickerWheelOptions = new List<KeyValuePair<object, object>>
                                {
                                    new KeyValuePair<object, object>("Any", null),
                                    new KeyValuePair<object, object>("Today", "-1"),
                                    new KeyValuePair<object, object>("1 Week Old", "1"),
                                    new KeyValuePair<object, object>("2 Weeks Old", "2"),
                                    new KeyValuePair<object, object>("3 Weeks Old", "3"),
                                    new KeyValuePair<object, object>("4 Weeks Old", "4"),
                                }
                            }
                        },
            });
            options.Items.Add(new TableItem
            {
                Heading = "Max Listings",
                CellType = "PickerSelectorCell",
                PickerOptions = new List<PickerOptions>
                    {
                        new PickerOptions{PickerWheelOptions = new List<KeyValuePair<object, object>>
                            {
                                new KeyValuePair<object, object>(25, 25),
                                new KeyValuePair<object, object>(50, 50),
                                new KeyValuePair<object, object>(75, 75),
                                new KeyValuePair<object, object>(100, 100),
                            }}
                    },
            });

            tableItems.Add(searchterms);
            tableItems.Add(options);

            return tableItems;
        }
    }
}

