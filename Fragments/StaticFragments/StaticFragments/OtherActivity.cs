using Android.App;
using Android.OS;
using Android.Widget;

namespace StaticFragments
{
    [Activity(Label = "OtherActivity")]
    public class OtherActivity : LifecycleLoggingActivity
    {
        public const string EXTRA_MESSAGE = "msg";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.other);

            TextView textView = FindViewById<TextView>(Resource.Id.msg);
            textView.Text = Intent.GetStringExtra(EXTRA_MESSAGE);
        }
    }
}

