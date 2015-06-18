using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Gms.Maps.Model;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Interop;
using Java.Lang;
using Java.Util;
using Exception = System.Exception;

namespace Geofence
{
    [Activity(Label = "Geofence", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.Base")]
    public class MainActivity : ActionBarActivity, IGoogleApiClientConnectionCallbacks, IGoogleApiClientOnConnectionFailedListener, IResultCallback
    {
        protected static string TAG = "creating-and-monitoring-geofences";

        protected IGoogleApiClient mGoogleApiClient;

        protected IList<IGeofence> mGeofenceList;

        private bool mGeofencesAdded;

        private PendingIntent mGeofencePendingIntent;

        private ISharedPreferences mSharedPreferences;

        private Button mAddGeofencesButton;
        private Button mRemoveGeofencesButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mAddGeofencesButton = (Button) FindViewById(Resource.Id.add_geofences_button);
            mRemoveGeofencesButton = (Button) FindViewById(Resource.Id.remove_geofences_button);

            mGeofenceList = new List<IGeofence>();

            mGeofencePendingIntent = null;

            mSharedPreferences = GetSharedPreferences(Constants.SHARED_PREFERENCES_NAME, FileCreationMode.Private);

            mGeofencesAdded = mSharedPreferences.GetBoolean(Constants.GEOFENCES_ADDED_KEY, false);
            SetButtonsEnabledState();
            PopulateGeofenceList();
            BuildGoogleApiClient();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected void BuildGoogleApiClient()
        {
            mGoogleApiClient = new GoogleApiClientBuilder(this)
                .AddConnectionCallbacks(this)
                .AddOnConnectionFailedListener(this)
                .AddApi(LocationServices.API)
                .Build();
        }

        protected override void OnStart()
        {
            base.OnStart();
            mGoogleApiClient.Connect();
        }

        protected override void OnStop()
        {
            base.OnStop();
            mGoogleApiClient.Disconnect();
        }

        public void OnResult(Java.Lang.Object rawStatus)
        {
            var status = Extensions.JavaCast<IResult>(rawStatus);
            if (status.Status.IsSuccess)
            {
                mGeofencesAdded = !mGeofencesAdded;
                ISharedPreferencesEditor editor = mSharedPreferences.Edit();
                editor.PutBoolean(Constants.GEOFENCES_ADDED_KEY, mGeofencesAdded);
                editor.Commit();

                SetButtonsEnabledState();

                Toast.MakeText(this, GetString(mGeofencesAdded ? Resource.String.geofences_added : Resource.String.geofences_removed), ToastLength.Short).Show();
            }
            else
            {
                string errorMessage = GeofenceErrorMessages.GetErrorString(this, status.Status.StatusCode);
                Log.Error(TAG, errorMessage);
            }
        }

        public void OnConnectionFailed(Android.Gms.Common.ConnectionResult result)
        {
            Log.Info(TAG, "Connection failed: ConnectionResult.getErrorCode() = " + result.ErrorCode);
        }

        public void OnConnected(Bundle connectionHint)
        {
            Log.Info(TAG, "Connected to GoogleApiClient");
        }

        public void OnConnectionSuspended(int cause)
        {
            Log.Info(TAG, "Connection suspended");
        }

        private GeofencingRequest GetGeofencingRequest()
        {
            GeofencingRequest.Builder builder = new GeofencingRequest.Builder();

            builder.SetInitialTrigger(GeofencingRequest.InitialTriggerEnter);

            builder.AddGeofences(mGeofenceList);

            return builder.Build();
        }

        [Export("AddGeofencesButtonHandler")]
        public void AddGeofencesButtonHandler(View view)
        {
            if (!mGoogleApiClient.IsConnected)
            {
                Toast.MakeText(this, GetString(Resource.String.not_connected), ToastLength.Short).Show();
                return;
            }

            try
            {
                LocationServices.GeofencingApi.AddGeofences(mGoogleApiClient, GetGeofencingRequest(),
                    GetGeofencePendingIntent()).SetResultCallback(this);
            }
            catch (SecurityException securityException)
            {
                LogSecurityException(securityException);
            }
        }

        [Export("RemoveGeofencesButtonHandler")]
        public void RemoveGeofencesButtonHandler(View view)
        {
            if (!mGoogleApiClient.IsConnected)
            {
                Toast.MakeText(this, GetString(Resource.String.not_connected), ToastLength.Short).Show();
                return;
            }

            try
            {
                LocationServices.GeofencingApi.RemoveGeofences(mGoogleApiClient, GetGeofencePendingIntent()).SetResultCallback(this);
            }
            catch (SecurityException securityException)
            {
                LogSecurityException(securityException);
            }
        }

        private void LogSecurityException(SecurityException securityException)
        {
            Log.Error(TAG, "Invalid location permission. " +
                "You need to use ACCESS_FINE_LOCATION with geofences", securityException);
        }

        private PendingIntent GetGeofencePendingIntent()
        {
            if (mGeofencePendingIntent != null)
            {
                return mGeofencePendingIntent;
            }
            Intent intent = new Intent(this, typeof(GeofenceTransitionsIntentService));

            return PendingIntent.GetService(this, 0, intent, PendingIntentFlags.UpdateCurrent);
        }

        public void PopulateGeofenceList()
        {
            foreach (var entry in Constants.BAY_AREA_LANDMARKS)
            {
                mGeofenceList.Add(new GeofenceBuilder()
                    .SetRequestId(entry.Key)
                    .SetCircularRegion(entry.Value.Latitude,
                        entry.Value.Longitude,
                        Constants.GEOFENCE_RADIUS_IN_METERS)
                    .SetExpirationDuration(Constants.GEOFENCE_EXPIRATION_IN_MILLISECONDS)
                    .SetTransitionTypes(Android.Gms.Location.Geofence.GeofenceTransitionEnter |
                                        Android.Gms.Location.Geofence.GeofenceTransitionExit)
                    .Build());
            }
        }

        private void SetButtonsEnabledState()
        {
            if (mGeofencesAdded)
            {
                mAddGeofencesButton.Enabled = false;
                mRemoveGeofencesButton.Enabled = true;
            }
            else
            {
                mAddGeofencesButton.Enabled = true;
                mRemoveGeofencesButton.Enabled = false;
            }
        }
    }
}

