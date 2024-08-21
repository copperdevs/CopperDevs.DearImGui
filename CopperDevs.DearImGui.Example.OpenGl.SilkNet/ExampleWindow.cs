using CopperDevs.DearImGui.Attributes;

namespace CopperDevs.DearImGui.Example.OpenGl.SilkNet;

[Window("Example Window", WindowOpen = true)]
public class ExampleWindow : BaseWindow // inheriting from BaseWindow isn't required (only applying the Window attribute is), but you can inherit from it anyways so you can hard type the names
{
    private string inputString = "quick brown fox";
    private float inputFloat = 0.5f;

    public override void WindowUpdate()
    {
        CopperImGui.Text("Hello World");
        CopperImGui.Button("Save", () => Console.WriteLine("Button Click"));
        CopperImGui.Text("string", ref inputString);
        CopperImGui.SliderValue("float", ref inputFloat, 0f, 1f);
    }
}