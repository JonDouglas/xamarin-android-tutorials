using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Lollipop
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        private int NOTIFY_ID = 1337;
        public static string EXTRA_TYPE = "type";
        public override void OnReceive(Context context, Intent intent)
        {
            NotificationManager manager = NotificationManager.FromContext(context);

            switch (intent.GetIntExtra(EXTRA_TYPE, -1))
            {
                case 0:
                    NotifyPrivate(context, manager);
                    break;
                case 1:
                    NotifyPublic(context, manager);
                    break;
                case 2:
                    NotifySecret(context, manager);
                    break;
                case 3:
                    NotifyHeadsUp(context, manager);
                    break;
            }
        }

        private void NotifyPrivate(Context context, NotificationManager manager)
        {
            Notification pubNotification = BuildPublic(context).Build();

            manager.Notify(NOTIFY_ID, BuildNormal(context).SetPublicVersion(pubNotification).Build());
        }

        private void NotifyPublic(Context context, NotificationManager manager)
        {
            manager.Notify(NOTIFY_ID, BuildNormal(context).SetVisibility(NotificationVisibility.Public).Build());
        }

        private void NotifySecret(Context context, NotificationManager manager)
        {
            manager.Notify(NOTIFY_ID, BuildNormal(context).SetVisibility(NotificationVisibility.Secret).Build());
        }

        private void NotifyHeadsUp(Context context, NotificationManager manager)
        {
            manager.Notify(NOTIFY_ID, BuildNormal(context).SetPriority((int)NotificationPriority.High).Build());
        }

        private Notification.Builder BuildNormal(Context context)
        {
            Notification.Builder builder = new Notification.Builder(context);

            builder.SetAutoCancel(true)
                .SetDefaults(NotificationDefaults.All)
                .SetContentTitle("Complete")
                .SetContentText("Fun")
                .SetContentIntent(BuildPendingIntent(context, Settings.ActionSecuritySettings))
                .SetSmallIcon(Resource.Drawable.Icon)
                .SetTicker("Complete")
                .AddAction(Resource.Drawable.Icon, "Play", BuildPendingIntent(context, Settings.ActionSettings));

            return builder;
        }

        private Notification.Builder BuildPublic(Context context)
        {
            Notification.Builder builder = new Notification.Builder(context);

            builder.SetAutoCancel(true)
                .SetDefaults(NotificationDefaults.All)
                .SetContentTitle("Complete")
                .SetContentText("Fun")
                .SetContentIntent(BuildPendingIntent(context, Settings.ActionSecuritySettings))
                .SetSmallIcon(Resource.Drawable.Icon)
                .AddAction(Resource.Drawable.Icon, "Play", BuildPendingIntent(context, Settings.ActionSettings));

            return builder;
        }

        private PendingIntent BuildPendingIntent(Context context, string action)
        {
            Intent i = new Intent(action);
            return (PendingIntent.GetActivity(context, 0, i, 0));
        }
    }
}