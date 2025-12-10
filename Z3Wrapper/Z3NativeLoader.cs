using System.Reflection;
using System.Runtime.InteropServices;

namespace Z3Wrapper
{
    public static class Z3NativeLoader
    {
        private static bool loaded;
        private static string? extractedPath;

        public static void Load()
        {
            if (loaded) return;
            string? name = null;
            string? arch = null;
            if (OperatingSystem.IsAndroid())
            {
                name = "libz3.so";
                if (RuntimeInformation.OSArchitecture == Architecture.Arm64)
                {
                    arch = "arm64_v8a";
                }
            }
            else if (OperatingSystem.IsWindows())
            {
                name = "libz3.dll";
                if (RuntimeInformation.OSArchitecture == Architecture.Arm64)
                {
                    arch = "win_arm64";
                }
                else if (RuntimeInformation.OSArchitecture == Architecture.X86)
                {
                    arch = "win_x86";
                }
            }
            else if (OperatingSystem.IsLinux())
            {
                name = "libz3.so";
                if (RuntimeInformation.OSArchitecture == Architecture.X64)
                {
                    arch = "linux_x64";
                }
                else if (RuntimeInformation.OSArchitecture == Architecture.Arm64)
                {
                    arch = "linux_arm64";
                }
                else if (RuntimeInformation.OSArchitecture == Architecture.X86)
                {
                    arch = "linux_x86";
                }
            }

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrEmpty(arch))
            {
                Load(name, arch);
            }
        }

        private static void Load(string name, string arch)
        {
            // Pick your embedded resource here
            // e.g. "Z3Wrapper.Assets.linux-arm64.libz3.so"
            var resName = typeof(Z3NativeLoader).Assembly
                .GetManifestResourceNames()
                .First(n => n.Contains(arch, StringComparison.OrdinalIgnoreCase) &&
                n.EndsWith(name, StringComparison.OrdinalIgnoreCase));

            extractedPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                name);

            using var s = typeof(Z3NativeLoader).Assembly.GetManifestResourceStream(resName) ?? throw new Exception("Resource load failed.");
            using var f = File.Create(extractedPath);
            s.CopyTo(f);

            NativeLibrary.SetDllImportResolver(
                typeof(Microsoft.Z3.Context).Assembly,
                Resolve);

            loaded = true;
        }

        private static IntPtr Resolve(string libraryName, Assembly asm, DllImportSearchPath? path)
        {
            if (libraryName == "libz3" || libraryName == "z3")
                return NativeLibrary.Load(extractedPath!);

            return IntPtr.Zero;
        }
    }
}