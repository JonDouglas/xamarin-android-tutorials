using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util;
using String = System.String;
using TaskStackBuilder = Android.App.TaskStackBuilder;

namespace Geofence
{
    [Service]
    public class GeofenceTransitionsIntentService : IntentService
    {
        protected static string TAG = "geofence-transitions-service";

        public GeofenceTransitionsIntentService() : base(TAG)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }

        protected override void OnHandleIntent(Intent intent)
        {
            GeofencingEvent geofencingEvent = GeofencingEvent.FromIntent(intent);
            if (geofencingEvent.HasError)
            {
                string errorMessage = GeofenceErrorMessages.GetErrorString(this, geofencingEvent.ErrorCode);
                Log.Error(TAG, errorMessage);
                return;
            }

            int geofenceTransition = geofencingEvent.GeofenceTransition;

            if (geofenceTransition == Android.Gms.Location.Geofence.GeofenceTransitionEnter ||
                geofenceTransition == Android.Gms.Location.Geofence.GeofenceTransitionExit)
            {
                var triggeringGeofences = geofencingEvent.TriggeringGeofences;

                String geofenceTransitionDetails = GetGeofenceTransitionDetails(this, geofenceTransition,
                    triggeringGeofences);

                SendNotification(geofenceTransitionDetails);
                Log.Info(TAG, geofenceTransitionDetails);
            }
            else
            {
                Log.Error(TAG, GetString(Resource.String.geofence_transition_invalid_type, geofenceTransition));
            }
        }
        private string GetGeofenceTransitionDetails(Context Context, int geofenceTransition, IEnumerable<IGeofence> triggeringGeofences)
        {
            string geofenceTransitionString = GetTransitionString(geofenceTransition);

            ArrayList triggeringGeofencesIdsList = new ArrayList();
            foreach (var geofence in triggeringGeofences)
            {
                triggeringGeofencesIdsList.Add(geofence.RequestId);
            }

            string triggeringGeofencesIdsString = TextUtils.Join(", ", triggeringGeofencesIdsList);

            return geofenceTransitionString + ": " + triggeringGeofencesIdsString;
        }

        private void SendNotification(string notificationDetails)
        {
            Intent notificationIntent = new Intent(ApplicationContext, typeof(MainActivity));

            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(this);

            stackBuilder.AddParentStack(Class.FromType(typeof(MainActivity)));

            stackBuilder.AddNextIntent(notificationIntent);

            PendingIntent notificationPendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);

            builder.SetSmallIcon(Resource.Drawable.Icon)
                .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.Icon))
                .SetColor(Color.Red)
                .SetContentTitle(notificationDetails)
                .SetContentText(GetString(Resource.String.geofence_transition_notification_text))
                .SetContentIntent(notificationPendingIntent);

            builder.SetAutoCancel(true);

            NotificationManager mNotificationManager =
                (NotificationManager) GetSystemService(Context.NotificationService);

            mNotificationManager.Notify(0, builder.Build());
        }

        private string GetTransitionString(int transitionType)
        {
            switch (transitionType)
            {
                case Android.Gms.Location.Geofence.GeofenceTransitionEnter:
                    return GetString(Resource.String.geofence_transition_entered);
                case Android.Gms.Location.Geofence.GeofenceTransitionExit:
                    return GetString(Resource.String.geofence_transition_exited);
                default:
                    return GetString(Resource.String.unknown_geofence_transition);
            }
        }
    }
}