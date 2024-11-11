using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Utility;
using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    /// <summary>
    /// ID of the main dockspace
    /// </summary>
    public static uint DockSpaceId { get; private set; }

    /// <summary>
    ///     Set up the entire system
    /// </summary>
    /// <param name="rendererType">Type of your <see cref="ImGuiRenderer" /></param>
    /// <param name="isDockingEnabled">Should docking be enabled</param>
    /// <param name="shouldShowTabBar">Should the top main menu bar be rendered with all the windows in a dropdown</param>
    /// <param name="useReflectionForWindows">Should reflection be used to find any windows</param>
    /// <param name="loadFontAwesomeIcons">Should Font Awesome icons font be loaded automatically</param>
    public static void Setup(Type rendererType, bool isDockingEnabled = true, bool shouldShowTabBar = false, bool useReflectionForWindows = true, bool loadFontAwesomeIcons = false)
    {
        try
        {
            Log.Info($"Setting up {rendererType.Name} to use for rendering with {typeof(CopperImGui)}");

            currentRenderer = (ImGuiRenderer)Activator.CreateInstance(rendererType)!;
            showTabBar = shouldShowTabBar;
            dockingEnabled = isDockingEnabled;
            reflectionWindows = useReflectionForWindows;
            fontAwesomeIcons = loadFontAwesomeIcons;

            currentRenderer.Setup();

            LoadConfig();
            LoadStyle();

            if (reflectionWindows)
            {
                Log.Debug("Loading windows");

                LoadWindows().ForEach(attribute => windows.Add(new WindowData(attribute)));

                windows.ForEach(instance => instance.StartWindow());

                Log.Debug($"Loaded {windows.Count} windows");
            }

            canRender = true;

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
    /// <param name="useReflectionForWindows">Should reflection be used to find any windows</param>
    /// <param name="loadFontAwesomeIcons">Should Font Awesome icons font be loaded automatically</param>
    /// <typeparam name="TImGuiRenderer">Type of your <see cref="ImGuiRenderer" /></typeparam>
    public static void Setup<TImGuiRenderer>(bool isDockingEnabled = true, bool shouldShowTabBar = false, bool useReflectionForWindows = true, bool loadFontAwesomeIcons = true) where TImGuiRenderer : ImGuiRenderer, new()
    {
        Setup(typeof(TImGuiRenderer), isDockingEnabled, shouldShowTabBar, useReflectionForWindows, useReflectionForWindows);
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
                ImGui.DockSpaceOverViewport(null, ImGuiDockNodeFlags.PassthruCentralNode | ImGuiDockNodeFlags.AutoHideTabBar, null);

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
            windows.ForEach(instance => instance.StopWindow());
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
            ImGui.ShowAboutWindow(ref ShowDearImGuiAboutWindow);

        if (ShowDearImGuiDemoWindow)
            ImGui.ShowDemoWindow(ref ShowDearImGuiDemoWindow);

        if (ShowDearImGuiMetricsWindow)
            ImGui.ShowMetricsWindow(ref ShowDearImGuiMetricsWindow);

        if (ShowDearImGuiDebugLogWindow)
            ImGui.ShowDebugLogWindow(ref ShowDearImGuiDebugLogWindow);

        if (ShowDearImGuiIdStackToolWindow)
            ImGui.ShowIDStackToolWindow(ref ShowDearImGuiIdStackToolWindow);
    }
}