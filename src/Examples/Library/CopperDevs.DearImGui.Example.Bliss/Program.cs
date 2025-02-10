using CopperDevs.DearImGui.Example.Bliss.Data;

namespace CopperDevs.DearImGui.Example.Bliss;

public static class Program
{
    public static void Main()
    {
        var options = new ExampleGameOptions();
        using var game = new ExampleGame(options);
        
        game.Setup();
        game.Run();
    }
}