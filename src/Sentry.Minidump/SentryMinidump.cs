using System;
using System.IO;
using System.Runtime.InteropServices;
// TODO: Generate internal
using static sentry.sentry;

namespace Sentry.Minidump
{
    public class SentryMinidump
    {
        public static void Init(Action<SentryOptions> configure)
        {
            var o = new SentryOptions();
            configure?.Invoke(o);
            if (o.Dsn is null)
            {
                if (o.Debug)
                {
                    Console.WriteLine("Sentry Native will be disabled: Dsn not provided.");
                }
                return;
            }
            
            var options = SentryOptionsNew();
            SentryOptionsSetDsn(options, o.Dsn);
            if (o.Debug)
            {
                SentryOptionsSetDebug(options, 1);
            }

            // TODO: Do I need to append .exe on Windows or sentry-native does it?

            // When compiled for a specific platform, the package runtimes for the
            // right platform folder gets copied out into it.
            SentryOptionsSetHandlerPath(options, "crashpad_handler");
            SentryInit(options);
        }
    }
    
    public class SentryOptions
    {
        public string Dsn { get; set; }
        public bool Debug { get; set; }
    }
}
