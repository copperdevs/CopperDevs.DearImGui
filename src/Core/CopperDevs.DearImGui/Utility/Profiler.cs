using System.Diagnostics;
using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui.Utility;

internal static class Profiler
{
    private const int ProfilerItemHistory = 512;
    private static readonly Dictionary<string, ProfilerItem> timestamps = [];
    private static readonly Dictionary<string, ProfilerItem.Timestamp> currentTimestamps = [];

    public static void Begin(string name, int priority = 0)
    {
        if (!CopperImGui.IsDebug)
            return;

        if (!timestamps.ContainsKey(name))
            timestamps.Add(name, new ProfilerItem
            {
                Id = name,
                Priority = priority
            });

        var timestamp = new ProfilerItem.Timestamp
        {
            StartTime = Stopwatch.GetTimestamp()
        };

        currentTimestamps[name] = timestamp;
    }

    public static double End(string name)
    {
        if (!CopperImGui.IsDebug)
            return 0;

        currentTimestamps[name].ElapsedTime = GetCurrentTimestamp(name);

        timestamps[name].Timestamps.Add(currentTimestamps[name]);

        if (timestamps[name].Timestamps.Count >= ProfilerItemHistory)
            timestamps[name].Timestamps.RemoveAt(0);

        return currentTimestamps[name].ElapsedTime;
    }

    public static double GetCurrentTimestamp(string name) => Math.Round(((Stopwatch.GetTimestamp() - currentTimestamps[name].StartTime) / (double)Stopwatch.Frequency) * 1000, 4);

    public static List<ProfilerItem> GetTimestamps() => timestamps.Values.OrderBy(x => x.Priority).ToList();

    public sealed class ProfilerItem
    {
        public string Id = string.Empty;
        public int Priority;

        public List<Timestamp> Timestamps = [];

        public sealed class Timestamp
        {
            public long StartTime;
            public double ElapsedTime;

            public static implicit operator double(Timestamp timestamp) => timestamp.ElapsedTime;
            public static implicit operator float(Timestamp timestamp) => (float)timestamp.ElapsedTime;
        }
    }
}