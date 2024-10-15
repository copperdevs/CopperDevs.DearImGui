using CopperDevs.Core;
using CopperDevs.DearImGui.Rendering;

namespace CopperDevs.DearImGui.Testing;

[Window("Popup Testing", WindowOpen = false)]
public class PopupTestingWindow : BaseWindow
{
    public override void WindowStart()
    {
        CopperImGui.RegisterPopup("test-popup", TestPopup);
    }

    public override void WindowStop()
    {
        CopperImGui.DeregisterPopup("test-popup");
    }

    public override void WindowUpdate()
    {
        CopperImGui.Button("open popup", () => CopperImGui.ShowPopup("test-popup"));
        // CopperImGui.ForceRenderPopup("test-popup");
    }

    private void TestPopup()
    {
        CopperImGui.Text("Test popup");
    }
}