using CopperDevs.DearImGui.Renderer.OpenGl.SilkNet.Internal;
using ImGuiNET;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace CopperDevs.DearImGui.Renderer.OpenGl.SilkNet;

public class OpenGlRenderer : IImGuiRenderer
{
    public static Action<ImFontAtlasPtr> SetupUserFonts = null!;

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

    public void Setup()
    {
        controller = new ImGuiController(targetGl, targetWindow, targetInputContext);
    }

    public void Begin()
    {
        controller.Update((float)deltaTime);
    }

    public void End()
    {
        controller.Render();
    }

    public void Shutdown()
    {
        controller.Dispose();
    }
}