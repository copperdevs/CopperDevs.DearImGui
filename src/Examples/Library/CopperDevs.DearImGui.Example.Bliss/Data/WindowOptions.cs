using Bliss.CSharp.Windowing;
using CopperDevs.Core.Data;

namespace CopperDevs.DearImGui.Example.Bliss.Data;

public record WindowOptions
{
    public string Title = "Window";
    public Vector2Int Size = new(1150, 680);
    public WindowState Flags = WindowState.Resizable;
}