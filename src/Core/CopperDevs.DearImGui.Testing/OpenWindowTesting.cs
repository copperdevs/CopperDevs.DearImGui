using System.Reflection;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.DearImGui.Rendering;

namespace CopperDevs.DearImGui.Testing;

[Window("Open Window Testing", WindowOpen = true)]
public class OpenWindowTesting : BaseWindow
{
    public override void WindowUpdate()
    {
        // CopperImGui.GetWindow<RandomTestingWindow>()!.WindowOpen = true;

        CopperImGui.Button("open random testing window", () => SetWindowOpen<RandomTestingWindow>(true));

        // SetWindowOpen<RandomTestingWindow>(true);
    }

    public static void SetWindowOpen<T>(bool isOpen)
    {
        var windows = (List<WindowAttribute>)typeof(CopperImGui).GetField("windows", BindingFlags.Static | BindingFlags.NonPublic)!.GetValue(null)!;

        var targetWindow = windows.FirstOrDefault(window =>
        {
            var targetClass = window.GetType().GetField("TargetClass", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(window);

            return targetClass?.GetType() == typeof(T);
        });

        targetWindow!.WindowOpen = isOpen;
    }
}