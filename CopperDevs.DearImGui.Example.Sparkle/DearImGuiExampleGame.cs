using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Renderer.Raylib;
using CopperDevs.Logger;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;
using Sparkle.CSharp;
using Sparkle.CSharp.Logging;
using SparkleLogger = Sparkle.CSharp.Logging.Logger;

namespace CopperDevs.DearImGui.Example.Sparkle;

public class DearImGuiExampleGame : Game
{
    public DearImGuiExampleGame(GameSettings settings) : base(settings) {
        SparkleLogger.Message += Program.CustomLog;
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
        
        SetWindowStyling();
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
    
    private static void SetWindowStyling() // windows 11 window styling stuff. its not required at all but it looks really nice
    {
        if (!WindowsApi.IsWindows11)
            return;
        
        var handle = Window.GetHandle();

        WindowsApi.SetDwmImmersiveDarkMode(handle, true);
        WindowsApi.SetDwmSystemBackdropType(handle, WindowsApi.SystemBackdropType.Acrylic);
        WindowsApi.SetDwmWindowCornerPreference(handle, WindowsApi.WindowCornerPreference.Default);
    }
}