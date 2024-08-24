namespace CopperDevs.DearImGui.Wrapping.Enums;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
[Flags]
public enum BackendFlags
{
    None = 0,
    HasGamepad = 1,
    HasMouseCursors = 2,
    HasSetMousePos = 4,
    RendererHasVtxOffset = 8,
    PlatformHasViewports = 1024,
    HasMouseHoveredViewport = 2048,
    RendererHasViewports = 4096,
}