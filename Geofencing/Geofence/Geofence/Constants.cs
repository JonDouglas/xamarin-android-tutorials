using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace Geofence
{
    public class Constants
    {
        private Constants()
        {
            
        }

        public static string PACKAGE_NAME = "com.google.android.gms.location.Geofence";

        public static string SHARED_PREFERENCES_NAME = PACKAGE_NAME + ".SHARED_PREFERENCES_NAME";

        public static string GEOFENCES_ADDED_KEY = PACKAGE_NAME + ".GEOFENCES_ADDED_KEY";

        public static long GEOFENCE_EXPIRATION_IN_HOURS = 12;

        public static long GEOFENCE_EXPIRATION_IN_MILLISECONDS = GEOFENCE_EXPIRATION_IN_HOURS * 60 * 60 * 1000;

        public static float GEOFENCE_RADIUS_IN_METERS = 1609;

        public static Dictionary<string, LatLng> BAY_AREA_LANDMARKS = new Dictionary<string, LatLng>()
        {
            {"SFO", new LatLng(37.621313, -122.378955)},
            {"GOOGLE", new LatLng(37.422611, -122.0840577)}
        };

        public static Dictionary<string, LatLng> HOME_LANDMARKS = new Dictionary<string, LatLng>()
        {
            {"HOUSE", new LatLng(41.169045, -111.986887)},
            {"PARK", new LatLng(41.169118, -111.990481)}
        };

            
    }
}