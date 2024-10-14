using System.Numerics;
using System.Reflection;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Rendering.Renderers;
using ZeroElectric.Vinculum;

namespace CopperDevs.DearImGui.Renderer.Raylib.Raylib_CSharp_Vinculum.Internal.FieldRenderers;

internal class ColorFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var value = (Color)(fieldInfo.GetValue(component) ?? ZeroElectric.Vinculum.Raylib.WHITE);
        var vectorColor = new Vector4(value.r / 255f, value.g / 255f, value.b / 255f, value.a / 255f);

        CopperImGui.ColorEdit($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref vectorColor,
            interactedValue =>
            {
                fieldInfo.SetValue(component, new Color((byte)(interactedValue.X * 255), (byte)(interactedValue.Y * 255), (byte)(interactedValue.Z * 255), (byte)(interactedValue.W * 255)));
                valueChanged?.Invoke();
            });
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        var colorValue = new Vector4(((Color)value).r / 255f, ((Color)value).g / 255f, ((Color)value).b / 255f, ((Color)value).a / 255f);

        CopperImGui.ColorEdit($"{value.GetType().Name.ToTitleCase()}##{id}", ref colorValue, _ => valueChanged?.Invoke());

        value = new Color((byte)(colorValue.X * 255), (byte)(colorValue.Y * 255), (byte)(colorValue.Z * 255), (byte)(colorValue.W * 255));
    }
}