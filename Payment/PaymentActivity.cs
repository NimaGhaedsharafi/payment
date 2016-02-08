
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

namespace Payment
{
	[Activity(Label = "PaymentActivity")]			
	public class PaymentActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.payment);

			var wv = FindViewById<WebView>(Resource.Id.webView1);

			wv.SetWebViewClient(new myWebClient());
			wv.Settings.JavaScriptEnabled = true;
			wv.AddJavascriptInterface(new PaymentInterface(this), "CSharp");

			var data = string.Format("email={0}&name={1}", "me@nifi.ir", "Nima Ghaedsharfi");

			wv.PostUrl("http://192.168.1.5:81/payment", UTF8Encoding.UTF8.GetBytes(data));

		}

		public void showMsg()
		{
			Toast.MakeText(this, "Hello World from Javascript", ToastLength.Long).Show();
			Finish();
		}
	}

	class myWebClient:WebViewClient
	{
		public override bool ShouldOverrideUrlLoading(WebView view, string url)
		{
			view.LoadUrl(url);

			return false;
		}
	}
}

