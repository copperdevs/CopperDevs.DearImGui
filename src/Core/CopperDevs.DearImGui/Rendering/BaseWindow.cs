namespace CopperDevs.DearImGui.Rendering;

/// <summary>
/// This isn't required for a window to actually be registered because the windows methods are called through reflection. But you can use this class so the methods are actually there
/// </summary>
public abstract class BaseWindow
{
    /// <summary>
    /// Called at the start of a windows lifetime
    /// </summary>
    public virtual void WindowStart()
    {
    }

    /// <summary>
    /// Called every frame to render all of a windows components
    /// </summary>
    public virtual void WindowUpdate()
    {
    }

    /// <summary>
    /// Called at the end of a windows lifetime
    /// </summary>
    public virtual void WindowStop()
    {
    }
}