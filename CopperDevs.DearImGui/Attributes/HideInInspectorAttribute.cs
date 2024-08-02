namespace CopperDevs.DearImGui.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class HideInInspectorAttribute : Attribute
{
}