using CopperDevs.DearImGui.Attributes;
using CopperDevs.DearImGui.Backend.Enums;
using CopperDevs.DearImGui.Rendering;

namespace CopperDevs.DearImGui.Testing;

[Window("Menu Bar Testing", Flags = WindowFlags.MenuBar)]
public class MenuBarTesting : BaseWindow
{
    public override void WindowUpdate()
    {
        CopperImGui.MenuBar(false, ("not main", () => CopperImGui.MenuItem("yeah")));
        CopperImGui.MenuBar(true, ("main", () => CopperImGui.MenuItem("yeah")));
    }
}