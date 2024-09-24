namespace CopperDevs.DearImGui.Renderer.Raylib.Bindings;

[Flags]
public enum ConfigFlags : uint
{
    VSyncHint = 0x00000040,
    FullscreenMode = 0x00000002,
    ResizableWindow = 0x00000004,
    UndecoratedWindow = 0x00000008,
    HiddenWindow = 0x00000080,
    MinimizedWindow = 0x00000200,
    MaximizedWindow = 0x00000400,
    UnfocusedWindow = 0x00000800,
    TopmostWindow = 0x00001000,
    AlwaysRunWindow = 0x00000100,
    TransparentWindow = 0x00000010,
    HighDpiWindow = 0x00002000,
    MousePassthroughWindow = 0x00004000,
    BorderlessWindowMode = 0x00008000,
    Msaa4xHint = 0x00000020,
    InterlacedHint = 0x00010000,
}