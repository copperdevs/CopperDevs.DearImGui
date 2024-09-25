using System.Numerics;
using CopperDevs.Core.Data;
using CopperDevs.DearImGui.Renderer.Raylib.Bindings;
using CopperDevs.DearImGui.Renderer.Raylib.Raylib_cs.Internal.FieldRenderers;
using Raylib_cs;
using ConfigFlags = CopperDevs.DearImGui.Renderer.Raylib.Bindings.ConfigFlags;
using GamepadAxis = CopperDevs.DearImGui.Renderer.Raylib.Bindings.GamepadAxis;
using GamepadButton = CopperDevs.DearImGui.Renderer.Raylib.Bindings.GamepadButton;
using KeyboardKey = CopperDevs.DearImGui.Renderer.Raylib.Bindings.KeyboardKey;
using MouseButton = CopperDevs.DearImGui.Renderer.Raylib.Bindings.MouseButton;
using MouseCursor = CopperDevs.DearImGui.Renderer.Raylib.Bindings.MouseCursor;
using Texture2D = CopperDevs.DearImGui.Renderer.Raylib.Bindings.Texture2D;
using rlTexture2D = Raylib_cs.Texture2D;
using rlKeyboardKey = Raylib_cs.KeyboardKey;
using rlMouseButton = Raylib_cs.MouseButton;
using rlConfigFlags = Raylib_cs.ConfigFlags;
using rlMouseCursor = Raylib_cs.MouseCursor;
using rlGamepadButton = Raylib_cs.GamepadButton;
using rlGamepadAxis = Raylib_cs.GamepadAxis;
using Rl = Raylib_cs.Raylib;

namespace CopperDevs.DearImGui.Renderer.Raylib.Raylib_cs;

public class RlImGuiBinding : RlBinding
{
    public RlImGuiBinding()
    {
        CopperImGui.RegisterFieldRenderer<Color, ColorFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<rlTexture2D, Texture2DFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<RenderTexture2D, RenderTexture2DFieldRenderer>();
    }

    public override unsafe Texture2D LoadFontTexture(IntPtr data, Vector2Int size)
    {
        var image = new Image
        {
            Data = data.ToPointer(),
            Width = size.X,
            Height = size.Y,
            Mipmaps = 1,
            Format = PixelFormat.UncompressedR8G8B8A8,
        };

        var rlTexture = Rl.LoadTextureFromImage(image);

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
        Rl.UnloadTexture((rlTexture2D)texture.bindingObject);
    }

    public override bool InputIsKeyDown(KeyboardKey key)
    {
        return Rl.IsKeyDown((rlKeyboardKey)key);
    }

    public override bool WindowIsFocused()
    {
        return Rl.IsWindowFocused();
    }

    public override string WindowGetClipboardText()
    {
        return Rl.GetClipboardText_();
    }

    public override void WindowSetClipboardText(string text)
    {
        Rl.SetClipboardText(text);
    }

    public override bool InputIsMouseButtonPressed(MouseButton button)
    {
        return Rl.IsMouseButtonPressed((rlMouseButton)button);
    }

    public override bool InputIsMouseButtonReleased(MouseButton button)
    {
        return Rl.IsMouseButtonReleased((rlMouseButton)button);
    }

    public override bool WindowIsFullscreen()
    {
        return Rl.IsWindowFullscreen();
    }

    public override int WindowGetCurrentMonitor()
    {
        return Rl.GetCurrentMonitor();
    }

    public override int WindowGetMonitorWidth(int monitor)
    {
        return Rl.GetMonitorWidth(monitor);
    }

    public override int WindowGetMonitorHeight(int monitor)
    {
        return Rl.GetMonitorHeight(monitor);
    }

    public override int WindowGetScreenWidth()
    {
        return Rl.GetScreenWidth();
    }

    public override int WindowGetScreenHeight()
    {
        return Rl.GetScreenHeight();
    }

    public override bool WindowIsState(ConfigFlags flag)
    {
        return Rl.IsWindowState((rlConfigFlags)flag);
    }

    public override Vector2 WindowGetScaleDPI()
    {
        return Rl.GetWindowScaleDPI();
    }

    public override float TimeGetFrameTime()
    {
        return Rl.GetFrameTime();
    }

    public override void InputSetMousePosition(int mousePosX, int mousePosY)
    {
        Rl.SetMousePosition(mousePosX, mousePosY);
    }

    public override int InputGetMouseX()
    {
        return Rl.GetMouseX();
    }

    public override int InputGetMouseY()
    {
        return Rl.GetMouseY();
    }

    public override Vector2 InputGetMouseWheelMoveV()
    {
        return Rl.GetMouseWheelMoveV();
    }

    public override void InputHideCursor()
    {
        Rl.HideCursor();
    }

    public override void InputShowCursor()
    {
        Rl.ShowCursor();
    }

    public override void InputSetMouseCursor(MouseCursor value)
    {
        Rl.SetMouseCursor((rlMouseCursor)value);
    }

    public override KeyboardKey InputGetKeyPressed()
    {
        return (KeyboardKey)Rl.GetKeyPressed();
    }

    public override bool InputIsKeyReleased(KeyboardKey key)
    {
        return Rl.IsKeyReleased((rlKeyboardKey)key);
    }

    public override int InputGetCharPressed()
    {
        return Rl.GetCharPressed();
    }

    public override bool InputIsGamepadAvailable(int i)
    {
        return Rl.IsGamepadAvailable(i);
    }

    public override bool InputIsGamepadButtonPressed(int i, GamepadButton button)
    {
        return Rl.IsGamepadButtonPressed(i, (rlGamepadButton)button);
    }

    public override bool InputIsGamepadButtonReleased(int i, GamepadButton button)
    {
        return Rl.IsGamepadButtonReleased(i, (rlGamepadButton)button);
    }

    public override float InputGetGamepadAxisMovement(int i, GamepadAxis axis)
    {
        return Rl.GetGamepadAxisMovement(i, (rlGamepadAxis)axis);
    }

    public override void RlGlEnableScissorTest()
    {
        Rlgl.EnableScissorTest();
    }

    public override void RlGlScissor(int scaleX, int displaySizeY, int width, int scaleY)
    {
        Rlgl.Scissor(scaleX, displaySizeY, width, scaleY);
    }

    public override void RlGlColor4F(float colorX, float colorY, float colorZ, float colorW)
    {
        Rlgl.Color4f(colorX, colorY, colorZ, colorW);
    }

    public override void RlGlTexCoord2F(float uvX, float uvY)
    {
        Rlgl.TexCoord2f(uvX, uvY);
    }

    public override void RlGlVertex2F(float posX, float posY)
    {
        Rlgl.Vertex2f(posX, posY);
    }

    public override void RlGlBegin(int p0)
    {
        Rlgl.Begin(p0);
    }

    public override void RlGlSetTexture(uint textureId)
    {
        Rlgl.SetTexture(textureId);
    }

    public override bool RlGlCheckRenderBatchLimit(int i)
    {
        return Rlgl.CheckRenderBatchLimit(i);
    }

    public override void RlGlEnd()
    {
        Rlgl.End();
    }

    public override void RlGlDrawRenderBatchActive()
    {
        Rlgl.DrawRenderBatchActive();
    }

    public override void RlGlDisableBackfaceCulling()
    {
        Rlgl.DisableBackfaceCulling();
    }

    public override void RlGlDisableScissorTest()
    {
        Rlgl.DisableScissorTest();
    }

    public override void RlGlEnableBackfaceCulling()
    {
        Rlgl.EnableBackfaceCulling();
    }
}