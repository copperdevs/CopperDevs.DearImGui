using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui;

public abstract class Window
{
    private Guid id = Guid.NewGuid();

    public Window(string name)
    {
        CopperImGui.CurrentlyCreatingWindowData = (name, true, ImGuiWindowFlags.None);
    }

    public Window(string name, bool open)
    {
        CopperImGui.CurrentlyCreatingWindowData = (name, open, ImGuiWindowFlags.None);
    }

    public Window(string name, bool open, ImGuiWindowFlags flags)
    {
        CopperImGui.CurrentlyCreatingWindowData = (name, open, flags);
    }

    public abstract void Render();

    public virtual void OnLoad()
    {
    }

    public virtual void Shutdown()
    {
    }

    public Guid GetId() => id;
    public bool IsOpen() => CopperImGui.IsWindowOpen(id);
}