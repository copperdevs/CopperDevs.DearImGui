#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace CopperDevs.DearImGui.Enums;

public enum TabBarFlags
{
    None = 0,
    Reorderable = 1,
    AutoSelectNewTabs = 2,
    TabListPopupButton = 4,
    NoCloseWithMiddleMouseButton = 8,
    NoTabListScrollingButtons = 16,
    NoTooltip = 32,
    DrawSelectedOverline = 64,
    FittingPolicyResizeDown = 128,
    FittingPolicyScroll = 256,
    FittingPolicyMask = FittingPolicyScroll | FittingPolicyResizeDown,
    FittingPolicyDefault = FittingPolicyResizeDown,
}