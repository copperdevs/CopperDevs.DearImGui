using Veldrid;

namespace CopperDevs.DearImGui.Example.Bliss.Data;

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