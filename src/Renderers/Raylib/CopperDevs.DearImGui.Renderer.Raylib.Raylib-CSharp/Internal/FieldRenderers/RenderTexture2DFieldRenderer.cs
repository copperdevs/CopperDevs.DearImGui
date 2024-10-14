using System.Reflection;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Rendering.Renderers;
using Raylib_CSharp.Textures;
using CopperTexture2D = CopperDevs.DearImGui.Renderer.Raylib.Bindings.Texture2D;
using Texture2D = Raylib_CSharp.Textures.Texture2D;

namespace CopperDevs.DearImGui.Renderer.Raylib.Raylib_CSharp.Internal.FieldRenderers;

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

            CopperImGui.CollapsingHeader("Texture", () => { 
                RlImGuiRenderer<RlImGuiBinding>.ImageRenderTexture(new CopperTexture2D()
                {
                    Width = textureValue.Texture.Width,
                    Height = textureValue.Texture.Height,
                    bindingObject = textureValue.Texture,
                    Id = textureValue.Id,
                }); });
        });
    }
}