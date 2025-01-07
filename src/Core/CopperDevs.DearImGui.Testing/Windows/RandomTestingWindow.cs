using System.Numerics;

namespace CopperDevs.DearImGui.Testing.Windows;

public class RandomTestingWindow() : Window("Random Testing", false)
{
    private Vector3 vector3 = Vector3.Zero;
    
    public override void Render()
    {
        CopperImGui.DragValue(nameof(vector3), ref vector3);
    }
}