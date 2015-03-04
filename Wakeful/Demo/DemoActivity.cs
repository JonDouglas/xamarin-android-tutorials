using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Demo
{
    [Activity(Label = "Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class DemoActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            WakefulIntentService.WakefulIntentService.ScheduleAlarms(new AppListener(), this, false);

            Toast.MakeText(this, "Alarms active!", ToastLength.Long).Show();

            Finish();
        }
    }
}

