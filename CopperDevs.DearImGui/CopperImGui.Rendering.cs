namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    /// <summary>
    /// Setup the entire system
    /// </summary>
    /// <param name="isDockingEnabled">Should docking be enabled</param>
    /// <param name="shouldShowTabBar">Should the top main menu bar be rendered with all the windows in a dropdown</param>
    /// <param name="rendererType">Type of your <see cref="ImGuiRenderer"/></param>
    public static void Setup(Type rendererType, bool isDockingEnabled = true, bool shouldShowTabBar = false)
    {
        currentRenderer = (ImGuiRenderer)Activator.CreateInstance(rendererType)!;

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
    /// Setup the entire system
    /// </summary>
    /// <param name="isDockingEnabled">Should docking be enabled</param>
    /// <param name="shouldShowTabBar">Should the top main menu bar be rendered with all the windows in a dropdown</param>
    /// <typeparam name="TImGuiRenderer">Type of your <see cref="ImGuiRenderer"/></typeparam>
    public static void Setup<TImGuiRenderer>(bool isDockingEnabled = true, bool shouldShowTabBar = false)
        where TImGuiRenderer : ImGuiRenderer, new()
    {
        Setup(typeof(TImGuiRenderer), isDockingEnabled, shouldShowTabBar);
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
            CurrentBackend.DockSpaceOverMainViewport();

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
        UnloadFontAwesomeIcons();
    }

    private static void RenderBuiltInWindows()
    {
        if (ShowDearImGuiAboutWindow)
            CurrentBackend.ShowAboutWindow(ref ShowDearImGuiAboutWindow);

        if (ShowDearImGuiDemoWindow)
            CurrentBackend.ShowDemoWindow(ref ShowDearImGuiDemoWindow);

        if (ShowDearImGuiMetricsWindow)
            CurrentBackend.ShowMetricsWindow(ref ShowDearImGuiMetricsWindow);

        if (ShowDearImGuiDebugLogWindow)
            CurrentBackend.ShowDebugLogWindow(ref ShowDearImGuiDebugLogWindow);

        if (ShowDearImGuiIdStackToolWindow)
            CurrentBackend.ShowIdStackToolWindow(ref ShowDearImGuiIdStackToolWindow);
    }
}