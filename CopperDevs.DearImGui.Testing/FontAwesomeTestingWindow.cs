using System.Reflection;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Resources;

namespace CopperDevs.DearImGui.Testing;

[Window("Font Awesome", WindowOpen = true)]
public class FontAwesomeTestingWindow : BaseWindow
{
    private List<string> values = [];

    public override void WindowStart()
    {
        values = typeof(FontAwesomeIcons).GetAllPublicConstantValues<string>();
    }

    public override void WindowUpdate()
    {
        CopperImGui.RenderObjectValues(this);
        foreach (var value in values)
        {
            CopperImGui.Text(value);
        }
    }
}