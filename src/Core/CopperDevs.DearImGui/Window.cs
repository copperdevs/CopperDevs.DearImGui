using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui;

public abstract class Window
{
    private Guid id = Guid.NewGuid();

    public Window(string name, bool open = true)
    {
    }

    public abstract void Render();

    public virtual void OnLoad()
    {
    }

    public virtual void Shutdown()
    {
    }

    public Guid GetId() => id;

    public void SetWindowSettings(ImGuiWindowSettings windowSettings)
    {
        CopperImGui.SetWindowSettings(id, windowSettings);
    }

    public bool IsOpen()
    {
        
    }
}