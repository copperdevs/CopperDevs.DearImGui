using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Renderer.OpenGl.SilkNet;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace CopperDevs.DearImGui.Example.OpenGl.SilkNet;

[SuppressMessage("ReSharper", "AccessToDisposedClosure")]
public static class Program
{
    private static GL? gl = null!;

    public static void Main()
    {
        var windowOptions = WindowOptions.Default with
        {
            Title = "CopperDevs.DearImGui Example",
            PreferredDepthBufferBits = 24,
            PreferredStencilBufferBits = 8,
            API = new GraphicsAPI(ContextAPI.OpenGL, new APIVersion(3, 3)),
        };

        using var window = Window.Create(windowOptions);

        window.Load += () =>
        {
            gl = window.CreateOpenGL();
            SetWindowStyling();

            OpenGlRenderer.SetupReferences(gl, window, window.CreateInput());
            CopperImGui.Setup<OpenGlRenderer>(true, true);
        };

        window.Render += deltaTime =>
        {
            gl?.ClearColor(Color.SkyBlue);
            gl?.Clear(ClearBufferMask.ColorBufferBit);

            OpenGlRenderer.SetDeltaTime(deltaTime);
            CopperImGui.Render();
        };

        window.FramebufferResize += newSize => gl?.Viewport(newSize);

        window.Closing += CopperImGui.Shutdown;

        window.Run();
    }

    private static void SetWindowStyling() // windows 11 window styling stuff. its not required at all but it looks really nice
    {
        if (!WindowsApi.IsWindows11)
            return;

        var currentProcessHandle = Process.GetCurrentProcess().MainWindowHandle;
        WindowsApi.SetDwmImmersiveDarkMode(currentProcessHandle, true);
        WindowsApi.SetDwmSystemBackdropType(currentProcessHandle, WindowsApi.SystemBackdropType.Acrylic);
        WindowsApi.SetDwmWindowCornerPreference(currentProcessHandle, WindowsApi.WindowCornerPreference.Default);
        WindowsApi.RegisterWindow(currentProcessHandle);
    }
}