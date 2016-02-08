using System;
using Android.Webkit;
using Android.Widget;
using Java.Interop;

namespace Payment
{
	public class PaymentInterface : Java.Lang.Object
	{
		PaymentActivity act;

		public PaymentInterface(PaymentActivity act)
		{
			this.act = act;
		}

		[JavascriptInterface]
		[Export]
		public void showMeSmth()
		{
			act.showMsg();
		}
	}
}

