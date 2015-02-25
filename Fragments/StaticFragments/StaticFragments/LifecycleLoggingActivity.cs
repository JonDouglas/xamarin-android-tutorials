using Android.App;
using Android.OS;
using Android.Util;

namespace StaticFragments
{
    [Activity(Label = "LifecycleLoggingActivity")]
    public class LifecycleLoggingActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Log.Debug(Class.SimpleName, "onCreate()");
        }

        protected override void OnRestart()
        {
            base.OnRestart();

            Log.Debug(Class.SimpleName, "onRestart()");
        }

        protected override void OnStart()
        {
            base.OnStart();

            Log.Debug(Class.SimpleName, "onStart()");
        }

        protected override void OnResume()
        {
            base.OnResume();

            Log.Debug(Class.SimpleName, "onResume()");
        }

        protected override void OnPause()
        {
            base.OnResume();

            Log.Debug(Class.SimpleName, "onPause()");
        }

        protected override void OnStop()
        {
            base.OnResume();

            Log.Debug(Class.SimpleName, "onStop()");
        }
        protected override void OnDestroy()
        {
            base.OnResume();

            Log.Debug(Class.SimpleName, "onDestroy()");
        }
    }
}