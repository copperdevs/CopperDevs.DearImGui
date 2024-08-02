namespace CopperDevs.DearImGui.Attributes;

/// <summary>
/// Attribute to always hide an object from being shown via reflection
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class HideInInspectorAttribute : Attribute
{
}