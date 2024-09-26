namespace CopperDevs.DearImGui.Attributes;

/// <summary>
/// Attribute for exposing an object to be shown via reflection
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class ExposedAttribute : Attribute
{
}