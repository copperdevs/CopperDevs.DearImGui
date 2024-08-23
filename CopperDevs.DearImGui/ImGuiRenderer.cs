namespace CopperDevs.DearImGui;

/// <summary>
/// Base interface for implementing custom imgui renderer
/// </summary>
public abstract class ImGuiRenderer
{
    /// <summary>
    /// Callback for users to load fonts
    /// </summary>
    public static Action? LoadUserFonts;

    /// <summary>
    /// Called at the start of the renderers lifetime
    /// </summary>
    public abstract void Setup();

    /// <summary>
    /// Called before rendering all registered windows
    /// </summary>
    public abstract void Begin();

    /// <summary>
    /// Called after rendering all registered windows
    /// </summary>
    public abstract void End();

    /// <summary>
    /// Called at the end of the renderers lifetime
    /// </summary>
    public abstract void Shutdown();
}