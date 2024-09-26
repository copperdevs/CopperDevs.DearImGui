
namespace CopperDevs.DearImGui.Utility;

/// <summary>
/// Scope for specifying a region of code as disabled when rendered
/// </summary>
public sealed class DisabledScope : Scope
{
    private readonly bool condition;

    /// <summary>
    /// Create a disabled scope
    /// </summary>
    public DisabledScope() : this(true)
    {
    }

    /// <summary>
    /// Create a disabled scope where it's disabled on a condition
    /// </summary>
    /// <param name="condition">Condition to disable by</param>
    public DisabledScope(bool condition)
    {
        this.condition = condition;

        if (condition)
            CopperImGui.CurrentBackend.BeginDisabled();
    }

    /// <summary>
    /// Close the scope
    /// </summary>
    protected override void CloseScope()
    {
        if (condition)
            CopperImGui.CurrentBackend.EndDisabled();
    }
}