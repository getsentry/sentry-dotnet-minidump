using System;
using System.Runtime.InteropServices;

namespace Sentry.Minidump.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            SentryMinidump.Init(o =>
            {
                o.Dsn = "https://f689b96c05754247afaf0b6ebc83fd5c@o447951.ingest.sentry.io/5572891";
                o.Debug = true;
            });
            
            Marshal.ReadInt32(IntPtr.Zero);
        }
    }
}
