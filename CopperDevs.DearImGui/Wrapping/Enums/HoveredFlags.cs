namespace CopperDevs.DearImGui.Wrapping.Enums;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
[Flags]
public enum HoveredFlags
{
    None = 0,
    ChildWindows = 1,
    RootWindow = 2,
    AnyWindow = 4,
    NoPopupHierarchy = 8,
    DockHierarchy = 16,
    AllowWhenBlockedByPopup = 32,
    AllowWhenBlockedByActiveItem = 128,
    AllowWhenOverlappedByItem = 256,
    AllowWhenOverlappedByWindow = 512,
    AllowWhenDisabled = 1024,
    NoNavOverride = 2048,
    AllowWhenOverlapped = AllowWhenOverlappedByWindow | AllowWhenOverlappedByItem,
    RectOnly = AllowWhenOverlapped | AllowWhenBlockedByActiveItem | AllowWhenBlockedByPopup,
    RootAndChildWindows = RootWindow | ChildWindows,
    ForTooltip = 4096,
    Stationary = 8192,
    DelayNone = 16384,
    DelayShort = 32768,
    DelayNormal = 65536,
    NoSharedDelay = 131072,
}