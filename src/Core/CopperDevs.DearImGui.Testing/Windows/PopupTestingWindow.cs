namespace CopperDevs.DearImGui.Testing.Windows;

public class PopupTestingWindow() : Window("Popup Testing", false)
{
    public override void OnLoad()
    {
        CopperImGui.RegisterPopup("test-popup", TestPopup);
    }

    public override void Render()
    {
        CopperImGui.Button("open popup", () => CopperImGui.ShowPopup("test-popup"));
        // CopperImGui.ForceRenderPopup("test-popup");
    }

    public override void Shutdown()
    {
        CopperImGui.DeregisterPopup("test-popup");
    }
    
    private void TestPopup()
    {
        CopperImGui.Text("Test popup");
    }
}