using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    private static Dictionary<Guid, Window> Windows = [];
    private static Dictionary<Guid, ImGuiWindowSettings> WindowSettings = [];
}