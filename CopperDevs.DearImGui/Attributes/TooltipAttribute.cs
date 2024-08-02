namespace CopperDevs.DearImGui.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class TooltipAttribute : Attribute
{
    public readonly string Message;

    public TooltipAttribute(string message)
    {
        Message = message;
    }
}