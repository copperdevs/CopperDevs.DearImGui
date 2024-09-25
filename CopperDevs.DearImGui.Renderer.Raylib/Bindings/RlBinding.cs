using System.Numerics;
using CopperDevs.Core.Data;

namespace CopperDevs.DearImGui.Renderer.Raylib.Bindings;

public abstract class RlBinding
{
    // wrapped 
    
    public abstract Texture2D LoadFontTexture(IntPtr data, Vector2Int size);
    public abstract void UnloadTexture(Texture2D texture);
    
    // direct calls
    
    public abstract bool InputIsKeyDown(KeyboardKey key);
    public abstract bool WindowIsFocused();
    public abstract string WindowGetClipboardText();
    public abstract void WindowSetClipboardText(string text);
    public abstract bool InputIsMouseButtonPressed(MouseButton button);
    public abstract bool InputIsMouseButtonReleased(MouseButton button);
    public abstract bool WindowIsFullscreen();
    public abstract int WindowGetCurrentMonitor();
    public abstract int WindowGetMonitorWidth(int monitor);
    public abstract int WindowGetMonitorHeight(int monitor);
    public abstract int WindowGetScreenWidth();
    public abstract int WindowGetScreenHeight();
    public abstract bool WindowIsState(ConfigFlags flag);
    public abstract Vector2 WindowGetScaleDPI();
    public abstract float TimeGetFrameTime();
    public abstract void InputSetMousePosition(int mousePosX, int mousePosY);
    public abstract int InputGetMouseX();
    public abstract int InputGetMouseY();
    public abstract Vector2 InputGetMouseWheelMoveV();
    public abstract void InputHideCursor();
    public abstract void InputShowCursor();
    public abstract void InputSetMouseCursor(MouseCursor value);
    public abstract KeyboardKey InputGetKeyPressed();
    public abstract bool InputIsKeyReleased(KeyboardKey key);
    public abstract int InputGetCharPressed();
    public abstract bool InputIsGamepadAvailable(int i);
    public abstract bool InputIsGamepadButtonPressed(int i, GamepadButton button);
    public abstract bool InputIsGamepadButtonReleased(int i, GamepadButton button);
    public abstract float InputGetGamepadAxisMovement(int i, GamepadAxis axis);
    public abstract void RlGlEnableScissorTest();
    public abstract void RlGlScissor(int scaleX, int displaySizeY, int width, int scaleY);
    public abstract void RlGlColor4F(float colorX, float colorY, float colorZ, float colorW);
    public abstract void RlGlTexCoord2F(float uvX, float uvY);
    public abstract void RlGlVertex2F(float posX, float posY);
    public abstract void RlGlBegin(int p0);
    public abstract void RlGlSetTexture(uint textureId);
    public abstract bool RlGlCheckRenderBatchLimit(int i);
    public abstract void RlGlEnd();
    public abstract void RlGlDrawRenderBatchActive();
    public abstract void RlGlDisableBackfaceCulling();
    public abstract void RlGlDisableScissorTest();
    public abstract void RlGlEnableBackfaceCulling();
}