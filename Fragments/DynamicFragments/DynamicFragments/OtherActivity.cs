using Android.App;
using Android.OS;

namespace DynamicFragments
{
    [Activity(Label = "OtherActivity")]
    public class OtherActivity : LifecycleLoggingActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (FragmentManager.FindFragmentById(Android.Resource.Id.Content) == null)
            {
                FragmentManager.BeginTransaction().Add(Android.Resource.Id.Content, new OtherFragment()).Commit();
            }
        }
    }
}

