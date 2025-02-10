using CopperDevs.DearImGui.Rendering;

namespace CopperDevs.DearImGui;

/// <summary>
///     Core class that handles most the rendering
/// </summary>
public static partial class CopperImGui
{
    internal static bool IsDebug
    {
        get
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }

    // ---------------- render settings ---------------- // 
    private static RenderingSettings settings = RenderingSettings.Everything;
    private static bool canRender;
    private static bool DockingEnabled => settings.HasFlag(RenderingSettings.DockingEnabled);
    private static bool ShowWindowsOnMenuBar => settings.HasFlag(RenderingSettings.ShowWindowsOnMenuBar);
    private static bool UseReflectionForWindows => settings.HasFlag(RenderingSettings.ReflectionForWindows);
    private static bool FontAwesomeIconsEnabled => settings.HasFlag(RenderingSettings.FontAwesomeIcons);

    private static ImGuiRenderer currentRenderer = null!;

    // ---------------- dearimgui windows ---------------- //
    /// <summary>
    ///     Setting for if the built in DearImGui about window is rendered
    /// </summary>
    public static bool ShowDearImGuiAboutWindow;

    /// <summary>
    ///     Setting for if the built in DearImGui demo window is rendered
    /// </summary>
    public static bool ShowDearImGuiDemoWindow;

    /// <summary>
    ///     Setting for if the built in DearImGui metrics window is rendered
    /// </summary>
    public static bool ShowDearImGuiMetricsWindow;

    /// <summary>
    ///     Setting for if the built in DearImGui debug log window is rendered
    /// </summary>
    public static bool ShowDearImGuiDebugLogWindow;

    /// <summary>
    ///     Setting for if the built in DearImGui id stack tool window is rendered
    /// </summary>
    public static bool ShowDearImGuiIdStackToolWindow;

    // ---------------- temps ---------------- //
    private static readonly Vector2 tempVec = new();

    // ---------------- actions ---------------- //

    /// <summary>
    ///     Called right after the renderer is started for this frame, but before anything else
    /// </summary>
    public static Action? PreRendered;

    /// <summary>
    ///     Called right before the renderer is ending for this frame, but after everything else
    /// </summary>
    public static Action? Rendered;
}