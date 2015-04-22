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
    [Service]
    public class PlayerService : Service
    {
        public static string EXTRA_PLAYLIST = "EXTRA_PLAYLIST";
        public static string EXTRA_SHUFFLE = "EXTRA_SHUFFLE";
        private bool isPlaying = false;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            string playlist = intent.GetStringExtra(EXTRA_PLAYLIST);
            bool useShuffle = intent.GetBooleanExtra(EXTRA_SHUFFLE, false);

            Play(playlist, useShuffle);

            return StartCommandResult.NotSticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            return (null);
        }

        public override void OnDestroy()
        {
            Stop();
            base.OnDestroy();
        }

        private void Play(string playlist, bool useShuffle)
        {
            if (!isPlaying)
            {
                Log.WriteLine(LogPriority.Info, Class.Name, "Got to Play()");
                isPlaying = true;
            }
        }

        private void Stop()
        {
            if (isPlaying)
            {
                Log.WriteLine(LogPriority.Info, Class.Name, "Got to Stop()");
                isPlaying = false;
            }
        }
    }
}