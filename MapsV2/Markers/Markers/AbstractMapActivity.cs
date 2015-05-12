using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Markers
{
    [Activity(Label = "AbstractMapActivity")]
    public class AbstractMapActivity : Activity
    {
        protected static string TAG_ERROR_DIALOG_FRAGMENT = "errorDialog";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.activity_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.legal)
            {
                StartActivity(new Intent(this, typeof(LegalNoticesActivity)));
            }
            return base.OnOptionsItemSelected(item);
        }

        protected bool ReadyToGo()
        {
            int status = GooglePlayServicesUtil.IsGooglePlayServicesAvailable(this);

            if (status == ConnectionResult.Success)
            {
                if (GetVersionFromPackageManager(this) >= 2)
                {
                    return true;
                }
                else
                {
                    Toast.MakeText(this, Resource.String.no_maps, ToastLength.Long).Show();
                    Finish();
                }
            }
            else if (GooglePlayServicesUtil.IsUserRecoverableError(status))
            {
                ErrorDialogFragment e = new ErrorDialogFragment();

                e.NewInstance(status).Show(FragmentManager, TAG_ERROR_DIALOG_FRAGMENT);
            }
            else
            {
                Toast.MakeText(this, Resource.String.no_maps, ToastLength.Long).Show();
                Finish();
            }

            return false;
        }

        public class ErrorDialogFragment : DialogFragment
        {
            private static string ARG_STATUS = "status";

            public ErrorDialogFragment NewInstance(int status)
            {
                Bundle args = new Bundle();

                args.PutInt(ARG_STATUS, status);

                ErrorDialogFragment result = new ErrorDialogFragment();

                result.Arguments = args;

                return result;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                Bundle args = Arguments;
                return GooglePlayServicesUtil.GetErrorDialog(args.GetInt(ARG_STATUS), Activity, 0);
            }

            public override void OnDismiss(IDialogInterface dialog)
            {
                if (Activity != null)
                {
                    Activity.Finish();
                }
            }
        }

        private static int GetVersionFromPackageManager(Context context)
        {
            Android.Content.PM.PackageManager packageManager = context.PackageManager;

            FeatureInfo[] featureInfos = packageManager.GetSystemAvailableFeatures();

            if (featureInfos != null && featureInfos.Length > 0)
            {
                foreach (var f in featureInfos)
                {
                    if (f.Name == null)
                    {
                        if (f.ReqGlEsVersion != FeatureInfo.GlEsVersionUndefined)
                        {
                            return GetMajorVersion(f.ReqGlEsVersion);
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
            }

            return 1;

        }

        private static int GetMajorVersion(int glEsVersion)
        {
            return (int)((glEsVersion & 0xffff0000) >> 16);
        }

    }

}