using CopperDevs.DearImGui.Attributes;

namespace CopperDevs.DearImGui;

/// <summary>
/// Core class that handles most the rendering
/// </summary>
public static partial class CopperImGui
{
    // ---------------- render settings ---------------- // 
    private static bool canRender;
    private static bool dockingEnabled;
    private static bool showTabBar;
    private static IImGuiRenderer currentRenderer = null!;
    internal static IImGuiBackend CurrentBackend { get; private set; } = null!;

    // ---------------- windows ---------------- //
    private static List<WindowAttribute> windows = [];

    // ---------------- dearimgui windows ---------------- //
    /// <summary>
    /// Setting for if the built in DearImGui about window is rendered
    /// </summary>
    public static bool ShowDearImGuiAboutWindow;

    /// <summary>
    /// Setting for if the built in DearImGui demo window is rendered
    /// </summary>
    public static bool ShowDearImGuiDemoWindow;

    /// <summary>
    /// Setting for if the built in DearImGui metrics window is rendered
    /// </summary>
    public static bool ShowDearImGuiMetricsWindow;

    /// <summary>
    /// Setting for if the built in DearImGui debug log window is rendered
    /// </summary>
    public static bool ShowDearImGuiDebugLogWindow;

    /// <summary>
    /// Setting for if the built in DearImGui id stack tool window is rendered
    /// </summary>
    public static bool ShowDearImGuiIdStackToolWindow;

    // ---------------- temps ---------------- //
    private static Vector2 tempVec = new();

    // ---------------- actions ---------------- //

    /// <summary>
    /// Called right after the renderer is started for this frame, but before anything else
    /// </summary>
    public static Action? PreRendered;

    /// <summary>
    /// Called right before the renderer is ending for this frame, but after everything else
    /// </summary>
    public static Action? Rendered;
}