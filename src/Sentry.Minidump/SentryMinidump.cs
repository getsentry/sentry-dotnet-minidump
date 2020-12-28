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
                o.Log("Sentry Native will be disabled: Dsn not provided.");
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
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    crashpadHandler = "crashpad_handler_osx";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    crashpadHandler = "crashpad_handler_linux";
                } 
            }

            if (!File.Exists(crashpadHandler))
            {
                o.Log("Can't find crashpad handler {0}", crashpadHandler);
                
                var extraPath = Path.Combine(Path.GetDirectoryName(typeof(SentryMinidump).Assembly.Location) ?? ".", crashpadHandler);
                if (!File.Exists(extraPath))
                {
                    o.Log("Couldn't find crashpad handler at {0}. Will not initialize sentry-native.", extraPath);
                    return;
                }

                o.Log("Found at {0}", extraPath);
                crashpadHandler = extraPath;
            }
            
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && o.AddExecuteFlagCrashpadHandler)
            {
                o.Log("Setting the execute flag to: {0}", crashpadHandler);
                Permission.SetExecute(Path.GetFullPath(crashpadHandler));
            }

            o.Log("Initialize sentry-native with: {0}.", crashpadHandler);
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
    
    internal static class SentryOptionsExtension
    {
        public static void Log(this SentryOptions o, string message, params object[] args)
        {
            if (o.Debug)
            {
                Console.WriteLine(message, args);
            }
        }
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
