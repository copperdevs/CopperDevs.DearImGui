#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace CopperDevs.DearImGui.Enums;

public enum ChildFlags
{
    None = 0,
    Border = 1,
    AlwaysUseWindowPadding = 2,
    ResizeX = 4,
    ResizeY = 8,
    AutoResizeX = 16, 
    AutoResizeY = 32, 
    AlwaysAutoResize = 64, 
    FrameStyle = 128,
    NavFlattened = 256, 
}