using Bliss.CSharp.Windowing;
using CopperDevs.Core.Utility;
using Veldrid;

namespace CopperDevs.DearImGui.Example.Bliss;

public class ExampleGame(ExampleGameOptions options) : SafeDisposable
{
    public readonly ExampleGameOptions Options = options;
    
    private IWindow window = null!;
    private GraphicsDevice graphicsDevice = null!;


    public void Setup()
    {
        window = Window.CreateWindow(WindowType.Sdl3,
            Options.WindowOptions.Size.X,
            Options.WindowOptions.Size.Y,
            Options.WindowOptions.Title,
            Options.WindowOptions.Flags,
            Options.GraphicsDeviceOptions,
            Window.GetPlatformDefaultBackend(),
            out graphicsDevice);
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