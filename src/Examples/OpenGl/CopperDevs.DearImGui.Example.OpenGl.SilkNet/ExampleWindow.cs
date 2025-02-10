namespace CopperDevs.DearImGui.Example.OpenGl.SilkNet;

public class ExampleWindow() : Window("Example Window", true)
{
    private string inputString = "quick brown fox";
    private float inputFloat = 0.5f;

    public override void Render()
    {
        CopperImGui.Text("Hello World");
        CopperImGui.Button("Save", () => Console.WriteLine("Button Click"));
        CopperImGui.Text("string", ref inputString);
        CopperImGui.SliderValue("float", ref inputFloat, 0f, 1f);
    }
}