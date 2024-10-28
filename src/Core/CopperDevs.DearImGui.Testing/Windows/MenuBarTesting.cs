using CopperDevs.DearImGui.Rendering;
using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui.Testing.Windows;

[Window("Menu Bar Testing", Flags = ImGuiWindowFlags.MenuBar)]
public class MenuBarTesting : BaseWindow
{
    public override void WindowUpdate()
    {
        CopperImGui.MenuBar(false, ("not main", () => CopperImGui.MenuItem("yeah")));
        CopperImGui.MenuBar(true, ("main", () => CopperImGui.MenuItem("yeah")));
    }
}