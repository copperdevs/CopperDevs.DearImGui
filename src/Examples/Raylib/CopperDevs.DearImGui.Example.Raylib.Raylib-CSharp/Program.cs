using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Renderer.Raylib;
using CopperDevs.DearImGui.Renderer.Raylib.Raylib_CSharp;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;
using static Raylib_CSharp.Raylib;
using GameWindow = Raylib_CSharp.Windowing.Window;

namespace CopperDevs.DearImGui.Example.Raylib.Raylib_CSharp;

// this is way too extra for an example but meah i felt like it
public static class Program
{
    private const bool TransparentWindow = true;
    private static readonly Color TransparentColor = new(0, 0, 0, 0);

    public static void Main()
    {
        var configFlags = ConfigFlags.ResizableWindow | ConfigFlags.Msaa4XHint | ConfigFlags.VSyncHint | ConfigFlags.AlwaysRunWindow;
        if (WindowsApi.IsWindows11 && TransparentWindow)
            configFlags |= ConfigFlags.TransparentWindow;

        SetConfigFlags(configFlags);
        GameWindow.Init(800, 480, "CopperDevs.DearImGui Example");
        SetWindowStyling();

        CopperImGui.Setup<RlImGuiRenderer<RlImGuiBinding>>(); // setup the actual imgui layering, as well as enabling all the built in dearimgui windows
        CopperImGui.ShowDearImGuiAboutWindow = true;
        CopperImGui.ShowDearImGuiDemoWindow = true;
        CopperImGui.ShowDearImGuiMetricsWindow = true;
        CopperImGui.ShowDearImGuiDebugLogWindow = true;
        CopperImGui.ShowDearImGuiIdStackToolWindow = true;

        while (!GameWindow.ShouldClose())
            RenderGame();

        CopperImGui.Shutdown();
        GameWindow.Close();
    }

    private static void RenderGame() // this is in its own method so that WindowsApi.OnWindowResize can call it so the window gets redrawn on resize
    {
        Graphics.BeginDrawing();
        Graphics.ClearBackground(WindowsApi.IsWindows11 && TransparentWindow ? TransparentColor : Color.RayWhite);

        CopperImGui.Render();

        Graphics.EndDrawing();
    }

    private static void SetWindowStyling() // windows 11 window styling stuff. its not required at all but it looks really nice
    {
        if (!WindowsApi.IsWindows11)
            return;

        var handle = GameWindow.GetHandle();

        WindowsApi.SetDwmImmersiveDarkMode(handle, true);
        WindowsApi.SetDwmSystemBackdropType(handle, WindowsApi.SystemBackdropType.Acrylic);
        WindowsApi.SetDwmWindowCornerPreference(handle, WindowsApi.WindowCornerPreference.Default);

        WindowsApi.RegisterWindow(handle);

        WindowsApi.OnWindowResize += _ => { RenderGame(); };
    }
}