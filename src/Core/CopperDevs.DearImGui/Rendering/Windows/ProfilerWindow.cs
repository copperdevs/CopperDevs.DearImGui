using CopperDevs.DearImGui.Utility;
using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui.Rendering.Windows;

[Window("Profiler")]
[DebugOnly]
internal class ProfilerWindow : BaseWindow
{
    public override void WindowUpdate()
    {
        foreach (var timestamp in Profiler.GetTimestamps())
        {
            CopperImGui.Text(timestamp.ElapsedTime, timestamp.Id);
        }        
    }
}