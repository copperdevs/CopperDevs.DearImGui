using ImGuiNET;

namespace CopperDevs.DearImGui.Utility;

public class DisabledScope : Scope
{
    private readonly bool condition;

    public DisabledScope() : this(true)
    {
    }

    public DisabledScope(bool condition)
    {
        this.condition = condition;

        if (condition)
            ImGui.BeginDisabled();
    }

    protected override void CloseScope()
    {
        if (condition)
            ImGui.EndDisabled();
    }
}