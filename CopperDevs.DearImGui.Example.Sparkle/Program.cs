using CopperDevs.Core.Utility;
using CopperDevs.Logger;
using Raylib_CSharp.Windowing;
using Sparkle.CSharp;
using Sparkle.CSharp.Logging;

namespace CopperDevs.DearImGui.Example.Sparkle;

public static class Program
{

    private static void Main()
    {
        var settings = new GameSettings()
        {
            Title = "CopperDevs.DearImGui Example",
            WindowFlags = ConfigFlags.ResizableWindow | ConfigFlags.Msaa4XHint | ConfigFlags.VSyncHint | ConfigFlags.AlwaysRunWindow,
            Width = 650,
            Height = 450,
        };

        using var game = new DearImGuiExampleGame(settings);
        game.Run(null);
    }

    internal static bool CustomLog(LogType type, string msg, int skipFrames, ConsoleColor color) // using my custom log class instead of the built-in one (personal preference)
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
}