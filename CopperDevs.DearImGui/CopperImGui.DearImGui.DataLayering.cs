using ImGuiNET;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    public static bool AnyWindowHovered => ImGui.IsWindowHovered(ImGuiHoveredFlags.AnyWindow);
    public static bool AnyElementHovered => ImGui.GetIO().WantCaptureMouse;

    public static float CurrentWindowWidth => ImGui.GetWindowWidth();
    public static float CurrentWindowHeight => ImGui.GetWindowHeight();
    public static Vector2 CurrentWindowPosition => ImGui.GetWindowPos();
    public static Vector2 CurrentWindowSize => ImGui.GetWindowSize();
    public static ImGuiViewportPtr CurrentWindowViewport => ImGui.GetWindowViewport();
    public static float CurrentWindowDockId => ImGui.GetWindowDockID();

    public static ImFontPtr Font => ImGui.GetFont();
    public static double Time => ImGui.GetTime();
}