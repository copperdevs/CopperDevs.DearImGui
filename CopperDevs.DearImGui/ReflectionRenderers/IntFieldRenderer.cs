using CopperDevs.DearImGui.Attributes;

namespace CopperDevs.DearImGui.ReflectionRenderers;

public class IntFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var rangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (rangeAttribute is not null)
        {
            var value = (int)(fieldInfo.GetValue(component) ?? 0);

            switch (rangeAttribute.TargetRangeType)
            {
                case RangeType.Drag:
                    CopperImGui.DragValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
                        (int)rangeAttribute.Min, (int)rangeAttribute.Min, (int)rangeAttribute.Max,
                        newValue =>
                        {
                            fieldInfo.SetValue(component, newValue);
                            valueChanged?.Invoke();
                        });
                    break;
                case RangeType.Slider:
                    CopperImGui.SliderValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
                        (int)rangeAttribute.Min, (int)rangeAttribute.Max,
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
            var value = (int)(fieldInfo.GetValue(component) ?? 0);

            CopperImGui.DragValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
                newValue =>
                {
                    fieldInfo.SetValue(component, newValue);
                    valueChanged?.Invoke();
                });
        }
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        var intValue = (int)value;

        CopperImGui.DragValue($"{value.GetType().Name}##{id}", ref intValue, _ => valueChanged?.Invoke());

        value = intValue;
    }
}