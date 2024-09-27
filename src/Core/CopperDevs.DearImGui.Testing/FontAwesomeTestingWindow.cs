using CopperDevs.DearImGui.Attributes;
using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Resources;

namespace CopperDevs.DearImGui.Testing;

[Window("Font Awesome", WindowOpen = false)]
public class FontAwesomeTestingWindow : BaseWindow
{
    private List<string> values = [];

    public override void WindowStart()
    {
        Task.Run(() => { values = typeof(FontAwesomeIcons).GetAllPublicConstantValues<string>(); });
    }


    public override void WindowUpdate()
    {
        foreach (var value in values) CopperImGui.Text(value);
    }
}