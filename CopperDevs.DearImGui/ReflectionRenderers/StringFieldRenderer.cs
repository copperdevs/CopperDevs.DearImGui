namespace CopperDevs.DearImGui.ReflectionRenderers;

public class StringFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var value = (string)(fieldInfo.GetValue(component) ?? false);


        CopperImGui.Text($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
            newValue => { fieldInfo.SetValue(component, newValue);
                valueChanged?.Invoke(); });
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        var stringValue = (string)value;

        CopperImGui.Text($"{value.GetType().Name.ToTitleCase()}##{id}", ref stringValue, _ => valueChanged?.Invoke());

        value = stringValue;
    }
}