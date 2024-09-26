using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Utility;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    /// <summary>
    ///     Setup the entire system
    /// </summary>
    /// <param name="isDockingEnabled">Should docking be enabled</param>
    /// <param name="shouldShowTabBar">Should the top main menu bar be rendered with all the windows in a dropdown</param>
    /// <param name="rendererType">Type of your <see cref="ImGuiRenderer" /></param>
    public static void Setup(Type rendererType, bool isDockingEnabled = true, bool shouldShowTabBar = false)
    {
        try
        {
            Log.Info($"Setting up {rendererType.Name} to use for rendering with {typeof(CopperImGui)}");

            currentRenderer = (ImGuiRenderer)Activator.CreateInstance(rendererType)!;

            currentRenderer.Setup();

            // loading config and styles

            LoadConfig();
            LoadStyle();

            Log.Debug("Loading windows");

            windows = LoadWindows();

            windows.ForEach(instance => instance.Start());

            Log.Debug($"Loaded {windows.Count} windows");

            canRender = true;
            showTabBar = shouldShowTabBar;
            dockingEnabled = isDockingEnabled;
            
            Log.Success($"Finished setting up {typeof(CopperImGui)}");
        }
        catch (Exception e)
        {
            Log.Critical($"Setting up the rendering for {typeof(CopperImGui)} failed");
            Log.Exception(e);
        }
    }


    /// <summary>
    ///     Set up the entire system
    /// </summary>
    /// <param name="isDockingEnabled">Should docking be enabled</param>
    /// <param name="shouldShowTabBar">Should the top main menu bar be rendered with all the windows in a dropdown</param>
    /// <typeparam name="TImGuiRenderer">Type of your <see cref="ImGuiRenderer" /></typeparam>
    public static void Setup<TImGuiRenderer>(bool isDockingEnabled = true, bool shouldShowTabBar = false) where TImGuiRenderer : ImGuiRenderer, new()
    {
        Setup(typeof(TImGuiRenderer), isDockingEnabled, shouldShowTabBar);
    }

    /// <summary>
    ///     Render all registered items
    /// </summary>
    public static void Render()
    {
        if (!canRender)
            return;

        try
        {
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
        catch (Exception e)
        {
            Log.Critical($"Rendering for {typeof(CopperImGui)} has had an error");
            Log.Exception(e);
        }
    }

    /// <summary>
    ///     Shutdown the entire system
    /// </summary>
    public static void Shutdown()
    {
        if (!canRender)
            return;

        try
        {
            Log.Info($"Shutting down the rendering for {typeof(CopperImGui)}");
            
            currentRenderer.Shutdown();
            windows.ForEach(instance => instance.Stop());
            UnloadFontAwesomeIcons();
        }
        catch (Exception e)
        {
            Log.Critical($"Shutting down the rendering for {typeof(CopperImGui)} failed");
            Log.Exception(e);
        }
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