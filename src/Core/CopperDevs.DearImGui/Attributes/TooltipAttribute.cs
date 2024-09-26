namespace CopperDevs.DearImGui.Attributes;

/// <summary>
/// Add a tooltip on hover to a field
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class TooltipAttribute : Attribute
{
    /// <summary>
    /// Tooltip message
    /// </summary>
    public readonly string Message;

    /// <summary>
    /// Tooltip with a specific message
    /// </summary>
    /// <param name="message">Target message</param>
    public TooltipAttribute(string message)
    {
        Message = message;
    }
}