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

namespace ethanslist.ios
{
	[Register ("SavedListingDetailsViewController")]
	partial class SavedListingDetailsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel dateLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem DeleteButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem DoneButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel PostingDescription { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView postingImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel PostingTitle { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (dateLabel != null) {
				dateLabel.Dispose ();
				dateLabel = null;
			}
			if (DeleteButton != null) {
				DeleteButton.Dispose ();
				DeleteButton = null;
			}
			if (DoneButton != null) {
				DoneButton.Dispose ();
				DoneButton = null;
			}
			if (PostingDescription != null) {
				PostingDescription.Dispose ();
				PostingDescription = null;
			}
			if (postingImageView != null) {
				postingImageView.Dispose ();
				postingImageView = null;
			}
			if (PostingTitle != null) {
				PostingTitle.Dispose ();
				PostingTitle = null;
			}
		}
	}
}