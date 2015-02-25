using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;

namespace StaticFragments
{
    public class ContentFragment : Fragment, View.IOnClickListener
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View result = inflater.Inflate(Resource.Layout.mainfrag, container, false);

            result.FindViewById(Resource.Id.showOther).SetOnClickListener(this);

            return result;
        }

        public void OnClick(View v)
        {
            ((StaticFragmentsDemoActivity) Activity).ShowOther(v);
        }

        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Log.Debug(Class.SimpleName, "onCreate()");
        }

        public override void OnStart()
        {
            base.OnStart();

            Log.Debug(Class.SimpleName, "onStart()");
        }

        public override void OnResume()
        {
            base.OnResume();

            Log.Debug(Class.SimpleName, "onResume()");
        }

        public override void OnPause()
        {
            base.OnResume();

            Log.Debug(Class.SimpleName, "onPause()");
        }

        public override void OnStop()
        {
            base.OnResume();

            Log.Debug(Class.SimpleName, "onStop()");
        }
        public override void OnDestroy()
        {
            base.OnResume();

            Log.Debug(Class.SimpleName, "onDestroy()");
        }
    }
}