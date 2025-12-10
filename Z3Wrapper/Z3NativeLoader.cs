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

            // Pick your embedded resource here
            // e.g. "Z3Wrapper.Assets.linux-arm64.libz3.so"
            var resName = typeof(Z3NativeLoader).Assembly
                .GetManifestResourceNames()
                .First(n => n.EndsWith("libz3.so"));

            extractedPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "libz3.so");

            using (var s = typeof(Z3NativeLoader).Assembly.GetManifestResourceStream(resName))
            using (var f = File.Create(extractedPath))
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