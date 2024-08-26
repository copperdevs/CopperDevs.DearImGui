using System.Reflection;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Renderer.Raylib.Internal.Rendering;
using CopperDevs.DearImGui.Rendering.Renderers;
using Raylib_CSharp.Textures;

namespace CopperDevs.DearImGui.Renderer.Raylib.Internal.FieldRenderers;

internal class RenderTexture2DFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var value = (RenderTexture2D)(fieldInfo.GetValue(component) ?? new RenderTexture2D());

        TextureRenderer($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", value);
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        TextureRenderer($"{value.GetType().Name.ToTitleCase()}##{id}", (RenderTexture2D)value);
    }

    private static void TextureRenderer(string title, RenderTexture2D textureValue)
    {
        CopperImGui.CollapsingHeader(title, () =>
        {
            CopperImGui.CollapsingHeader("Texture Info", () =>
            {
                CopperImGui.Text(textureValue.Id, "Render texture id");
                CopperImGui.Text(textureValue.Texture.Format, "Format");
                CopperImGui.Text($"{textureValue.Texture.Width},{textureValue.Texture.Height}", "Size");
                CopperImGui.Text(textureValue.Texture.Id, "OpenGL id");
                CopperImGui.Text(textureValue.Texture.Mipmaps, "Mipmap level");
            });

            CopperImGui.CollapsingHeader("Texture", () => { rlImGui.ImageRenderTextureFit(textureValue); });
        });
    }
}