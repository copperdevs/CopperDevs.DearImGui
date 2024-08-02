using ImGuiNET;

namespace CopperDevs.DearImGui.Utility;

public class IndentScope : Scope
{
    private readonly bool condition;

    public IndentScope() : this(true)
    {
    }

    public IndentScope(bool condition)
    {
        this.condition = condition;

        if (condition)
            ImGui.Indent();
    }

    protected override void CloseScope()
    {
        if (condition)
            ImGui.Unindent();
    }
}