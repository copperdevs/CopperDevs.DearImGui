namespace CopperDevs.DearImGui.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class SpaceAttribute : Attribute
{
    private float spacing;

    public SpaceAttribute()
    {
        spacing = 20;
    }

    public SpaceAttribute(float space)
    {
        spacing = space;
    }

    internal void Render()
    {
        CopperImGui.Space(spacing);
    }
}