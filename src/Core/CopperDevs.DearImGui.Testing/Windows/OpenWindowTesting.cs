using CopperDevs.DearImGui.Rendering;

namespace CopperDevs.DearImGui.Testing.Windows;

[Window("Open Window Testing", WindowOpen = false)]
public class OpenWindowTesting : BaseWindow
{
    public override void WindowUpdate()
    {
        CopperImGui.Button("open random testing window", CopperImGui.ShowWindow<RandomTestingWindow>);
    }
}