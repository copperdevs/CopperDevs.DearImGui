namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    /// <summary>
    /// Setup the entire system
    /// </summary>
    /// <param name="isDockingEnabled">Should docking be enabled</param>
    /// <param name="shouldShowTabBar">Should the top main menu bar be rendered with all the windows in a dropdown</param>
    /// <param name="rendererType">Type of your <see cref="CopperDevs.DearImGui.IImGuiRenderer"/></param>
    /// <param name="backendType">Type of your <see cref="IImGuiBackend"/></param>
    public static void Setup(Type rendererType, Type backendType, bool isDockingEnabled = true, bool shouldShowTabBar = false)
    {
        currentRenderer = (IImGuiRenderer)Activator.CreateInstance(rendererType)!;
        CurrentBackend = (IImGuiBackend)Activator.CreateInstance(backendType)!;

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
    /// <param name="backendType">Type of your <see cref="IImGuiBackend"/></param>
    /// <typeparam name="TImGuiRenderer">Type of your <see cref="CopperDevs.DearImGui.IImGuiRenderer"/></typeparam>
    public static void Setup<TImGuiRenderer>(Type backendType, bool isDockingEnabled = true, bool shouldShowTabBar = false)
        where TImGuiRenderer : IImGuiRenderer, new()
    {
        Setup(typeof(TImGuiRenderer), backendType, isDockingEnabled, shouldShowTabBar);
    }

    /// <summary>
    /// Setup the entire system
    /// </summary>
    /// <param name="isDockingEnabled">Should docking be enabled</param>
    /// <param name="shouldShowTabBar">Should the top main menu bar be rendered with all the windows in a dropdown</param>
    /// <typeparam name="TImGuiRenderer">Type of your <see cref="CopperDevs.DearImGui.IImGuiRenderer"/></typeparam>
    /// <typeparam name="TImGuiBackend">Type of your <see cref="IImGuiBackend"/></typeparam>
    public static void Setup<TImGuiRenderer, TImGuiBackend>(bool isDockingEnabled = true, bool shouldShowTabBar = false)
        where TImGuiRenderer : IImGuiRenderer, new()
        where TImGuiBackend : IImGuiBackend, new()
    {
        Setup(typeof(TImGuiRenderer), typeof(TImGuiBackend), isDockingEnabled, shouldShowTabBar);
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