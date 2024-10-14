using System.Reflection;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Rendering.Renderers;
using CopperTexture2D = CopperDevs.DearImGui.Renderer.Raylib.Bindings.Texture2D;
using Texture2D = ZeroElectric.Vinculum.Texture;

namespace CopperDevs.DearImGui.Renderer.Raylib.Raylib_CSharp_Vinculum.Internal.FieldRenderers;

internal class TextureFieldRenderer : FieldRenderer
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
                CopperImGui.HorizontalGroup(() =>
                    {
                        RlImGuiRenderer<RlImGuiBinding>.ImageRenderTexture(new CopperTexture2D()
                        {
                            Width = textureValue.width,
                            Height = textureValue.height,
                            bindingObject = textureValue,
                            Id = textureValue.id,
                        });
                    },
                    () => { CopperImGui.Text($"Size: <{textureValue.width},{textureValue.height}> \nFormat: {textureValue.Format} \nOpenGL id: {textureValue.id} \nMipmap level: {textureValue.mipmaps}"); });
            });
    }
}