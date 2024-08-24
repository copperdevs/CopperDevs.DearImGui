namespace CopperDevs.DearImGui.Wrapping.Enums;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
[Flags]
public enum ConfigFlags
{
    None = 0,
    NavEnableKeyboard = 1,
    NavEnableGamepad = 2,
    NavEnableSetMousePos = 4,
    NavNoCaptureKeyboard = 8,
    NoMouse = 16,
    NoMouseCursorChange = 32,
    NoKeyboard = 64,
    DockingEnable = 128,
    ViewportsEnable = 1024,
    DpiEnableScaleViewports = 16384,
    DpiEnableScaleFonts = 32768,
    IsSRGB = 1048576,
    IsTouchScreen = 2097152,
}