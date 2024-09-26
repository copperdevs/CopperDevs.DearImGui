using CopperDevs.DearImGui.Renderer.OpenGl.SilkNet.Internal;
using CopperDevs.DearImGui.Rendering;
using ImGuiNET;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace CopperDevs.DearImGui.Renderer.OpenGl.SilkNet;

public class OpenGlRenderer : ImGuiRenderer
{
    private static ImGuiController controller = null!;

    private static GL targetGl = null!;
    private static IWindow targetWindow;
    private static IInputContext targetInputContext = null!;

    private static double deltaTime;

    public static void SetupReferences(GL gl, IWindow window, IInputContext input)
    {
        targetGl = gl;
        targetWindow = window;
        targetInputContext = input;
    }

    public static void SetDeltaTime(double newDeltaTime) => deltaTime = newDeltaTime;

    public override void Setup()
    {
        controller = new ImGuiController(targetGl, targetWindow, targetInputContext);
    }

    public override void Begin()
    {
        controller.Update((float)deltaTime);
    }

    public override void End()
    {
        controller.Render();
    }

    public override void Shutdown()
    {
        controller.Dispose();
    }
}