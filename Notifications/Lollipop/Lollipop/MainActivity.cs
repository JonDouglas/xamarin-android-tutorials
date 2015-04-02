using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Interop;

namespace Lollipop
{
    [Activity(Label = "Lollipop", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Spinner typeSpinner;
        private SeekBar delaySeekBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            typeSpinner = FindViewById<Spinner>(Resource.Id.type);

            ArrayAdapter<string> typesAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Resources.GetStringArray(Resource.Array.types));

            typesAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            typeSpinner.Adapter = typesAdapter;

            delaySeekBar = FindViewById<SeekBar>(Resource.Id.delay);
        }

        [Export("NotifyMe")]
        public void NotifyMe(View view)
        {
            Intent i = new Intent(this, typeof(AlarmReceiver)).PutExtra(AlarmReceiver.EXTRA_TYPE, typeSpinner.SelectedItemPosition);
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, i, PendingIntentFlags.UpdateCurrent);
            AlarmManager manager = (AlarmManager)GetSystemService(Application.AlarmService);

            manager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + (1000 * delaySeekBar.Progress), pendingIntent);
        }
    }
}

