using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace DynamicFragments
{
    public class OtherFragment : ListFragment
    {

        private string[] items = { "lorem", "ipsum", "dolor",
      "sit", "amet", "consectetuer", "adipiscing", "elit", "morbi",
      "vel", "ligula", "vitae", "arcu", "aliquet", "mollis", "etiam",
      "vel", "erat", "placerat", "ante", "porttitor", "sodales",
      "pellentesque", "augue", "purus" };

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            ListAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, items);
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