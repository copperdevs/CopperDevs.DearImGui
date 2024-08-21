using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Binding.ImGuiNET;
using CopperDevs.DearImGui.Renderer.Raylib;
using ImGuiNET;
using Raylib_CSharp;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;
using static Raylib_CSharp.Raylib;

namespace CopperDevs.DearImGui.Testing;

public static class Program
{
    private static readonly Color TransparentColor = new(0, 0, 0, 0);
    private const bool TransparentWindow = true;

    public static void Main()
    {
        RaylibLogger.Initialize();

        var configFlags = ConfigFlags.ResizableWindow | ConfigFlags.Msaa4XHint | ConfigFlags.VSyncHint | ConfigFlags.AlwaysRunWindow;
        if (WindowsApi.IsWindows11 && TransparentWindow) configFlags |= ConfigFlags.TransparentWindow;

        SetConfigFlags(configFlags);
        Window.Init(800, 480, "CopperDevs.DearImGui Example");
        SetWindowStyling();

        CopperImGui.Setup<RlImGuiRenderer, ImGuiNETBindings>(true, true);

        while (!Window.ShouldClose())
            RenderGame();

        CopperImGui.Shutdown();
        Window.Close();
    }

    private static void RenderGame()
    {
        Graphics.BeginDrawing();
        Graphics.ClearBackground(WindowsApi.IsWindows11 && TransparentWindow ? TransparentColor : Color.RayWhite);

        CopperImGui.Render();

        Graphics.EndDrawing();
    }

    private static void SetWindowStyling()
    {
        var handle = Window.GetHandle();

        WindowsApi.SetDwmImmersiveDarkMode(handle, true);
        WindowsApi.SetDwmSystemBackdropType(handle, WindowsApi.SystemBackdropType.Acrylic);
        WindowsApi.SetDwmWindowCornerPreference(handle, WindowsApi.WindowCornerPreference.Default);

        WindowsApi.RegisterWindow(handle);

        WindowsApi.OnWindowResize += _ => { RenderGame(); };
    }
}