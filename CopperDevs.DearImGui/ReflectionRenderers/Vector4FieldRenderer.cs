using CopperDevs.DearImGui.Attributes;

namespace CopperDevs.DearImGui.ReflectionRenderers;

public class Vector4FieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var rangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (rangeAttribute is not null)
        {
            var value = (Vector4)(fieldInfo.GetValue(component) ?? Vector4.Zero);

            switch (rangeAttribute.TargetRangeType)
            {
                case RangeType.Drag:
                    CopperImGui.DragValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
                        rangeAttribute.Speed, rangeAttribute.Min, rangeAttribute.Max,
                        newValue =>
                        {
                            fieldInfo.SetValue(component, newValue);
                            valueChanged?.Invoke();
                        });
                    break;
                case RangeType.Slider:
                    CopperImGui.SliderValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
                        rangeAttribute.Min, rangeAttribute.Max,
                        newValue =>
                        {
                            fieldInfo.SetValue(component, newValue);
                            valueChanged?.Invoke();
                        });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            var value = (Vector4)(fieldInfo.GetValue(component) ?? Vector4.Zero);

            CopperImGui.DragValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
                newValue => { fieldInfo.SetValue(component, newValue); });
        }
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        var vectorValue = (Vector4)value;

        CopperImGui.DragValue($"{value.GetType().Name.ToTitleCase()}##{id}", ref vectorValue, _ => valueChanged?.Invoke());

        value = vectorValue;
    }
}