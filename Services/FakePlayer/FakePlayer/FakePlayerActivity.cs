using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace FakePlayer
{
    [Activity(Label = "FakePlayer", MainLauncher = true, Icon = "@drawable/icon")]
    public class FakePlayerActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (FragmentManager.FindFragmentById(Android.Resource.Id.Content) == null)
            {
                FragmentManager.BeginTransaction().Add(Android.Resource.Id.Content, new PlayerFragment()).Commit();
            }
        }
    }
}

