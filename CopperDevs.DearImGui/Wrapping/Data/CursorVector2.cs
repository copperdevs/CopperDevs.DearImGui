using System.Diagnostics.CodeAnalysis;

namespace CopperDevs.DearImGui.Wrapping.Data;

/// <summary>
/// Vector2 wrapping the DearImGui cursor
/// </summary>
[SuppressMessage("Performance", "CA1822:Mark members as static")]
[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
public class CursorVector2
{
    /// <summary>
    /// X position of the cursor
    /// </summary>
    public float X
    {
        get => CopperImGui.CurrentBackend.GetCursorXPos();
        set => CopperImGui.CurrentBackend.SetCursorXPos(value);
    }

    /// <summary>
    /// Y position of the cursor
    /// </summary>
    public float Y
    {
        get => CopperImGui.CurrentBackend.GetCursorYPos();
        set => CopperImGui.CurrentBackend.SetCursorYPos(value);
    }
}