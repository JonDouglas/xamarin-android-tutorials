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

namespace Demo
{
    public class AppService : WakefulIntentService.WakefulIntentService
    {
        public AppService() : base("AppService")
        {
            
        }

        protected override void DoWakefulWork(Intent intent)
        {
            Log.Info("AppService", "I'm awake! I'm awake!");
        }
    }
}