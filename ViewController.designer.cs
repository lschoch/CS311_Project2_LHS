// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Craps
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSImageView imgDie1 { get; set; }

		[Outlet]
		AppKit.NSImageView imgDie2 { get; set; }

		[Outlet]
		AppKit.NSTextField lblBalance { get; set; }

		[Outlet]
		AppKit.NSTextField lblResult { get; set; }

		[Outlet]
		AppKit.NSTextField txtEnterBet { get; set; }

		[Action ("btnRoll:")]
		partial void btnRoll (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (imgDie1 != null) {
				imgDie1.Dispose ();
				imgDie1 = null;
			}

			if (imgDie2 != null) {
				imgDie2.Dispose ();
				imgDie2 = null;
			}

			if (lblBalance != null) {
				lblBalance.Dispose ();
				lblBalance = null;
			}

			if (lblResult != null) {
				lblResult.Dispose ();
				lblResult = null;
			}

			if (txtEnterBet != null) {
				txtEnterBet.Dispose ();
				txtEnterBet = null;
			}
		}
	}
}
