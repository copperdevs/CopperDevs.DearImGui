namespace CopperDevs.DearImGui.Rendering;

/// <summary>
/// Decides what elements of the script will be rendered
/// </summary>
public enum RenderingType
{
    /// <summary>
    /// Renders every field that is public
    /// </summary>
    Public,
    
    /// <summary>
    /// Renders every field that has the <see cref="CopperDevs.DearImGui.Attributes.ExposedAttribute"/>
    /// </summary>
    Exposed,
    
    /// <summary>
    /// Renders every field regardless if its private or public
    /// </summary>
    All
}