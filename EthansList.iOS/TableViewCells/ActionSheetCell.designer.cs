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
	[Register ("ActionSheetCell")]
	partial class ActionSheetCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel Heading { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel MinLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (Heading != null) {
				Heading.Dispose ();
				Heading = null;
			}
			if (MinLabel != null) {
				MinLabel.Dispose ();
				MinLabel = null;
			}
		}
	}
}