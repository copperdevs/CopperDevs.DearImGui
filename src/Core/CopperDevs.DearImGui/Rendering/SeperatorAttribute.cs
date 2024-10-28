namespace CopperDevs.DearImGui.Rendering;

/// <summary>
/// Add a seperator line above the field before rendering it
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class SeperatorAttribute : Attribute
{
    private readonly string? seperatorText;

    /// <summary>
    /// Create a normal seperator with no text
    /// </summary>
    public SeperatorAttribute()
    {
        seperatorText = null;
    }

    /// <summary>
    /// Created a seperator with specific text
    /// </summary>
    /// <param name="text">Text to display</param>
    public SeperatorAttribute(string text)
    {
        seperatorText = text;
    }

    internal void Render()
    {
        switch (seperatorText is null)
        {
            case true:
                CopperImGui.Separator();
                break;
            case false:
                CopperImGui.Separator(seperatorText);
                break;
        }
    }
}