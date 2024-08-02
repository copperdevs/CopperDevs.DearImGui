using CopperDevs.DearImGui.Attributes;

namespace CopperDevs.DearImGui.ReflectionRenderers;

public class FloatFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var rangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (rangeAttribute is not null)
        {
            var value = (float)(fieldInfo.GetValue(component) ?? 0);

            switch (rangeAttribute.TargetRangeType)
            {
                case RangeType.Drag:
                    CopperImGui.DragValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value, rangeAttribute.Speed, rangeAttribute.Min, rangeAttribute.Max,
                        interactedValue =>
                        {
                            fieldInfo.SetValue(component, interactedValue);
                            valueChanged?.Invoke();
                        });
                    break;
                case RangeType.Slider:
                    CopperImGui.SliderValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value, rangeAttribute.Min, rangeAttribute.Max,
                        interactedValue =>
                        {
                            fieldInfo.SetValue(component, interactedValue);
                            valueChanged?.Invoke();
                        });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            var value = (float)(fieldInfo.GetValue(component) ?? 0);

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
        var floatValue = (float)value;

        CopperImGui.DragValue($"{value.GetType().Name}{id}##{id}", ref floatValue, _ => valueChanged?.Invoke());

        value = floatValue;
    }
}