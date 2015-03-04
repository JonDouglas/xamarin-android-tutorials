using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Demo
{
    public class AppListener : WakefulIntentService.WakefulIntentService.IAlarmListener
    {
        public void ScheduleAlarms(AlarmManager alarmManager, PendingIntent pendingIntent, Context context)
        {
            alarmManager.SetInexactRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + 6000, AlarmManager.IntervalFifteenMinutes, pendingIntent);
        }

        public void SendWakefulWork(Context context)
        {
            WakefulIntentService.WakefulIntentService.SendWakefulWork(context, typeof(AppService));
        }

        public long GetMaxAge()
        {
            return (AlarmManager.IntervalFifteenMinutes*2);
        }
    }
}