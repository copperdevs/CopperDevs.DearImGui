namespace CopperDevs.DearImGui.Testing.Windows;

// [Window("Menu Bar Testing", Flags = ImGuiWindowFlags.MenuBar)]
public class MenuBarTesting() : Window("Menu Bar Testing", false)
{
    public override void Render()
    {
        CopperImGui.MenuBar(false, ("not main", () => CopperImGui.MenuItem("yeah")));
        CopperImGui.MenuBar(true, ("main", () => CopperImGui.MenuItem("yeah")));
    }
}