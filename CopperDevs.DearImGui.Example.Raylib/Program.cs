using CopperDevs.Core.Utility;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace CopperDevs.DearImGui.Example.Raylib;

// this is way to extra for an example but meah i felt like it
public static class Program
{
    private static readonly Color TransparentColor = new(0, 0, 0, 0);
    private const bool TransparentWindow = true;

    public static void Main()
    {
        var configFlags = ConfigFlags.ResizableWindow | ConfigFlags.Msaa4xHint | ConfigFlags.VSyncHint | ConfigFlags.AlwaysRunWindow;
        if (WindowsApi.IsWindows11 && TransparentWindow) configFlags |= ConfigFlags.TransparentWindow;

        SetConfigFlags(configFlags);
        InitWindow(800, 480, "CopperDevs.DearImGui Example");
        SetWindowStyling();

        CopperImGui.Setup<RlImGuiRenderer>(); // setup the actual imgui layering, as well as enabling all the built in dearimgui windows
        CopperImGui.ShowDearImGuiAboutWindow = true;
        CopperImGui.ShowDearImGuiDemoWindow = true;
        CopperImGui.ShowDearImGuiMetricsWindow = true;
        CopperImGui.ShowDearImGuiDebugLogWindow = true;
        CopperImGui.ShowDearImGuiIdStackToolWindow = true;

        while (!WindowShouldClose())
            RenderGame();

        CopperImGui.Shutdown();
        CloseWindow();
    }

    private static void RenderGame() // this is in its own method so that WindowsApi.OnWindowResize can call it so the window gets redrawn on resize
    {
        BeginDrawing();
        ClearBackground(WindowsApi.IsWindows11 && TransparentWindow ? TransparentColor : Color.RayWhite);

        CopperImGui.Render();

        EndDrawing();
    }

    private static void SetWindowStyling() // windows 11 window styling stuff. its not required at all but it looks really nice
    {
        unsafe
        {
            var handle = new IntPtr(GetWindowHandle());

            WindowsApi.SetDwmImmersiveDarkMode(handle, true);
            WindowsApi.SetDwmSystemBackdropType(handle, WindowsApi.SystemBackdropType.Acrylic);
            WindowsApi.SetDwmWindowCornerPreference(handle, WindowsApi.WindowCornerPreference.Default);

            WindowsApi.RegisterWindow(handle);

            WindowsApi.OnWindowResize += _ => { RenderGame(); };
        }
    }
}