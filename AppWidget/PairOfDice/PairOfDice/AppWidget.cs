using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Math = Java.Lang.Math;

namespace PairOfDice
{
    [BroadcastReceiver(Label = "Pair Of Dice", Name = "pairofdice.AppWidget")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/widget_provider")]
    public class AppWidget : AppWidgetProvider
    {
        private static int[] IMAGES = { Resource.Drawable.die_1, Resource.Drawable.die_2, Resource.Drawable.die_3, Resource.Drawable.die_4, Resource.Drawable.die_5, Resource.Drawable.die_6};

        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            ComponentName me = new ComponentName(context, typeof(AppWidget).Name);

            appWidgetManager.UpdateAppWidget(me, BuildUpdate(context, appWidgetIds));
        }

        private RemoteViews BuildUpdate(Context context, int[] appWidgetIds)
        {
            RemoteViews updateViews = new RemoteViews(context.PackageName, Resource.Layout.widget);

            Intent i = new Intent(context, typeof(AppWidget));
            i.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            i.PutExtra(AppWidgetManager.ExtraAppwidgetIds, appWidgetIds);

            PendingIntent pi = PendingIntent.GetBroadcast(context, 0, i, PendingIntentFlags.UpdateCurrent);

            updateViews.SetImageViewResource(Resource.Id.left_die, IMAGES[(int)(Math.Random()*6)]);
            updateViews.SetOnClickPendingIntent(Resource.Id.left_die, pi);

            updateViews.SetImageViewResource(Resource.Id.right_die, IMAGES[(int)(Math.Random() * 6)]);
            updateViews.SetOnClickPendingIntent(Resource.Id.right_die, pi);

            updateViews.SetOnClickPendingIntent(Resource.Id.background, pi);

            return (updateViews);
        }
    }
}