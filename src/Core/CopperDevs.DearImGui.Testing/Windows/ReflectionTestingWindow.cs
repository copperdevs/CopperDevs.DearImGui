using CopperDevs.DearImGui.Rendering;

namespace CopperDevs.DearImGui.Testing.Windows;

public class ReflectionTestingWindow() : Window("Reflection Testing")
{
    private ReflectionTest reflectionTest = new();
    
    public override void Render()
    {
        CopperImGui.RenderObjectValues(reflectionTest);
    }
}