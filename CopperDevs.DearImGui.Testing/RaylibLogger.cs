using CopperDevs.Logger;
using Raylib_CSharp.Logging;
using rlLogger = Raylib_CSharp.Logging.Logger;

namespace CopperDevs.DearImGui.Testing;

public static class RaylibLogger
{
    public static void Initialize()
    {
        rlLogger.Init();
        rlLogger.Message += RayLibLog;
        rlLogger.SetTraceLogLevel(TraceLogLevel.All);
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
        var color = AnsiColors.GetColor(AnsiColors.Names.Cyan);
        Log.Info($"{AnsiColors.Black}{AnsiColors.WhiteBackground} Raylib {AnsiColors.Reset}{color} {message}");
    }

    private static void RaylibLogTrace(object message)
    {
        var color = AnsiColors.GetColor(AnsiColors.Names.LightBlue);
        Log.Trace($"{AnsiColors.Black}{AnsiColors.WhiteBackground} Raylib {AnsiColors.Reset}{color} {message}");
    }

    private static void RaylibLogDebug(object message)
    {
        var color = AnsiColors.GetColor(AnsiColors.Names.Gray);
        Log.Debug($"{AnsiColors.Black}{AnsiColors.WhiteBackground} Raylib {AnsiColors.Reset}{color} {message}");
    }

    private static void RaylibLogWarning(object message)
    {
        var color = AnsiColors.GetColor(AnsiColors.Names.BrightYellow);
        Log.Warning($"{AnsiColors.Black}{AnsiColors.WhiteBackground} Raylib {AnsiColors.Reset}{color} {message}");
    }

    private static void RaylibLogError(object message)
    {
        var color = AnsiColors.GetColor(AnsiColors.Names.Red);
        Log.Error($"{AnsiColors.Black}{AnsiColors.WhiteBackground} Raylib {AnsiColors.Reset}{color} {message}");
    }

    private static void RaylibLogFatal(object message)
    {
        var color = AnsiColors.GetColor(AnsiColors.Names.DarkRed);
        Log.Fatal($"{AnsiColors.Black}{AnsiColors.WhiteBackground} Raylib {AnsiColors.Reset}{color} {message}");
    }
}