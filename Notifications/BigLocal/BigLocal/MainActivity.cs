using System;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace BigLocal
{
    [Activity(Label = "BigLocal", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int NOTIFY_ID = 1337;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            NotificationManager manager = (NotificationManager) GetSystemService(Context.NotificationService);
            Notification.Builder builder = BuildNormal();
            Notification.InboxStyle bigStyle = new Notification.InboxStyle(builder);

            manager.Notify(NOTIFY_ID, bigStyle.SetSummaryText("Summary Text Here")
                .AddLine("Here is entry 1")
                .AddLine("Here is entry 2")
                .AddLine("Here is entry 3")
                .AddLine("Here is entry 4")
                .Build());

            Finish();
        }

        private Notification.Builder BuildNormal()
        {
            Notification.Builder builder = new Notification.Builder(this);

            builder.SetAutoCancel(true)
                .SetDefaults(NotificationDefaults.All)
                .SetContentTitle("Complete")
                .SetContentText("Fun")
                .SetContentIntent(BuildPendingIntent(Settings.ActionSecuritySettings))
                .SetSmallIcon(Resource.Drawable.Icon)
                .SetTicker("Complete")
                .SetPriority((int) NotificationPriority.High)
                .SetLocalOnly(true)
                .AddAction(Resource.Drawable.Icon, "Play", BuildPendingIntent(Settings.ActionSettings));

            return builder;
        }

        private PendingIntent BuildPendingIntent(string action)
        {
            Intent i = new Intent(action);
            return (PendingIntent.GetActivity(this, 0, i, 0));
        }
    }
}

