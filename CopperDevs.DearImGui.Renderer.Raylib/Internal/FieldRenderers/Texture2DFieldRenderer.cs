using System.Reflection;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.ReflectionRenderers;
using CopperDevs.DearImGui.Renderer.Raylib.Internal.Rendering;
using Raylib_CSharp.Textures;

namespace CopperDevs.DearImGui.Renderer.Raylib.Internal.FieldRenderers;

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
                CopperImGui.HorizontalGroup(() => { rlImGui.ImageSize(textureValue, 64, 64); },
                    () => { CopperImGui.Text($"Size: <{textureValue.Width},{textureValue.Height}> \nFormat: {textureValue.Format} \nOpenGL id: {textureValue.Id} \nMipmap level: {textureValue.Mipmaps}"); });
            });
    }
}