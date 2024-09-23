using Raylib_CSharp.Windowing;
using Sparkle.CSharp;

namespace CopperDevs.DearImGui.Example.Sparkle;

public static class Program
{
    private static void Main()
    {
        var settings = new GameSettings()
        {
            Title = "CopperDevs.DearImGui Example",
            WindowFlags = ConfigFlags.ResizableWindow | ConfigFlags.Msaa4XHint | ConfigFlags.VSyncHint | ConfigFlags.AlwaysRunWindow,
            Width = 650,
            Height = 450,
        };

        using var game = new OverlayExampleGame(settings);
        // using var game = new BaseExampleGame(settings);
        game.Run(null);
    }
}