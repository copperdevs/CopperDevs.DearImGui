namespace CopperDevs.DearImGui.ReflectionRenderers;

public class BoolFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var value = (bool)(fieldInfo.GetValue(component) ?? false);

        CopperImGui.Checkbox($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value, interacted =>
        {
            fieldInfo.SetValue(component, interacted);
            valueChanged?.Invoke();
        });
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        var boolValue = (bool)value;

        CopperImGui.Checkbox($"{value.GetType().Name.ToTitleCase()}##{id}", ref boolValue, outValue =>
        {
            valueChanged?.Invoke();
        });

        value = boolValue;
    }
}