namespace CopperDevs.DearImGui.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class SeperatorAttribute : Attribute
{
    private string? seperatorText;

    public SeperatorAttribute()
    {
        seperatorText = null;
    }

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