using System.Numerics;
using CopperDevs.DearImGui.Renderer.Raylib.Bindings;
using CopperDevs.DearImGui.Renderer.Raylib.Internal;
using CopperDevs.DearImGui.Rendering;

namespace CopperDevs.DearImGui.Renderer.Raylib;

public class RlImGuiRenderer<TBinding> : ImGuiRenderer where TBinding : RlBinding, new()
{
    public override void Setup()
    {
        rlImGui.Setup(new TBinding());
    }

    public override void Begin()
    {
        rlImGui.Begin();
    }

    public override void End()
    {
        rlImGui.End();
    }

    public override void Shutdown()
    {
        rlImGui.Shutdown();
    }

    /// <inheritdoc cref="rlImGui.Image"/>
    public static void Image(Texture2D image) => rlImGui.Image(image);

    /// <inheritdoc cref="rlImGui.ImageSize(Texture2D, int, int)"/>
    public static void ImageSize(Texture2D image, int width, int height) => rlImGui.ImageSize(image, width, height);

    /// <inheritdoc cref="rlImGui.ImageSize(Texture2D, Vector2)"/>
    public static void ImageSize(Texture2D image, Vector2 size) => rlImGui.ImageSize(image, size);

    /// <inheritdoc cref="rlImGui.ImageRect"/>
    public static void ImageRect(Texture2D image, int destWidth, int destHeight, Rectangle sourceRect) => rlImGui.ImageRect(image, destWidth, destHeight, sourceRect);

    /// <inheritdoc cref="rlImGui.ImageRenderTexture"/>
    public static void ImageRenderTexture(Texture2D image) => rlImGui.ImageRenderTexture(image);

    /// <inheritdoc cref="rlImGui.ImageRenderTextureFit"/>
    // ReSharper disable once MethodOverloadWithOptionalParameter
    public static void ImageRenderTextureFit(Texture2D image, bool center = true) => rlImGui.ImageRenderTextureFit(image, center);

    /// <inheritdoc cref="rlImGui.ImageButton"/>
    public static bool ImageButton(string name, Texture2D image) => rlImGui.ImageButton(name, image);

    /// <inheritdoc cref="rlImGui.ImageButtonSize"/>
    public static bool ImageButtonSize(string name, Texture2D image, Vector2 size) => rlImGui.ImageButtonSize(name, image, size);
}