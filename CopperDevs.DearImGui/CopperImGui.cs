using CopperDevs.DearImGui.Attributes;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    // render settings
    private static bool canRender;
    private static bool dockingEnabled;
    private static IImGuiRenderer currentRenderer = null!;

    // windows
    private static List<WindowAttribute> windows = [];

    // dearimgui windows
    public static bool ShowDearImGuiAboutWindow;
    public static bool ShowDearImGuiDemoWindow;
    public static bool ShowDearImGuiMetricsWindow;
    public static bool ShowDearImGuiDebugLogWindow;
    public static bool ShowDearImGuiIdStackToolWindow;

    // temps
    private static Vector2 tempVec = new();

    // actions
    public static Action? PreRendered;
    public static Action? Rendered;
}