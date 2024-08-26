using CopperDevs.DearImGui.Attributes;
using CopperDevs.DearImGui.Rendering;

namespace CopperDevs.DearImGui.Testing;

[Window("Reflection Testing", WindowOpen = false)]
public class ReflectionTestingWindow : BaseWindow
{
    private ReflectionTest reflectionTest = new();
    
    public override void WindowUpdate()
    {
        CopperImGui.RenderObjectValues(reflectionTest);
    }
}