using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui;

/// <summary>
/// Base window class
/// </summary>
public abstract class Window
{
    private readonly Guid id = Guid.NewGuid();

    /// <summary>
    /// Create a new window
    /// </summary>
    /// <param name="name">Name of the window</param>
    public Window(string name)
    {
        CopperImGui.CurrentlyCreatingWindowData = (name, true, ImGuiWindowFlags.None);
    }

    /// <summary>
    /// Create a new window
    /// </summary>
    /// <param name="name">Name of the window</param>
    /// <param name="open">Starting opening state of the window</param>
    public Window(string name, bool open)
    {
        CopperImGui.CurrentlyCreatingWindowData = (name, open, ImGuiWindowFlags.None);
    }

    /// <summary>
    /// Create a new window
    /// </summary>
    /// <param name="name">Name of the window</param>
    /// <param name="open">Starting opening state of the window</param>
    /// <param name="flags">Rendering flags for the window</param>
    public Window(string name, bool open, ImGuiWindowFlags flags)
    {
        CopperImGui.CurrentlyCreatingWindowData = (name, open, flags);
    }

    /// <summary>
    /// Per frame rendering method
    /// </summary>
    public abstract void Render();

    /// <summary>
    /// Called whenever the window is loaded
    /// </summary>
    public virtual void OnLoad()
    {
    }

    /// <summary>
    /// Called whenever the window is removed
    /// </summary>
    public virtual void Shutdown()
    {
    }

    /// <summary>
    /// The <see cref="Guid"/> of the window
    /// </summary>
    /// <returns>The <see cref="Guid"/> object</returns>
    public Guid GetId() => id;

    /// <summary>
    /// Current open state of the window
    /// </summary>
    /// <returns>True if open</returns>
    public bool IsOpen() => CopperImGui.IsWindowOpen(id);
}