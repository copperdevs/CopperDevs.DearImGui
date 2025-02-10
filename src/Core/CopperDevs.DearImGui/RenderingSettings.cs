namespace CopperDevs.DearImGui;

/// <summary>
/// Settings for configuring the rendering
/// </summary>
[Flags]
public enum RenderingSettings
{
    /// <summary>
    /// Base setting
    /// </summary>
    None = 0,

    /// <summary>
    /// Should docking be enabled for the main viewport
    /// </summary>
    DockingEnabled = 1 << 0,

    /// <summary>
    /// Should the main menu bar render all registered windows in a 'Windows' dropdown 
    /// </summary>
    ShowWindowsOnMenuBar = 1 << 1,

    /// <summary>
    /// Should reflection be used to find any windows
    /// </summary>
    ReflectionForWindows = 1 << 2,

    /// <summary>
    /// Should Font Awesome icons font be loaded automatically
    /// </summary>
    FontAwesomeIcons = 1 << 3,

    /// <summary>
    /// Load the custom default styling
    /// </summary>
    UseCustomStyling = 1 << 4,

    /// <summary>
    /// Everything enabled
    /// </summary>
    Everything = DockingEnabled | ShowWindowsOnMenuBar | ReflectionForWindows | FontAwesomeIcons | UseCustomStyling,
}