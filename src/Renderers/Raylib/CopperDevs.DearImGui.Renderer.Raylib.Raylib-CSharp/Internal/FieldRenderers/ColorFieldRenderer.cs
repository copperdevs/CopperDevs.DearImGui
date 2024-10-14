using System.Numerics;
using System.Reflection;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Rendering.Renderers;
using Raylib_CSharp.Colors;

using CopperTexture2D = CopperDevs.DearImGui.Renderer.Raylib.Bindings.Texture2D;
using Texture2D = Raylib_CSharp.Textures.Texture2D;

namespace CopperDevs.DearImGui.Renderer.Raylib.Raylib_CSharp.Internal.FieldRenderers;

internal class ColorFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var value = (Color)(fieldInfo.GetValue(component) ?? Color.White);
        var vectorColor = new Vector4(value.R / 255f, value.G / 255f, value.B / 255f, value.A / 255f);

        CopperImGui.ColorEdit($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref vectorColor,
            interactedValue =>
            {
                fieldInfo.SetValue(component, new Color((byte)(interactedValue.X * 255), (byte)(interactedValue.Y * 255), (byte)(interactedValue.Z * 255), (byte)(interactedValue.W * 255)));
                valueChanged?.Invoke();
            });
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        var colorValue = new Vector4(((Color)value).R / 255f, ((Color)value).G / 255f, ((Color)value).B / 255f, ((Color)value).A / 255f);

        CopperImGui.ColorEdit($"{value.GetType().Name.ToTitleCase()}##{id}", ref colorValue, _ => valueChanged?.Invoke());

        value = new Color((byte)(colorValue.X * 255), (byte)(colorValue.Y * 255), (byte)(colorValue.Z * 255), (byte)(colorValue.W * 255));
    }
}