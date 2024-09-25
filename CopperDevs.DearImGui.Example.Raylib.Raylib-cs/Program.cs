using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Renderer.Raylib;
using CopperDevs.DearImGui.Renderer.Raylib.Raylib_cs;
using Raylib_cs;
using Rl = Raylib_cs.Raylib;

namespace CopperDevs.DearImGui.Example.Raylib.Raylib_cs;

// this is way too extra for an example but meah i felt like it
public static class Program
{
    private const bool TransparentWindow = true;
    private static readonly Color TransparentColor = new(0, 0, 0, 0);

    public static void Main()
    {
        var configFlags = ConfigFlags.ResizableWindow | ConfigFlags.Msaa4xHint | ConfigFlags.VSyncHint | ConfigFlags.AlwaysRunWindow;
        if (WindowsApi.IsWindows11 && TransparentWindow)
            configFlags |= ConfigFlags.TransparentWindow;

        Rl.SetConfigFlags(configFlags);
        Rl.InitWindow(800, 480, "CopperDevs.DearImGui Example");
        SetWindowStyling();

        CopperImGui.Setup<RlImGuiRenderer<RlImGuiBinding>>(true, true); // setup the actual imgui layering, as well as enabling all the built in dearimgui windows
        CopperImGui.ShowDearImGuiAboutWindow = true;
        CopperImGui.ShowDearImGuiDemoWindow = true;
        CopperImGui.ShowDearImGuiMetricsWindow = true;
        CopperImGui.ShowDearImGuiDebugLogWindow = true;
        CopperImGui.ShowDearImGuiIdStackToolWindow = true;

        while (!Rl.WindowShouldClose())
            RenderGame();

        CopperImGui.Shutdown();
        Rl.CloseWindow();
    }

    private static void RenderGame() // this is in its own method so that WindowsApi.OnWindowResize can call it so the window gets redrawn on resize
    {
        Rl.BeginDrawing();
        Rl.ClearBackground(WindowsApi.IsWindows11 && TransparentWindow ? TransparentColor : Color.RayWhite);

        CopperImGui.Render();

        Rl.EndDrawing();
    }

    private static unsafe void SetWindowStyling() // windows 11 window styling stuff. its not required at all but it looks really nice
    {
        if (!WindowsApi.IsWindows11)
            return;

        var handle = new IntPtr(Rl.GetWindowHandle());

        WindowsApi.SetDwmImmersiveDarkMode(handle, true);
        WindowsApi.SetDwmSystemBackdropType(handle, WindowsApi.SystemBackdropType.Acrylic);
        WindowsApi.SetDwmWindowCornerPreference(handle, WindowsApi.WindowCornerPreference.Default);

        WindowsApi.RegisterWindow(handle);

        WindowsApi.OnWindowResize += _ => { RenderGame(); };
    }
}