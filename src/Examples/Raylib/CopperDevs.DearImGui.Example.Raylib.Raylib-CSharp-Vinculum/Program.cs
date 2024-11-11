using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Renderer.Raylib;
using CopperDevs.DearImGui.Renderer.Raylib.Raylib_CSharp_Vinculum;
using ZeroElectric.Vinculum;
using Rl = ZeroElectric.Vinculum.Raylib;

namespace CopperDevs.DearImGui.Example.Raylib.Raylib_CSharp_Vinculum;

// this is way too extra for an example but meah i felt like it
public static class Program
{
    private const bool TransparentWindow = true;
    private static readonly Color TransparentColor = new(0, 0, 0, 0);

    public static void Main()
    {
        var configFlags = ConfigFlags.FLAG_WINDOW_RESIZABLE | ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_ALWAYS_RUN;
        if (WindowsApi.IsWindows11 && TransparentWindow)
            configFlags |= ConfigFlags.FLAG_WINDOW_TRANSPARENT;

        Rl.SetConfigFlags(configFlags);
        Rl.InitWindow(800, 480, "CopperDevs.DearImGui Example");
        SetWindowStyling();

        CopperImGui.Setup<RlImGuiRenderer<RlImGuiBinding>>(); // setup the actual imgui layering, as well as enabling all the built in dearimgui windows
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
        Rl.ClearBackground(WindowsApi.IsWindows11 && TransparentWindow ? TransparentColor : Rl.RAYWHITE);

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