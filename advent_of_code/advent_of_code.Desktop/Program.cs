using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace advent_of_code.Desktop
{
    internal sealed partial class Program
    {
        [LibraryImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool AllocConsole();

        public static void Main(string[] args)
        {
            if (args.Contains("--console") || args.Contains("-c"))
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    AllocConsole();
                }
                ConsoleOnly.AdventOfCode.Main();
                return;
            }

            //Linux XOrg check
            string? waylandDisplayVariable = Environment.GetEnvironmentVariable("WAYLAND_DISPLAY");
            string? displayVariable = Environment.GetEnvironmentVariable("DISPLAY");
        
            if (string.IsNullOrEmpty(displayVariable) && string.IsNullOrEmpty(waylandDisplayVariable) && Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                ConsoleOnly.AdventOfCode.Main();
                return;
            }

            AvaloniaMain(args);
        }

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void AvaloniaMain(string[] args)
        {
            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .UseReactiveUI();
    }
}
