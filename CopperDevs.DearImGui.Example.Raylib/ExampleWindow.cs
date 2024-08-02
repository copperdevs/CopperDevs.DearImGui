using CopperDevs.DearImGui.Attributes;
using ImGuiNET;

namespace CopperDevs.DearImGui.Example.Raylib;

[Window("Example Window", WindowOpen = true)]
public class ExampleWindow : BaseWindow
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