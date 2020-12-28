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
                o.Dsn = "http://80aed643f81249d4bed3e30687b310ab@sentry.garcia.in/5428537";
                o.Debug = true;
            });
            
            Marshal.ReadInt32(IntPtr.Zero);
        }
    }
}
