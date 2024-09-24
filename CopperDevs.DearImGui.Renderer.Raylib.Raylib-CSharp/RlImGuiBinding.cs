using System.Numerics;
using CopperDevs.Core.Data;
using CopperDevs.DearImGui.Renderer.Raylib.Bindings;
using CopperDevs.DearImGui.Renderer.Raylib.Raylib_CSharp.Internal.FieldRenderers;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Images;
using Raylib_CSharp.Textures;
using rlTexture2D = Raylib_CSharp.Textures.Texture2D;
using Texture2D = CopperDevs.DearImGui.Renderer.Raylib.Bindings.Texture2D;

namespace CopperDevs.DearImGui.Renderer.Raylib.Raylib_CSharp;

public class RlImGuiBinding : RlBinding
{
    public RlImGuiBinding()
    {
        CopperImGui.RegisterFieldRenderer<Color, ColorFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<rlTexture2D, Texture2DFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<RenderTexture2D, RenderTexture2DFieldRenderer>();
    }
    
    public override Texture2D LoadFontTexture(IntPtr data, Vector2Int size)
    {
        var image = new Image
        {
            Data = data,
            Width = size.X,
            Height = size.Y,
            Mipmaps = 1,
            Format = PixelFormat.UncompressedR8G8B8A8,
        };
        
        var rlTexture = rlTexture2D.LoadFromImage(image);

        return new Texture2D()
        {
            Width = rlTexture.Width,
            Height = rlTexture.Height,
            Id = rlTexture.Id,
            bindingObject = rlTexture
        };
    }

    public override void UnloadTexture(Texture2D texture)
    {
        ((rlTexture2D)texture.bindingObject).Unload();
    }


    public override bool InputIsKeyDown(KeyboardKey key)
    {
        throw new NotImplementedException();
    }

    public override bool WindowIsFocused()
    {
        throw new NotImplementedException();
    }

    public override string WindowGetClipboardText()
    {
        throw new NotImplementedException();
    }

    public override void WindowSetClipboardText(string text)
    {
        throw new NotImplementedException();
    }

    public override bool InputIsMouseButtonPressed(MouseButton button)
    {
        throw new NotImplementedException();
    }

    public override bool InputIsMouseButtonReleased(MouseButton button)
    {
        throw new NotImplementedException();
    }

    public override bool WindowIsFullscreen()
    {
        throw new NotImplementedException();
    }

    public override int WindowGetCurrentMonitor()
    {
        throw new NotImplementedException();
    }

    public override int WindowGetMonitorWidth(int monitor)
    {
        throw new NotImplementedException();
    }

    public override int WindowGetMonitorHeight(int monitor)
    {
        throw new NotImplementedException();
    }

    public override int WindowGetScreenWidth()
    {
        throw new NotImplementedException();
    }

    public override int WindowGetScreenHeight()
    {
        throw new NotImplementedException();
    }

    public override bool WindowIsState(ConfigFlags flag)
    {
        throw new NotImplementedException();
    }

    public override Vector2 WindowGetScaleDPI()
    {
        throw new NotImplementedException();
    }

    public override float TimeGetFrameTime()
    {
        throw new NotImplementedException();
    }

    public override void InputSetMousePosition(int mousePosX, int mousePosY)
    {
        throw new NotImplementedException();
    }

    public override int InputGetMouseX()
    {
        throw new NotImplementedException();
    }

    public override int InputGetMouseY()
    {
        throw new NotImplementedException();
    }

    public override Vector2 InputGetMouseWheelMoveV()
    {
        throw new NotImplementedException();
    }

    public override void InputHideCursor()
    {
        throw new NotImplementedException();
    }

    public override void InputShowCursor()
    {
        throw new NotImplementedException();
    }

    public override void InputSetMouseCursor(MouseCursor value)
    {
        throw new NotImplementedException();
    }

    public override KeyboardKey InputGetKeyPressed()
    {
        throw new NotImplementedException();
    }

    public override bool InputIsKeyReleased(KeyboardKey keyItrKey)
    {
        throw new NotImplementedException();
    }

    public override int InputGetCharPressed()
    {
        throw new NotImplementedException();
    }

    public override bool InputIsGamepadAvailable(int i)
    {
        throw new NotImplementedException();
    }

    public override bool InputIsGamepadButtonPressed(int i, GamepadButton button)
    {
        throw new NotImplementedException();
    }

    public override bool InputIsGamepadButtonReleased(int i, GamepadButton button)
    {
        throw new NotImplementedException();
    }

    public override float InputGetGamepadAxisMovement(int i, GamepadAxis axis)
    {
        throw new NotImplementedException();
    }

    public override void RlGlEnableScissorTest()
    {
        throw new NotImplementedException();
    }

    public override void RlGlScissor(int scaleX, int displaySizeY, int width, int scaleY)
    {
        throw new NotImplementedException();
    }

    public override void RlGlColor4F(float colorX, float colorY, float colorZ, float colorW)
    {
        throw new NotImplementedException();
    }

    public override void RlGlTexCoord2F(float uvX, float uvY)
    {
        throw new NotImplementedException();
    }

    public override void RlGlVertex2F(float posX, float posY)
    {
        throw new NotImplementedException();
    }

    public override void RlGlBegin(int p0)
    {
        throw new NotImplementedException();
    }

    public override void RlGlSetTexture(uint textureId)
    {
        throw new NotImplementedException();
    }

    public override bool RlGlCheckRenderBatchLimit(int i)
    {
        throw new NotImplementedException();
    }

    public override void RlGlEnd()
    {
        throw new NotImplementedException();
    }

    public override void RlGlDrawRenderBatchActive()
    {
        throw new NotImplementedException();
    }

    public override void RlGlDisableBackfaceCulling()
    {
        throw new NotImplementedException();
    }

    public override void RlGlDisableScissorTest()
    {
        throw new NotImplementedException();
    }

    public override void RlGlEnableBackfaceCulling()
    {
        throw new NotImplementedException();
    }
}