#WakefulIntentService

I've seen this problem quite a lot recently...How do I receive notifications when my device is not awake? This implementation is done by CommonsWare(http://commonsware.com/) and ported over to Xamarin.Android by myself:

So you may ask: "What does `WakefulIntentService` do?"

`WakefulIntentService` keeps a device awake by using the mechanism of `WakeLock`. A `WakeLock` is better known as a way to keep the device awake indefinitely until the `WakeLock` is released somehow.

The good part about this code, is that it has been tested much further than a normal implementation of a `WakeLock` mechanism.

https://github.com/commonsguy/cwac-wakeful (Original Implementation in Java)

https://github.com/JonDouglas/xamarin-android-tutorials/tree/master/Wakeful

## Other Alternatives

You could additionally use the Android Support package's `WakefulBroadcastReceiver`, which is a way to trigger work to be done by a broadcast. This however depends completely on the situation.

https://developer.android.com/reference/android/support/v4/content/WakefulBroadcastReceiver.html

### Pros

- Greater flexibility as it's not strictly tied to an `IntentService`.
- Uses one `WakeLock` per request and therefore is more resilient for potential problems with a static `WakeLock`.

### Cons

- Can be prone to a leak as the developer must call `CompleteWakefulIntent()`.
- It is a time-limited `WakeLock` and therefore will release after one minute. Which limits the flexibility with no overrides.
- If the process is terminated by anything which causes the service to restart, the restarted service will not be under a `WakeLock`.