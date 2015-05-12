using System;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Markers
{
    [Activity(Label = "Markers", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light.DarkActionBar")]
    public class MainActivity : AbstractMapActivity, IOnMapReadyCallback
    {
        private bool needsInit = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            if (ReadyToGo())
            {
                SetContentView(Resource.Layout.Main);

                MapFragment mapFrag = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);

                if (bundle == null)
                {
                    needsInit = true;
                }

                mapFrag.GetMapAsync(this);
            }
        }

        public void OnMapReady(GoogleMap map)
        {
            if (needsInit)
            {
                CameraUpdate center = CameraUpdateFactory.NewLatLng(new LatLng(40.76793169992044, -73.98180484771729));
                CameraUpdate zoom = CameraUpdateFactory.ZoomTo(15);
                map.MoveCamera(center);
                map.AnimateCamera(zoom);
            }

            AddMarker(map, 40.748963847316034, -73.96807193756104,
                Resource.String.un, Resource.String.united_nations);
            AddMarker(map, 40.76866299974387, -73.98268461227417,
                Resource.String.lincoln_center,
                Resource.String.lincoln_center_snippet);
            AddMarker(map, 40.765136435316755, -73.97989511489868,
                Resource.String.carnegie_hall, Resource.String.practice_x3);
            AddMarker(map, 40.70686417491799, -74.01572942733765,
                Resource.String.downtown_club, Resource.String.heisman_trophy);
        }

        private void AddMarker(GoogleMap map, double lat, double lon, int title, int snippet)
        {
            MarkerOptions options = new MarkerOptions();
            options.InvokePosition(new LatLng(lat, lon));
            options.InvokeTitle(GetString(title));
            options.InvokeSnippet(GetString(snippet));

            map.AddMarker(options);
        }
    }
}

