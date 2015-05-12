using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Markers
{
    [Activity(Label = "LegalNoticesActivity")]
    public class LegalNoticesActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.legal);

            TextView legal = FindViewById<TextView>(Resource.Id.legal);

            legal.Text = GooglePlayServicesUtil.GetOpenSourceSoftwareLicenseInfo(this);
        }
    }
}