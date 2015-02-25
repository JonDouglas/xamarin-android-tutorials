using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;

namespace DynamicFragments
{
    [Activity(Label = "DynamicFragmentsDemoActivity", MainLauncher = true, Icon = "@drawable/icon")]
    public class DynamicFragmentsDemoActivity : LifecycleLoggingActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.main);
        }

        public void ShowOther(View view)
        {
            Intent other = new Intent(this, typeof(OtherActivity));
            StartActivity(other);
        }
    }
}