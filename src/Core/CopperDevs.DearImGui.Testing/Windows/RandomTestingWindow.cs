using System.Numerics;
using CopperDevs.DearImGui.Rendering;

namespace CopperDevs.DearImGui.Testing.Windows;

[Window("Random Testing", WindowOpen = false)]
public class RandomTestingWindow : BaseWindow
{
    private Vector3 vector3 = Vector3.Zero;
    
    public override void WindowUpdate()
    {
        CopperImGui.DragValue(nameof(vector3), ref vector3);
    }
}