using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Exception = System.Exception;

namespace WakefulIntentService
{
    public class AlarmReceiver : BroadcastReceiver
    {
        private static string WAKEFUL_META_DATA = "com.jondouglas.wakeful";

        public override void OnReceive(Context context, Intent intent)
        {
            WakefulIntentService.IAlarmListener alarmListener = GetListener(context);

            if (alarmListener != null)
            {
                if (intent.Action == null)
                {
                    ISharedPreferences preferences = context.GetSharedPreferences(WakefulIntentService.NAME, 0);

                    preferences.Edit().PutLong(WakefulIntentService.LAST_ALARM, System.DateTime.Now.Millisecond)
                        .Commit();

                    alarmListener.SendWakefulWork(context);
                }
                else
                {
                    WakefulIntentService.ScheduleAlarms(alarmListener, context, true);
                }
            }
        }

        private WakefulIntentService.IAlarmListener GetListener(Context context)
        {
            PackageManager packageManager = context.PackageManager;
            ComponentName componentName = new ComponentName(context, Class);

            try
            {
                ActivityInfo activityInfo = packageManager.GetReceiverInfo(componentName, PackageInfoFlags.MetaData);
                XmlReader reader = activityInfo.LoadXmlMetaData(packageManager, WAKEFUL_META_DATA);

                while (!reader.EOF)
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.Name == "WakefulIntentService")
                        {
                            string className = reader.Value;
                            Class cls = Java.Lang.Class.ForName(className);
                            return ((WakefulIntentService.IAlarmListener)cls.NewInstance());
                        }
                    }
                    reader.MoveToNextAttribute();
                }
            }
            catch (Exception ex)
            {
                //Figure this out later.
                throw;
            }
            return (null);
        }
    }
}