using CopperDevs.Core.Utility;
using CopperDevs.Logger;
using Raylib_CSharp.Windowing;
using Sparkle.CSharp.Logging;

namespace CopperDevs.DearImGui.Example.Sparkle;

public static class Utility
{
     
    // using my custom log class instead of the built-in one (personal preference)
    internal static bool CustomLog(LogType type, string msg, int skipFrames, ConsoleColor color) 
    {
        switch (type)
        {
            case LogType.Debug:
                Log.Debug(msg);
                return true;
            case LogType.Info:
                Log.Info(msg);
                return true;
            case LogType.Warn:
                Log.Warning(msg);
                return true;
            case LogType.Error:
                Log.Error(msg);
                return true;
            case LogType.Fatal:
                Log.Fatal(msg);
                return true;
            default:
                return false;
        }
    }
    
    // windows 11 window styling stuff. its not required at all but it looks really nice
    internal static void SetWindowStyling() 
    {
        if (!WindowsApi.IsWindows11)
            return;
        
        var handle = Window.GetHandle();

        WindowsApi.SetDwmImmersiveDarkMode(handle, true);
        WindowsApi.SetDwmSystemBackdropType(handle, WindowsApi.SystemBackdropType.Acrylic);
        WindowsApi.SetDwmWindowCornerPreference(handle, WindowsApi.WindowCornerPreference.Default);
    }
}