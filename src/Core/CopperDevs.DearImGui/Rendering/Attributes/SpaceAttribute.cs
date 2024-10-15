namespace CopperDevs.DearImGui.Rendering;

/// <summary>
/// Add a space before an attribute is rendered
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class SpaceAttribute : Attribute
{
    private readonly float spacing;

    /// <summary>
    /// Default spacing
    /// </summary>
    public SpaceAttribute()
    {
        spacing = 20;
    }

    /// <summary>
    /// Specific spacing
    /// </summary>
    /// <param name="space">Amount of spacing</param>
    public SpaceAttribute(float space)
    {
        spacing = space;
    }

    internal void Render()
    {
        CopperImGui.Space(spacing);
    }
}