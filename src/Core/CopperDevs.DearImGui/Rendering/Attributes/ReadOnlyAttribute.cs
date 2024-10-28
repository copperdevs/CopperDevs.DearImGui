namespace CopperDevs.DearImGui.Rendering;

/// <summary>
/// Disable the editing of a field
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class ReadOnlyAttribute : Attribute
{
}