
namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    /// <summary>
    /// Setup the entire system
    /// </summary>
    /// <param name="isDockingEnabled">Should docking be enabled</param>
    /// <param name="shouldShowTabBar">Should the top main menu bar be rendered with all the windows in a dropdown</param>
    /// <typeparam name="TImGuiRenderer">Type of your <see cref="CopperDevs.DearImGui.IImGuiRenderer"/></typeparam>
    /// <typeparam name="TImGuiBinding">Type of your <see cref="CopperDevs.DearImGui.IImGuiBinding"/></typeparam>
    public static void Setup<TImGuiRenderer, TImGuiBinding>(bool isDockingEnabled = true, bool shouldShowTabBar = false)
        where TImGuiRenderer : IImGuiRenderer, new()
        where TImGuiBinding : IImGuiBinding, new()
    {
        currentRenderer = new TImGuiRenderer();
        currentBinding = new TImGuiBinding();

        currentRenderer.Setup();

        LoadConfig();
        LoadStyle();

        windows = LoadWindows();

        windows.ForEach(instance => instance.Start());

        canRender = true;
        showTabBar = shouldShowTabBar;
        dockingEnabled = isDockingEnabled;
    }

    /// <summary>
    /// Render all registered items
    /// </summary>
    public static void Render()
    {
        if (!canRender)
            return;

        currentRenderer.Begin();

        PreRendered?.Invoke();

        if (dockingEnabled)
            currentBinding.DockSpaceOverMainViewport();
            // ImGui.DockSpaceOverViewport(0, ImGui.GetMainViewport(), ImGuiDockNodeFlags.PassthruCentralNode | ImGuiDockNodeFlags.AutoHideTabBar);

        RenderWindows();
        RenderBuiltInWindows();
        RenderPopups();

        Rendered?.Invoke();

        currentRenderer.End();
    }

    /// <summary>
    /// Shutdown the entire system
    /// </summary>
    public static void Shutdown()
    {
        if (!canRender)
            return;

        currentRenderer.Shutdown();
        windows.ForEach(instance => instance.Stop());
    }

    private static void RenderBuiltInWindows()
    {
        if (ShowDearImGuiAboutWindow)
            currentBinding.ShowAboutWindow(ref ShowDearImGuiAboutWindow);

        if (ShowDearImGuiDemoWindow)
            currentBinding.ShowDemoWindow(ref ShowDearImGuiDemoWindow);

        if (ShowDearImGuiMetricsWindow)
            currentBinding.ShowMetricsWindow(ref ShowDearImGuiMetricsWindow);

        if (ShowDearImGuiDebugLogWindow)
            currentBinding.ShowDebugLogWindow(ref ShowDearImGuiDebugLogWindow);

        if (ShowDearImGuiIdStackToolWindow)
            currentBinding.ShowIDStackToolWindow(ref ShowDearImGuiIdStackToolWindow);
    }
}