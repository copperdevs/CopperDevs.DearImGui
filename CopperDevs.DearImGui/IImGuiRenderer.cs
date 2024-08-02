namespace CopperDevs.DearImGui;

/// <summary>
/// Base interface for implementing custom imgui renderer
/// </summary>
public interface IImGuiRenderer
{
    /// <summary>
    /// Called at the start of the renderers lifetime
    /// </summary>
    public void Setup();

    /// <summary>
    /// Called before rendering all registered windows
    /// </summary>
    public void Begin();

    /// <summary>
    /// Called after rendering all registered windows
    /// </summary>
    public void End();

    /// <summary>
    /// Called at the end of the renderers lifetime
    /// </summary>
    public void Shutdown();
}