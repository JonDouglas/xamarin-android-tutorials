using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Gms.Location;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Geofence
{
    public class GeofenceErrorMessages
    {
        private GeofenceErrorMessages()
        {
            
        }

        public static string GetErrorString(Context context, int errorCode)
        {
            Resources mResources = context.Resources;
            switch (errorCode)
            {
                case GeofenceStatusCodes.GeofenceNotAvailable:
                    return mResources.GetString(Resource.String.geofence_not_available);
                case GeofenceStatusCodes.GeofenceTooManyGeofences:
                    return mResources.GetString(Resource.String.geofence_too_many_geofences);
                case GeofenceStatusCodes.GeofenceTooManyPendingIntents:
                    return mResources.GetString(Resource.String.geofence_too_many_pending_intents);
                default:
                    return mResources.GetString(Resource.String.unknown_geofence_error);
            }
        }
    }
}