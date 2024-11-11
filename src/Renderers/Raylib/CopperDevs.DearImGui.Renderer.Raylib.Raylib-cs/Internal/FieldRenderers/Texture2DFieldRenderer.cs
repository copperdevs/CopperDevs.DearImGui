using System.Reflection;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Rendering;
using CopperTexture2D = CopperDevs.DearImGui.Renderer.Raylib.Bindings.Texture2D;
using Texture2D = Raylib_cs.Texture2D;

namespace CopperDevs.DearImGui.Renderer.Raylib.Raylib_cs.Internal.FieldRenderers;

internal class Texture2DFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var value = (Texture2D)(fieldInfo.GetValue(component) ?? new Texture2D());

        TextureRenderer($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", value);
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        TextureRenderer($"{value.GetType().Name.ToTitleCase()}##{id}", (Texture2D)value);
    }

    private static void TextureRenderer(string title, Texture2D textureValue)
    {
        CopperImGui.CollapsingHeader(title,
            () =>
            {
                RlImGuiRenderer<RlImGuiBinding>.ImageRenderTexture(new CopperTexture2D()
                {
                    Width = textureValue.Width,
                    Height = textureValue.Height,
                    bindingObject = textureValue,
                    Id = textureValue.Id,
                });
            });
    }
}