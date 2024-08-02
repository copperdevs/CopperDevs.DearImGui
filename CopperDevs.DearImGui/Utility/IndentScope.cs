using ImGuiNET;

namespace CopperDevs.DearImGui.Utility;

/// <summary>
/// Create a indent scope
/// </summary>
public sealed class IndentScope : Scope
{
    private readonly bool condition;

    /// <summary>
    /// Create a disabled scope
    /// </summary>
    public IndentScope() : this(true)
    {
    }

    /// <summary>
    /// Create an indented scope where it's indented on a condition
    /// </summary>
    /// <param name="condition">Condition to indent by</param>
    public IndentScope(bool condition)
    {
        this.condition = condition;

        if (condition)
            ImGui.Indent();
    }

    /// <summary>
    /// Close the scope
    /// </summary>
    protected override void CloseScope()
    {
        if (condition)
            ImGui.Unindent();
    }
}