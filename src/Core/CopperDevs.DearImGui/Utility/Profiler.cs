using System.Diagnostics;
using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui.Utility;

internal static class Profiler
{
    private static readonly Dictionary<string, ProfilerItem> timestamps = [];

    public static void Begin(string name, int priority = 0)
    {
        if (!CopperImGui.IsDebug)
            return;
        
        var contains = timestamps.TryGetValue(name, out var outProfilerItem);
        var profilerItem = (contains ? outProfilerItem : new ProfilerItem()) ?? new ProfilerItem();

        profilerItem.Id = name;
        profilerItem.StartTime = Stopwatch.GetTimestamp();
        profilerItem.Priority = priority;

        if (!contains)
            timestamps.Add(name, profilerItem);
    }

    public static double End(string name)
    {
        if (!CopperImGui.IsDebug)
            return 0;
        
        var end = Stopwatch.GetTimestamp();
        var start = timestamps[name].StartTime;
        var elapsed = (end - start) / (double)Stopwatch.Frequency;

        timestamps[name].ElapsedTime = Math.Round(elapsed * 1000, 4);

        return elapsed;
    }

    public static List<ProfilerItem> GetTimestamps() => timestamps.Values.OrderBy(x => x.Priority).ToList();

    public sealed class ProfilerItem
    {
        public string Id = string.Empty;
        public int Priority;
        public long StartTime;
        public double ElapsedTime;
    }
}