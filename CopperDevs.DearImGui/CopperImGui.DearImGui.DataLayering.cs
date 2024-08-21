

using CopperDevs.DearImGui.Enums;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    /// <summary>
    /// Check if specifically a window is currently being hovered
    /// </summary>
    public static bool AnyWindowHovered => currentBinding.IsWindowHovered(HoveredFlags.AnyWindow);

    /// <summary>
    /// Check if any DearImGui element is currently being hovered
    /// </summary>
    public static bool AnyElementHovered => currentBinding.WantCaptureMouse;

    /// <summary>
    /// Get the currently rendering windows width
    /// </summary>
    public static float CurrentWindowWidth => currentBinding.GetWindowWidth();

    /// <summary>
    /// Get the currently rendering windows height
    /// </summary>
    public static float CurrentWindowHeight => currentBinding.GetWindowHeight();

    /// <summary>
    /// Get the currently rendering windows position
    /// </summary>
    public static Vector2 CurrentWindowPosition => currentBinding.GetWindowPos();

    /// <summary>
    /// Get the currently rendering windows size
    /// </summary>
    public static Vector2 CurrentWindowSize => currentBinding.GetWindowSize();

    /// <summary>
    /// Get the current DearImgui time
    /// </summary>
    public static double Time => currentBinding.GetTime();
}