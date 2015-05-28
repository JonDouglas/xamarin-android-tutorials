using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Android.OS;

namespace BrowserDemo1
{
    [Activity(Label = "BrowserDemo1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private WebView browser;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            browser = (WebView) FindViewById<WebView>(Resource.Id.webkit);

            browser.LoadUrl(@"https://www.google.com/maps/place/Disneyland+Park,+1313+Disneyland+Dr,+Anaheim,+CA+92802/@33.812092,-117.918974,17z/data=!4m2!3m1!1s0x80dcd7d12b3b5e6b:0x2ef62f8418225cfa");
        }
    }
}

