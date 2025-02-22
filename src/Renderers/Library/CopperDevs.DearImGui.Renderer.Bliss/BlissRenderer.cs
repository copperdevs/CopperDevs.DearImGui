using Bliss.CSharp.Windowing;
using CopperDevs.DearImGui.Rendering;
using Veldrid;

namespace CopperDevs.DearImGui.Renderer.Bliss;

public class BlissRenderer : ImGuiRenderer
{
    internal static IWindow Window = null!;
    internal static GraphicsDevice Device = null!;
    internal static CommandList CommandList = null!;

    internal static double DeltaTime;
    
    public static void SetupReferences(IWindow targetWindow, GraphicsDevice targetDevice, CommandList commandList)
    {
        Window = targetWindow;
        Device = targetDevice;
        CommandList = commandList;
    }

    public static void SetDeltaTime(double deltaTimeValue) => DeltaTime = deltaTimeValue;

    public override void Setup()
    {
        BlissImGui.Setup();
    }

    public override void Begin()
    {
        BlissImGui.Begin();
    }

    public override void End()
    {
        BlissImGui.End();
    }

    public override void Shutdown()
    {
        BlissImGui.Shutdown();
    }
}