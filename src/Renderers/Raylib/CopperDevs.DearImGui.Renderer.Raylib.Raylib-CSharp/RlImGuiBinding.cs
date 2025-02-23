using CopperDevs.Core.Data;
using CopperDevs.DearImGui.Renderer.Raylib.Bindings;
using CopperDevs.DearImGui.Renderer.Raylib.Raylib_CSharp.Internal.FieldRenderers;
using Raylib_CSharp;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Images;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering.Gl;
using Raylib_CSharp.Textures;
using ConfigFlags = CopperDevs.DearImGui.Renderer.Raylib.Bindings.ConfigFlags;
using GamepadAxis = CopperDevs.DearImGui.Renderer.Raylib.Bindings.GamepadAxis;
using GamepadButton = CopperDevs.DearImGui.Renderer.Raylib.Bindings.GamepadButton;
using KeyboardKey = CopperDevs.DearImGui.Renderer.Raylib.Bindings.KeyboardKey;
using MouseButton = CopperDevs.DearImGui.Renderer.Raylib.Bindings.MouseButton;
using MouseCursor = CopperDevs.DearImGui.Renderer.Raylib.Bindings.MouseCursor;
using Texture2D = CopperDevs.DearImGui.Renderer.Raylib.Bindings.Texture2D;
using rlTexture2D = Raylib_CSharp.Textures.Texture2D;
using rlKeyboardKey = Raylib_CSharp.Interact.KeyboardKey;
using rlMouseButton = Raylib_CSharp.Interact.MouseButton;
using rlConfigFlags = Raylib_CSharp.Windowing.ConfigFlags;
using rlMouseCursor = Raylib_CSharp.Interact.MouseCursor;
using rlGamepadButton = Raylib_CSharp.Interact.GamepadButton;
using rlGamepadAxis = Raylib_CSharp.Interact.GamepadAxis;
using GameWindow = Raylib_CSharp.Windowing.Window;
using Vector2 = System.Numerics.Vector2;

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
        return Input.IsKeyDown((rlKeyboardKey)key);
    }

    public override bool WindowIsFocused()
    {
        return GameWindow.IsFocused();
    }

    public override string WindowGetClipboardText()
    {
        return GameWindow.GetClipboardText();
    }

    public override void WindowSetClipboardText(string text)
    {
        GameWindow.SetClipboardText(text);
    }

    public override bool InputIsMouseButtonPressed(MouseButton button)
    {
        return Input.IsMouseButtonPressed((rlMouseButton)button);
    }

    public override bool InputIsMouseButtonReleased(MouseButton button)
    {
        return Input.IsMouseButtonReleased((rlMouseButton)button);
    }

    public override bool WindowIsFullscreen()
    {
        return GameWindow.IsFullscreen();
    }

    public override int WindowGetCurrentMonitor()
    {
        return GameWindow.GetCurrentMonitor();
    }

    public override int WindowGetMonitorWidth(int monitor)
    {
        return GameWindow.GetMonitorWidth(monitor);
    }

    public override int WindowGetMonitorHeight(int monitor)
    {
        return GameWindow.GetMonitorHeight(monitor);
    }

    public override int WindowGetScreenWidth()
    {
        return GameWindow.GetScreenWidth();
    }

    public override int WindowGetScreenHeight()
    {
        return GameWindow.GetScreenHeight();
    }

    public override bool WindowIsState(ConfigFlags flag)
    {
        return GameWindow.IsState((rlConfigFlags)flag);
    }

    public override Vector2 WindowGetScaleDPI()
    {
        return GameWindow.GetScaleDPI();
    }

    public override float TimeGetFrameTime()
    {
        return Time.GetFrameTime();
    }

    public override void InputSetMousePosition(int mousePosX, int mousePosY)
    {
        Input.SetMousePosition(mousePosX, mousePosY);
    }

    public override int InputGetMouseX()
    {
        return Input.GetMouseX();
    }

    public override int InputGetMouseY()
    {
        return Input.GetMouseY();
    }

    public override Vector2 InputGetMouseWheelMoveV()
    {
        return Input.GetMouseWheelMoveV();
    }

    public override void InputHideCursor()
    {
        Input.HideCursor();
    }

    public override void InputShowCursor()
    {
        Input.ShowCursor();
    }

    public override void InputSetMouseCursor(MouseCursor value)
    {
        Input.SetMouseCursor((rlMouseCursor)value);
    }

    public override KeyboardKey InputGetKeyPressed()
    {
        return (KeyboardKey)Input.GetKeyPressed();
    }

    public override bool InputIsKeyReleased(KeyboardKey key)
    {
        return Input.IsKeyReleased((rlKeyboardKey)key);
    }

    public override int InputGetCharPressed()
    {
        return Input.GetCharPressed();
    }

    public override bool InputIsGamepadAvailable(int i)
    {
        return Input.IsGamepadAvailable(i);
    }

    public override bool InputIsGamepadButtonPressed(int i, GamepadButton button)
    {
        return Input.IsGamepadButtonReleased(i, (rlGamepadButton)button);
    }

    public override bool InputIsGamepadButtonReleased(int i, GamepadButton button)
    {
        return Input.IsGamepadButtonReleased(i, (rlGamepadButton)button);
    }

    public override float InputGetGamepadAxisMovement(int i, GamepadAxis axis)
    {
        return Input.GetGamepadAxisMovement(i, (rlGamepadAxis)axis);
    }

    public override void RlGlEnableScissorTest()
    {
        RlGl.EnableScissorTest();
    }

    public override void RlGlScissor(int scaleX, int displaySizeY, int width, int scaleY)
    {
        RlGl.Scissor(scaleX, displaySizeY, width, scaleY);
    }

    public override void RlGlColor4F(float colorX, float colorY, float colorZ, float colorW)
    {
        RlGl.Color4F(colorX, colorY, colorZ, colorW);
    }

    public override void RlGlTexCoord2F(float uvX, float uvY)
    {
        RlGl.TexCoord2F(uvX, uvY);
    }

    public override void RlGlVertex2F(float posX, float posY)
    {
        RlGl.Vertex2F(posX, posY);
    }

    public override void RlGlBegin(int p0)
    {
        RlGl.Begin((DrawMode)p0);
    }

    public override void RlGlSetTexture(uint textureId)
    {
        RlGl.SetTexture(textureId);
    }

    public override bool RlGlCheckRenderBatchLimit(int i)
    {
        return RlGl.CheckRenderBatchLimit(i);
    }

    public override void RlGlEnd()
    {
        RlGl.End();
    }

    public override void RlGlDrawRenderBatchActive()
    {
        RlGl.DrawRenderBatchActive();
    }

    public override void RlGlDisableBackfaceCulling()
    {
        RlGl.DisableBackfaceCulling();
    }

    public override void RlGlDisableScissorTest()
    {
        RlGl.DisableScissorTest();
    }

    public override void RlGlEnableBackfaceCulling()
    {
        RlGl.EnableBackfaceCulling();
    }
}