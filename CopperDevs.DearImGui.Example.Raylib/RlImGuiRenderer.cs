using rlImGui_cs;

namespace CopperDevs.DearImGui.Example.Raylib;

public class RlImGuiRenderer : IImGuiRenderer
{
    public void Setup()
    {
        rlImGui.Setup(true, true);
    }

    public void Begin()
    {
        rlImGui.Begin();
    }

    public void End()
    {
        rlImGui.End();
    }

    public void Shutdown()
    {
        rlImGui.Shutdown();
    }
}