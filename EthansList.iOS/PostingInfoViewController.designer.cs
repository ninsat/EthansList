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
	[Register ("PostingInfoViewController")]
	partial class PostingInfoViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView PostingInfoTableView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (PostingInfoTableView != null) {
				PostingInfoTableView.Dispose ();
				PostingInfoTableView = null;
			}
		}
	}
}