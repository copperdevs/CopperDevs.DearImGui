using Bliss.CSharp.Windowing;
using CopperDevs.Core.Data;
using Veldrid;

namespace CopperDevs.DearImGui.Example.Bliss;

public record ExampleGameOptions
{
    public GraphicsDeviceOptions GraphicsDeviceOptions = new()
    {
        Debug = false,
        HasMainSwapchain = true,
        SwapchainDepthFormat = null,
        SyncToVerticalBlank = true,
        ResourceBindingModel = ResourceBindingModel.Improved,
        PreferDepthRangeZeroToOne = true,
        PreferStandardClipSpaceYDirection = true,
        SwapchainSrgbFormat = false
    };

    public WindowOptions WindowOptions = new();

    public static ExampleGameOptions Default => new();
}

public record WindowOptions
{
    public string Title = "Window";
    public Vector2Int Size = new(1150, 680);
    public WindowState Flags = WindowState.Resizable;
}