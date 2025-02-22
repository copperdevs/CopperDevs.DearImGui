using Bliss.CSharp.Colors;
using Bliss.CSharp.Interact;
using Bliss.CSharp.Transformations;
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
    private CommandList commandList = null!;

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

        commandList = graphicsDevice.ResourceFactory.CreateCommandList();

        window.Resized += OnResize;

        BlissRenderer.SetupReferences(window, graphicsDevice, commandList);
        CopperImGui.Setup<BlissRenderer>(RenderingSettings.DockingEnabled | RenderingSettings.UseCustomStyling);
    }

    public void Run()
    {
        while (window.Exists)
        {
            window.PumpEvents();
            Input.Begin();

            if (!window.Exists)
            {
                break;
            }

            Update();
        }
    }

    private void Update()
    {
        commandList.Begin();
        commandList.SetFramebuffer(graphicsDevice.SwapchainFramebuffer);
        commandList.ClearColorTarget(0, Color.DarkGray.ToRgbaFloat());

        CopperImGui.Render();

        commandList.End();
        graphicsDevice.SubmitCommands(commandList);
        graphicsDevice.SwapBuffers();
    }

    protected virtual void OnResize()
    {
        graphicsDevice.MainSwapchain.Resize((uint)window.GetWidth(), (uint)window.GetHeight());
    }

    public override void DisposeResources()
    {
        window.Dispose();
        graphicsDevice.Dispose();
        CopperImGui.Shutdown();
    }
}