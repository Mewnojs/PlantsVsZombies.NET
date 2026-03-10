using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MonoGame.IMEHelper
{
    internal static class Sdl
    {
        public static IntPtr NativeLibrary = GetNativeLibrary();

        private static IntPtr GetNativeLibrary()
        {
            if (CurrentPlatform.OS == OS.Windows)
                return FuncLoader.LoadLibraryExt("SDL2.dll");
            else if (CurrentPlatform.OS == OS.Linux)
                return FuncLoader.LoadLibraryExt("libSDL2-2.0.so.0");
            else if (CurrentPlatform.OS == OS.MacOSX)
                return FuncLoader.LoadLibraryExt("libSDL2-2.0.0.dylib");
            else
                return FuncLoader.LoadLibraryExt("sdl2");
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rectangle
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void d_sdl_starttextinput();
        public static d_sdl_starttextinput StartTextInput = FuncLoader.LoadFunction<d_sdl_starttextinput>(NativeLibrary, "SDL_StartTextInput");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void d_sdl_stoptextinput();
        public static d_sdl_stoptextinput StopTextInput = FuncLoader.LoadFunction<d_sdl_stoptextinput>(NativeLibrary, "SDL_StopTextInput");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void d_sdl_settextinputrect(ref Rectangle rect);
        public static d_sdl_settextinputrect SetTextInputRect = FuncLoader.LoadFunction<d_sdl_settextinputrect>(NativeLibrary, "SDL_SetTextInputRect");
    }
}
