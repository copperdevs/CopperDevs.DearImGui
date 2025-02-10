using Bliss.CSharp.Windowing;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Example.Bliss.Data;
using CopperDevs.DearImGui.Renderer.Bliss;
using Veldrid;
using BlissWindow = Bliss.CSharp.Windowing.Window;

namespace CopperDevs.DearImGui.Example.Bliss;

public class ExampleGame(ExampleGameOptions options) : SafeDisposable
{
    public readonly ExampleGameOptions Options = options;

    private IWindow window = null!;
    private GraphicsDevice graphicsDevice = null!;

    public void Setup()
    {
        window = BlissWindow.CreateWindow(WindowType.Sdl3,
            Options.WindowOptions.Size.X,
            Options.WindowOptions.Size.Y,
            Options.WindowOptions.Title,
            Options.WindowOptions.Flags,
            Options.GraphicsDeviceOptions,
            BlissWindow.GetPlatformDefaultBackend(),
            out graphicsDevice);
    }

    public void SetupImGui()
    {
        BlissRenderer.SetupReferences(window, graphicsDevice);
        CopperImGui.Setup<BlissRenderer>(RenderingSettings.DockingEnabled | RenderingSettings.UseCustomStyling);
    }

    public void Run()
    {
        while (window.Exists)
        {
            window.PumpEvents();
        }
    }

    public override void DisposeResources()
    {
        window.Dispose();
        graphicsDevice.Dispose();
    }
}