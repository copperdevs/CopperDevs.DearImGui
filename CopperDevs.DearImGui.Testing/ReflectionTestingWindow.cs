using CopperDevs.DearImGui.Attributes;

namespace CopperDevs.DearImGui.Testing;

[Window("Reflection Testing", WindowOpen = true)]
public class ReflectionTestingWindow : BaseWindow
{
    private ReflectionTest reflectionTest = new();
    
    public override void WindowUpdate()
    {
        CopperImGui.RenderObjectValues(reflectionTest);
    }
}