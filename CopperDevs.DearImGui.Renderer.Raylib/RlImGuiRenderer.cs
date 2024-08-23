using System.Numerics;
using ImGuiNET;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Transformations;

namespace CopperDevs.DearImGui.Renderer.Raylib;

public class RlImGuiRenderer : ImGuiRenderer
{
    public override void Setup()
    {
        rlImGui.Setup(true, true);

        CopperImGui.RegisterFieldRenderer<Color, ColorFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<Texture2D, Texture2DFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<RenderTexture2D, RenderTexture2DFieldRenderer>();
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

    /// <inheritdoc cref="CopperDevs.DearImGui.Renderer.Raylib.rlImGui.Image"/>
    public static void DisplayTexture2D(Texture2D image) => rlImGui.Image(image);

    /// <inheritdoc cref="CopperDevs.DearImGui.Renderer.Raylib.rlImGui.ImageSize(Texture2D, int, int)"/>
    public static void DisplayTexture2D(Texture2D image, int width, int height) => rlImGui.ImageSize(image, width, height);

    /// <inheritdoc cref="CopperDevs.DearImGui.Renderer.Raylib.rlImGui.ImageSize(Texture2D, Vector2)"/>
    public static void DisplayTexture2D(Texture2D image, Vector2 size) => rlImGui.ImageSize(image, size);

    /// <inheritdoc cref="CopperDevs.DearImGui.Renderer.Raylib.rlImGui.ImageRect"/>
    public static void DisplayTexture2D(Texture2D image, int destWidth, int destHeight, Rectangle sourceRect) => rlImGui.ImageRect(image, destWidth, destHeight, sourceRect);

    /// <inheritdoc cref="CopperDevs.DearImGui.Renderer.Raylib.rlImGui.ImageRenderTexture"/>
    public static void RenderRenderTexture2D(RenderTexture2D image) => rlImGui.ImageRenderTexture(image);

    /// <inheritdoc cref="CopperDevs.DearImGui.Renderer.Raylib.rlImGui.ImageRenderTextureFit"/>
    // ReSharper disable once MethodOverloadWithOptionalParameter
    public static void RenderTextureFit(RenderTexture2D image, bool center = true) => rlImGui.ImageRenderTextureFit(image, center);

    /// <inheritdoc cref="CopperDevs.DearImGui.Renderer.Raylib.rlImGui.ImageButton"/>
    public static bool RenderImageButton(string name, Texture2D image) => rlImGui.ImageButton(name, image);

    /// <inheritdoc cref="CopperDevs.DearImGui.Renderer.Raylib.rlImGui.ImageButtonSize"/>
    public static bool RenderImageButton(string name, Texture2D image, Vector2 size) => rlImGui.ImageButtonSize(name, image, size);
}