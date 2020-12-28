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

            var crashpadHandler = "crashpad_handler";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                crashpadHandler = "crashpad_handler.exe";
            }
            else
            {
                if (o.AddExecuteFlagCrashpadHandler)
                {
                    Permission.SetExecute(Path.GetFullPath("crashpad_handler"));
                }
            }
            SentryOptionsSetHandlerPath(options, crashpadHandler);
            SentryInit(options);
        }
    }
    
    public class SentryOptions
    {
        public string Dsn { get; set; }
        public bool Debug { get; set; }

        /// <summary>
        /// On Unix system, attempts to add execute permission to crashpad_handler.
        /// </summary>
        public bool AddExecuteFlagCrashpadHandler { get; set; } = true;
    }

    internal static class Permission
    {
        [DllImport("libc", SetLastError = false, EntryPoint = "chmod")]
        private static extern int Chmod(string pathname, int mode);

        public static void SetExecute(string path) => Chmod(path, _0744);
        const int _0744 =
            S_IRUSR | S_IXUSR | S_IWUSR
            | S_IRGRP
            | S_IROTH;
        
        const int S_IRUSR = 0x100;
        const int S_IWUSR = 0x80;
        const int S_IXUSR = 0x40;
        const int S_IRGRP = 0x20;
        const int S_IROTH = 0x4;
    }
}
