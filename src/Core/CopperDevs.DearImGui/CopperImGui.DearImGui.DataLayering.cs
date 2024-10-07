using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    /// <summary>
    ///     Check if specifically a window is currently being hovered
    /// </summary>
    public static bool AnyWindowHovered => ImGui.IsWindowHovered(ImGuiHoveredFlags.AnyWindow);

    /// <summary>
    ///     Check if any DearImGui element is currently being hovered
    /// </summary>
    public static bool AnyElementHovered => ImGui.GetIO().WantCaptureMouse;

    /// <summary>
    ///     Get the currently rendering windows width
    /// </summary>
    public static float CurrentWindowWidth => ImGui.GetWindowWidth();

    /// <summary>
    ///     Get the currently rendering windows height
    /// </summary>
    public static float CurrentWindowHeight => ImGui.GetWindowHeight();

    /// <summary>
    ///     Get the currently rendering windows position
    /// </summary>
    public static Vector2 CurrentWindowPosition => ImGui.GetWindowPos();

    /// <summary>
    ///     Get the currently rendering windows size
    /// </summary>
    public static Vector2 CurrentWindowSize => ImGui.GetWindowSize();

    /// <summary>
    ///     Get the current DearImGui time
    /// </summary>
    public static double Time => ImGui.GetTime();
}