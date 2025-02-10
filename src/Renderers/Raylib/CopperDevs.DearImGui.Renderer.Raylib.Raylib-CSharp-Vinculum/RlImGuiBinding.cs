using CopperDevs.Core.Data;
using CopperDevs.DearImGui.Renderer.Raylib.Bindings;
using CopperDevs.DearImGui.Renderer.Raylib.Raylib_CSharp_Vinculum.Internal.FieldRenderers;
using ZeroElectric.Vinculum;
using ConfigFlags = CopperDevs.DearImGui.Renderer.Raylib.Bindings.ConfigFlags;
using GamepadAxis = CopperDevs.DearImGui.Renderer.Raylib.Bindings.GamepadAxis;
using GamepadButton = CopperDevs.DearImGui.Renderer.Raylib.Bindings.GamepadButton;
using KeyboardKey = CopperDevs.DearImGui.Renderer.Raylib.Bindings.KeyboardKey;
using MouseButton = CopperDevs.DearImGui.Renderer.Raylib.Bindings.MouseButton;
using MouseCursor = CopperDevs.DearImGui.Renderer.Raylib.Bindings.MouseCursor;
using rlTexture2D = ZeroElectric.Vinculum.Texture;
using rlKeyboardKey = ZeroElectric.Vinculum.KeyboardKey;
using rlMouseButton = ZeroElectric.Vinculum.MouseButton;
using rlConfigFlags = ZeroElectric.Vinculum.ConfigFlags;
using rlMouseCursor = ZeroElectric.Vinculum.MouseCursor;
using rlGamepadButton = ZeroElectric.Vinculum.GamepadButton;
using rlGamepadAxis = ZeroElectric.Vinculum.GamepadAxis;
using Rl = ZeroElectric.Vinculum.Raylib;
using Vector2 = System.Numerics.Vector2;

namespace CopperDevs.DearImGui.Renderer.Raylib.Raylib_CSharp_Vinculum;

public class RlImGuiBinding : RlBinding
{
    public RlImGuiBinding()
    {
        CopperImGui.RegisterFieldRenderer<Color, ColorFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<rlTexture2D, TextureFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<RenderTexture, RenderTextureFieldRenderer>();
    }

    public override unsafe Texture2D LoadFontTexture(IntPtr data, Vector2Int size)
    {
        var image = new Image
        {
            data = data.ToPointer(),
            width = size.X,
            height = size.Y,
            mipmaps = 1,
            format = (int)PixelFormat.PIXELFORMAT_UNCOMPRESSED_R8G8B8A8,
        };

        var rlTexture = Rl.LoadTextureFromImage(image);

        return new Texture2D
        {
            Width = rlTexture.width,
            Height = rlTexture.height,
            Id = rlTexture.id,
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
        return Rl.GetClipboardTextAsString();
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
        return Rl.IsWindowFocused();
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
        RlGl.rlEnableScissorTest();
    }

    public override void RlGlScissor(int scaleX, int displaySizeY, int width, int scaleY)
    {
        RlGl.rlScissor(scaleX, displaySizeY, width, scaleY);
    }

    public override void RlGlColor4F(float colorX, float colorY, float colorZ, float colorW)
    {
        RlGl.rlColor4f(colorX, colorY, colorZ, colorW);
    }

    public override void RlGlTexCoord2F(float uvX, float uvY)
    {
        RlGl.rlTexCoord2f(uvX, uvY);
    }

    public override void RlGlVertex2F(float posX, float posY)
    {
        RlGl.rlVertex2f(posX, posY);
    }

    public override void RlGlBegin(int p0)
    {
        RlGl.rlBegin(p0);
    }

    public override void RlGlSetTexture(uint textureId)
    {
        RlGl.rlSetTexture(textureId);
    }

    public override bool RlGlCheckRenderBatchLimit(int i)
    {
        return RlGl.rlCheckRenderBatchLimit(i);
    }

    public override void RlGlEnd()
    {
        RlGl.rlEnd();
    }

    public override void RlGlDrawRenderBatchActive()
    {
        RlGl.rlDrawRenderBatchActive();
    }

    public override void RlGlDisableBackfaceCulling()
    {
        RlGl.rlDisableBackfaceCulling();
    }

    public override void RlGlDisableScissorTest()
    {
        RlGl.rlDisableScissorTest();
    }

    public override void RlGlEnableBackfaceCulling()
    {
        RlGl.rlEnableBackfaceCulling();
    }
}