namespace CopperDevs.DearImGui.ReflectionRenderers;

/// <summary>
/// Base class for creating a new renderer for an item to be rendered with reflection
/// </summary>
public abstract class FieldRenderer
{
    /// <summary>
    /// Abstract method for when you need to renderer a field gotten through reflection
    /// </summary>
    /// <param name="fieldInfo">Target field method gotten through reflection</param>
    /// <param name="component">The object the field is on</param>
    /// <param name="id">The id of the object (so when multiple fields of the same type are rendered they aren't all sharing one id)</param>
    /// <param name="valueChanged">Callback to invoke when the value is changed</param>
    public abstract void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!);
    
    /// <summary>
    /// Abstract method for when you need to renderer an object
    /// </summary>
    /// <param name="value">The target value as an object</param>
    /// <param name="id">The id of the object (so when multiple fields of the same type are rendered they aren't all sharing one id)</param>
    /// <param name="valueChanged">Callback to invoke when the value is changed</param>
    public abstract void ValueRenderer(ref object value, int id, Action valueChanged = null!);
}