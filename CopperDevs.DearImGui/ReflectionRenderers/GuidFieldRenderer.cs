namespace CopperDevs.DearImGui.ReflectionRenderers;

public class GuidFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var value = (Guid)(fieldInfo.GetValue(component) ?? new Guid());

        CopperImGui.Text(value, $"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}");
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        CopperImGui.Text(value, $"{value.GetType().Name.ToTitleCase()}##{id}");
    }
}