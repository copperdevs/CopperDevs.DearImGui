using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Utility;
using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    /// <summary>
    ///     Set up the entire system
    /// </summary>
    /// <param name="rendererType">Type of your <see cref="ImGuiRenderer" /></param>
    /// <param name="renderingSettings">Settings for configuring the rendering</param>
    public static void Setup(Type rendererType, RenderingSettings renderingSettings = RenderingSettings.Everything)
    {
        try
        {
            Log.Info($"Setting up {rendererType.Name} to use for rendering with {typeof(CopperImGui)}");

            currentRenderer = (ImGuiRenderer)Activator.CreateInstance(rendererType)!;
            settings = renderingSettings;

            currentRenderer.Setup();

            LoadConfig();
            LoadStyle();

            if (UseReflectionForWindows)
            {
                Log.Debug("Loading windows");

                LoadAllWindowsWithReflection();

                Log.Debug($"Loaded {Windows.Count} windows");
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
    /// <param name="renderingSettings">Settings for configuring the rendering</param>
    /// <typeparam name="TImGuiRenderer">Type of your <see cref="ImGuiRenderer" /></typeparam>
    public static void Setup<TImGuiRenderer>(RenderingSettings renderingSettings = RenderingSettings.Everything) where TImGuiRenderer : ImGuiRenderer, new()
    {
        Setup(typeof(TImGuiRenderer), renderingSettings);
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

            if (DockingEnabled)
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
            ShutdownAllWindows();
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