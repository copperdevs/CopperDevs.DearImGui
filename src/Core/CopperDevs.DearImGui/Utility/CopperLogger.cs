using CopperLog = CopperDevs.Logger.Log;

namespace CopperDevs.DearImGui.Utility;

/// <summary>
///     Internal logger class
/// </summary>
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class CopperLogger
{
    public static bool Silent = false;

    public static void LogDebug(object message)
    {
        if (!Silent)
            CopperLog.Debug(message);
    }

    public static void LogInfo(object message)
    {
        if (!Silent)
            CopperLog.Info(message);
    }

    public static void LogRuntime(object message)
    {
        if (!Silent)
            CopperLog.Runtime(message);
    }

    public static void LogNetwork(object message)
    {
        if (!Silent)
            CopperLog.Network(message);
    }

    public static void LogSuccess(object message)
    {
        if (!Silent)
            CopperLog.Success(message);
    }

    public static void LogWarning(object message)
    {
        if (!Silent)
            CopperLog.Warning(message);
    }

    public static void LogError(object message)
    {
        if (!Silent)
            CopperLog.Error(message);
    }

    public static void LogCritical(object message)
    {
        if (!Silent)
            CopperLog.Critical(message);
    }

    public static void LogAudit(object message)
    {
        if (!Silent)
            CopperLog.Audit(message);
    }

    public static void LogTrace(object message)
    {
        if (!Silent)
            CopperLog.Trace(message);
    }

    public static void LogSecurity(object message)
    {
        if (!Silent)
            CopperLog.Security(message);
    }

    public static void LogUserAction(object message)
    {
        if (!Silent)
            CopperLog.UserAction(message);
    }

    public static void LogPerformance(object message)
    {
        if (!Silent)
            CopperLog.Performance(message);
    }

    public static void LogConfig(object message)
    {
        if (!Silent)
            CopperLog.Config(message);
    }

    public static void LogFatal(object message)
    {
        if (!Silent)
            CopperLog.Fatal(message);
    }

    public static void LogException(Exception exception)
    {
        if (!Silent)
            CopperLog.Exception(exception);
    }
}

internal static class Log
{
    public static void Debug(object message)
    {
        CopperLogger.LogDebug(message);
    }

    public static void Info(object message)
    {
        CopperLogger.LogInfo(message);
    }

    public static void Runtime(object message)
    {
        CopperLogger.LogRuntime(message);
    }

    public static void Network(object message)
    {
        CopperLogger.LogNetwork(message);
    }

    public static void Success(object message)
    {
        CopperLogger.LogSuccess(message);
    }

    public static void Warning(object message)
    {
        CopperLogger.LogWarning(message);
    }

    public static void Error(object message)
    {
        CopperLogger.LogError(message);
    }

    public static void Critical(object message)
    {
        CopperLogger.LogCritical(message);
    }

    public static void Audit(object message)
    {
        CopperLogger.LogAudit(message);
    }

    public static void Trace(object message)
    {
        CopperLogger.LogTrace(message);
    }

    public static void Security(object message)
    {
        CopperLogger.LogSecurity(message);
    }

    public static void UserAction(object message)
    {
        CopperLogger.LogUserAction(message);
    }

    public static void Performance(object message)
    {
        CopperLogger.LogPerformance(message);
    }

    public static void Config(object message)
    {
        CopperLogger.LogConfig(message);
    }

    public static void Fatal(object message)
    {
        CopperLogger.LogFatal(message);
    }

    public static void Exception(Exception exception)
    {
        CopperLogger.LogException(exception);
    }
}