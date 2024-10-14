using System.Reflection;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Rendering.Renderers;

using CopperTexture2D = CopperDevs.DearImGui.Renderer.Raylib.Bindings.Texture2D;
using RenderTexture2D = ZeroElectric.Vinculum.RenderTexture;

namespace CopperDevs.DearImGui.Renderer.Raylib.Raylib_CSharp_Vinculum.Internal.FieldRenderers;

internal class RenderTextureFieldRenderer : FieldRenderer
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
                CopperImGui.Text(textureValue.texture.id, "Render texture id");
                CopperImGui.Text(textureValue.texture.Format, "Format");
                CopperImGui.Text($"{textureValue.texture.width},{textureValue.texture.height}", "Size");
                CopperImGui.Text(textureValue.texture.id, "OpenGL id");
                CopperImGui.Text(textureValue.texture.mipmaps, "Mipmap level");
            });

            CopperImGui.CollapsingHeader("Texture", () => { 
                RlImGuiRenderer<RlImGuiBinding>.ImageRenderTexture(new CopperTexture2D()
                {
                    Width = textureValue.texture.width,
                    Height = textureValue.texture.height,
                    bindingObject = textureValue,
                    Id = textureValue.texture.id
                }); });
        });
    }
}