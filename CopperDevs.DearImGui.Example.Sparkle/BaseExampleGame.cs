using CopperDevs.DearImGui.Renderer.Raylib;
using Sparkle.CSharp;
using SparkleLogger = Sparkle.CSharp.Logging.Logger;

namespace CopperDevs.DearImGui.Example.Sparkle;

public class BaseExampleGame : Game
{
    public BaseExampleGame(GameSettings settings) : base(settings) {
        SparkleLogger.Message += Utility.CustomLog;
    }

    protected override void Init()
    {
        base.Init();
        
        CopperImGui.Setup<RlImGuiRenderer>(true, true);
        CopperImGui.ShowDearImGuiAboutWindow = true;
        CopperImGui.ShowDearImGuiDemoWindow = true;
        CopperImGui.ShowDearImGuiMetricsWindow = true;
        CopperImGui.ShowDearImGuiDebugLogWindow = true;
        CopperImGui.ShowDearImGuiIdStackToolWindow = true;
        
        Utility.SetWindowStyling();
    }

    protected override void Draw()
    {
        base.Draw();
        
        CopperImGui.Render();
    }

    protected override void OnClose()
    {
        base.OnClose();
        
        CopperImGui.Shutdown();
    }
}