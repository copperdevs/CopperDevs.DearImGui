namespace CopperDevs.DearImGui.ReflectionRenderers;

public abstract class FieldRenderer
{
    public abstract void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!);
    public abstract void ValueRenderer(ref object value, int id, Action valueChanged = null!);
}