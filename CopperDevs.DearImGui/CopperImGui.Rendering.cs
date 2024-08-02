using ImGuiNET;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    public static void Setup<T>(bool isDockingEnabled = true) where T : IImGuiRenderer, new()
    {
        currentRenderer = new T();

        currentRenderer.Setup();

        LoadConfig();
        LoadStyle();

        windows = LoadWindows();

        windows.ForEach(instance => instance.Start());

        canRender = true;
        dockingEnabled = isDockingEnabled;
    }

    public static void Render()
    {
        if (!canRender)
            return;

        currentRenderer.Begin();

        PreRendered?.Invoke();

        if (dockingEnabled)
            ImGui.DockSpaceOverViewport(0, ImGui.GetMainViewport(), ImGuiDockNodeFlags.PassthruCentralNode | ImGuiDockNodeFlags.AutoHideTabBar);

        RenderWindows();
        RenderBuiltInWindows();
        RenderPopups();

        Rendered?.Invoke();

        currentRenderer.End();
    }

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