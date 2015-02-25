using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;

namespace StaticFragments
{
    [Activity(Label = "StaticFragmentsDemoActivity", MainLauncher = true, Icon = "@drawable/icon")]
    public class StaticFragmentsDemoActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.main);
        }

        public void ShowOther(View view)
        {
            Intent other = new Intent(this, typeof(OtherActivity));

            other.PutExtra(OtherActivity.EXTRA_MESSAGE, GetString(Resource.String.other));
            StartActivity(other);
        }
    }
}