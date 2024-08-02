using CopperDevs.Core;
using CopperDevs.Core.Data;
using Raylib_CSharp.Logging;

namespace CopperDevs.DearImGui.Testing;

public static class RaylibLogger
{
    public static void Initialize()
    {
        Logger.Init();
        Logger.Message += RayLibLog;
        Logger.SetTraceLogLevel(TraceLogLevel.All);
    }

    private static bool RayLibLog(TraceLogLevel level, string message)
    {
        switch (level)
        {
            case TraceLogLevel.All:
                RaylibLogInfo(message);
                break;
            case TraceLogLevel.Trace:
                RaylibLogTrace(message);
                break;
            case TraceLogLevel.Debug:
                RaylibLogDebug(message);
                break;
            case TraceLogLevel.Info:
                RaylibLogInfo(message);
                break;
            case TraceLogLevel.Warning:
                RaylibLogWarning(message);
                break;
            case TraceLogLevel.Error:
                RaylibLogError(message);
                break;
            case TraceLogLevel.Fatal:
                RaylibLogFatal(message);
                break;
            case TraceLogLevel.None:
                RaylibLogDebug(message);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
        }

        return true;
    }

    private static void RaylibLogInfo(object message)
    {
        var color = ConsoleColors.GetColor(ConsoleColors.Names.Cyan);
        Log.Info($"{ConsoleColors.Black}{ConsoleColors.WhiteBackground} Raylib {ConsoleColors.Reset}{color} {message}");
    }

    private static void RaylibLogTrace(object message)
    {
        var color = ConsoleColors.GetColor(ConsoleColors.Names.LightBlue);
        Log.Trace($"{ConsoleColors.Black}{ConsoleColors.WhiteBackground} Raylib {ConsoleColors.Reset}{color} {message}");
    }

    private static void RaylibLogDebug(object message)
    {
        var color = ConsoleColors.GetColor(ConsoleColors.Names.Gray);
        Log.Debug($"{ConsoleColors.Black}{ConsoleColors.WhiteBackground} Raylib {ConsoleColors.Reset}{color} {message}");
    }

    private static void RaylibLogWarning(object message)
    {
        var color = ConsoleColors.GetColor(ConsoleColors.Names.BrightYellow);
        Log.Warning($"{ConsoleColors.Black}{ConsoleColors.WhiteBackground} Raylib {ConsoleColors.Reset}{color} {message}");
    }

    private static void RaylibLogError(object message)
    {
        var color = ConsoleColors.GetColor(ConsoleColors.Names.Red);
        Log.Error($"{ConsoleColors.Black}{ConsoleColors.WhiteBackground} Raylib {ConsoleColors.Reset}{color} {message}");
    }

    private static void RaylibLogFatal(object message)
    {
        var color = ConsoleColors.GetColor(ConsoleColors.Names.DarkRed);
        Log.Fatal($"{ConsoleColors.Black}{ConsoleColors.WhiteBackground} Raylib {ConsoleColors.Reset}{color} {message}");
    }
}