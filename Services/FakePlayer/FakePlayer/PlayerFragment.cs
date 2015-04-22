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

namespace FakePlayer
{
    public class PlayerFragment : Fragment, View.IOnClickListener
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View result = inflater.Inflate(Resource.Layout.Main, container, false);

            result.FindViewById(Resource.Id.start).SetOnClickListener(this);
            result.FindViewById(Resource.Id.stop).SetOnClickListener(this);

            return result;
        }

        public void OnClick(View v)
        {
            Intent i = new Intent(Activity, typeof(PlayerService));

            if (v.Id == Resource.Id.start)
            {
                i.PutExtra(PlayerService.EXTRA_PLAYLIST, "main");
                i.PutExtra(PlayerService.EXTRA_SHUFFLE, true);

                Activity.StartService(i);
            }
            else
            {
                Activity.StopService(i);
            }
        }
    }
}