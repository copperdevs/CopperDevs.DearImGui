using CopperDevs.DearImGui.Rendering;

namespace CopperDevs.DearImGui.Testing.Windows;

[Disabled]
public class DisabledTestingWindow() : Window("Disabled Testing", false)
{
    public override void Render()
    {
    }
}