using System.Text;
using CopperDevs.DearImGui.Utility;
using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui.Rendering.Windows;

[Window("Profiler", WindowOpen = true)]
[DebugOnly]
internal class ProfilerWindow : BaseWindow
{
    public override void WindowUpdate()
    {
        ImGui.Text("This currently isn't fully accurate");
        
        foreach (var timestamp in Profiler.GetTimestamps())
        {
            unsafe
            {
                fixed (byte* textLabel = Encoding.ASCII.GetBytes(timestamp.Id))
                {
                    fixed (float* values = timestamp.Timestamps.Select(castingTimestamp => (float)castingTimestamp).ToArray())
                    {
                        ImGui.PlotHistogram(textLabel, values, timestamp.Timestamps.Count, 0);
                    }
                }
            }
        }        
    }
}