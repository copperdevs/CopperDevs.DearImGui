using CopperDevs.DearImGui.Attributes;
using CopperDevs.DearImGui.Rendering;

namespace CopperDevs.DearImGui.Example.Raylib.Raylib_cs;

[Window("Example Window", WindowOpen = true)]
public class ExampleWindow : BaseWindow // inheriting from BaseWindow isn't required (only applying the Window attribute is), but you can inherit from it anyways so you can hard type the names
{
    private float inputFloat = 0.5f;
    private string inputString = "quick brown fox";

    public override void WindowUpdate()
    {
        CopperImGui.Text("Hello World");
        CopperImGui.Button("Save", () => Console.WriteLine("Button Click"));
        CopperImGui.Text("string", ref inputString);
        CopperImGui.SliderValue("float", ref inputFloat, 0f, 1f);
    }
}