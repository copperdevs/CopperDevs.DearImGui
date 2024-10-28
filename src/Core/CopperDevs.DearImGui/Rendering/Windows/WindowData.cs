using Hexa.NET.ImGui;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace CopperDevs.DearImGui.Rendering;

public readonly struct WindowData
{
    private readonly WindowAttribute windowAttribute;

    private readonly BaseWindow? classWindow;

    public WindowType windowType => (classWindow is null) ? WindowType.Attribute : WindowType.Class;
    public object TargetClass => (windowType == WindowType.Attribute ? windowAttribute?.TargetClass : classWindow)!;

    public ref bool WindowOpen => ref windowAttribute.WindowOpen;
    public string WindowName => windowAttribute.WindowName;
    public ImGuiWindowFlags WindowFlags => windowAttribute.Flags;

    internal WindowData(WindowAttribute attributeWindow)
    {
        classWindow = null;
        windowAttribute = attributeWindow;
    }

    internal WindowData(BaseWindow classWindow)
    {
        this.classWindow = classWindow;

        var attribute = (WindowAttribute)(this.classWindow.GetType()).GetCustomAttribute(typeof(WindowAttribute))!;
        attribute.GetMethods(this.classWindow);
        windowAttribute = attribute;
    }

    internal void StartWindow() => windowAttribute?.Start();

    internal void UpdateWindow() => windowAttribute?.Update();

    internal void StopWindow() => windowAttribute?.Stop();

    public enum WindowType
    {
        Class,
        Attribute,
    }
}