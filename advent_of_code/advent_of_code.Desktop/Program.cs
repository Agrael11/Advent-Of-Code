using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace advent_of_code.Desktop
{
    internal sealed partial class Program
    {
        // Import GetConsoleWindow from kernel32.dll
        [LibraryImport("kernel32.dll", EntryPoint = "GetConsoleWindow")]
        private static partial nint GetConsoleWindow();

        [LibraryImport("kernel32.dll", EntryPoint = "FreeConsole")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool FreeConsole();

        public static void Main(string[] args)
        {
            if (args.Contains("--console") || args.Contains("-c") || args.Contains("/c"))
            {
                ConsoleOnly.AdventOfCode.Register();
                Visualizers.AOConsole.Clear();
                ConsoleOnly.AdventOfCode.Main();
            }
            else
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    var handle = GetConsoleWindow();
                    if (handle != 0)
                    {
                        FreeConsole();
                    }
                }

                //Linux XOrg check
                var waylandDisplayVariable = Environment.GetEnvironmentVariable("WAYLAND_DISPLAY");
                var displayVariable = Environment.GetEnvironmentVariable("DISPLAY");

                if (string.IsNullOrEmpty(displayVariable) && string.IsNullOrEmpty(waylandDisplayVariable) && Environment.OSVersion.Platform != PlatformID.Win32NT)
                {
                    ConsoleOnly.AdventOfCode.Main();
                    return;
                }

                Visualizers.AOConsole.Clear();
                AvaloniaMain(args);
            }
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
        {
            return AppBuilder.Configure<App>()
                        .UsePlatformDetect()
                        .WithInterFont()
                        .LogToTrace()
                        .UseReactiveUI();
        }
    }
}
