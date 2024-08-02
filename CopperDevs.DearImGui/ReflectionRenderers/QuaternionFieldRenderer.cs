namespace CopperDevs.DearImGui.ReflectionRenderers;

public class QuaternionFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var value = ((Quaternion)(fieldInfo.GetValue(component) ?? Quaternion.Identity)).ToVector();

        CopperImGui.DragValue($"{fieldInfo.Name.ToTitleCase()}##{id}", ref value,
            newValue =>
            {
                fieldInfo.SetValue(component, newValue.ToQuaternion());
                valueChanged?.Invoke();
            });
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        var vectorValue = ((Quaternion)value).ToVector();

        CopperImGui.DragValue($"{value.GetType().Name.ToTitleCase()}##{id}", ref vectorValue, _ => valueChanged?.Invoke());

        value = vectorValue.ToQuaternion();
    }
}