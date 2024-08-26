namespace CopperDevs.DearImGui.Rendering.Renderers;

internal class EnumFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var enumValue = fieldInfo.GetValue(component)!;

        RenderEnum(fieldInfo.FieldType, ref enumValue, id, fieldInfo.Name.ToTitleCase(), valueChanged);

        fieldInfo.SetValue(component, enumValue);
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        RenderEnum(value.GetType(), ref value, id, value.GetType().Name.ToTitleCase(), valueChanged);
    }

    private static void RenderEnum(Type type, ref object component, int id, string title, Action valueChanged = null!)
    {
        var enumValues = Enum.GetValues(type).Cast<object>().ToList();
        var currentValue = enumValues[(int)Convert.ChangeType(component, Enum.GetUnderlyingType(type))]!;

        var tempComponent = component;

        CopperImGui.HorizontalGroup(
            () => { CopperImGui.Text(title); },
            () =>
            {
                CopperImGui.Button($"{currentValue}###{title}{id}", () =>
                {
                    var targetIndex = (enumValues.IndexOf(currentValue) + 1) % enumValues.Count;
                    tempComponent = enumValues[targetIndex];
                    valueChanged?.Invoke();
                });
            });

        component = tempComponent;
    }
}