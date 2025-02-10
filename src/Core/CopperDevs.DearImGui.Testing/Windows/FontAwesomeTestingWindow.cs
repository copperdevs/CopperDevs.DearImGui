using CopperDevs.DearImGui.Resources;
using CopperDevs.DearImGui.Utility;

namespace CopperDevs.DearImGui.Testing.Windows;

public class FontAwesomeTestingWindow() : Window("Font Awesome", false)
{
    private List<string> values = [];

    public override void OnLoad()
    {
        Task.Run(() => { values = typeof(FontAwesomeIcons).GetAllPublicConstantValues<string>(); });
    }

    public override void Render()
    {
        foreach (var value in values)
            CopperImGui.Text(value);
    }
}