

using CopperDevs.DearImGui.Backend.Enums;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    /// <summary>
    /// Check if specifically a window is currently being hovered
    /// </summary>
    public static bool AnyWindowHovered => CurrentBackend.IsWindowHovered(HoveredFlags.AnyWindow);

    /// <summary>
    /// Check if any DearImGui element is currently being hovered
    /// </summary>
    public static bool AnyElementHovered => CurrentBackend.WantCaptureMouse;

    /// <summary>
    /// Get the currently rendering windows width
    /// </summary>
    public static float CurrentWindowWidth => CurrentBackend.GetWindowWidth();

    /// <summary>
    /// Get the currently rendering windows height
    /// </summary>
    public static float CurrentWindowHeight => CurrentBackend.GetWindowHeight();

    /// <summary>
    /// Get the currently rendering windows position
    /// </summary>
    public static Vector2 CurrentWindowPosition => CurrentBackend.GetWindowPos();

    /// <summary>
    /// Get the currently rendering windows size
    /// </summary>
    public static Vector2 CurrentWindowSize => CurrentBackend.GetWindowSize();

    /// <summary>
    /// Get the current DearImgui time
    /// </summary>
    public static double Time => CurrentBackend.GetTime();
}