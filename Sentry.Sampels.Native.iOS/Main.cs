using Sentry.Native.iOS;
using UIKit;

namespace Sentry.Sampels.Native.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            SentrySDK.StartWithConfigureOptions(o =>
            {
                o.Debug = true;
                o.Dsn = "https://80aed643f81249d4bed3e30687b310ab@o447951.ingest.sentry.io/5428537";
            });

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}