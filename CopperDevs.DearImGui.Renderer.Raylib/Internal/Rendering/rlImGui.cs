using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using CopperDevs.Core;
using ImGuiNET;
using Raylib_CSharp;
using Raylib_CSharp.Images;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering.Gl;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Transformations;
using Raylib_CSharp.Windowing;

namespace CopperDevs.DearImGui.Renderer.Raylib.Internal.Rendering;

// ReSharper disable once InconsistentNaming
internal static class rlImGui
{
    private static IntPtr imGuiContext = IntPtr.Zero;

    private static ImGuiMouseCursor currentMouseCursor = ImGuiMouseCursor.COUNT;
    private static Dictionary<ImGuiMouseCursor, MouseCursor> mouseCursorMap = [];
    private static Texture2D fontTexture;

    private static readonly Dictionary<KeyboardKey, ImGuiKey> RaylibKeyMap = [];

    private static bool lastFrameFocused;

    private static bool lastControlPressed;
    private static bool lastShiftPressed;
    private static bool lastAltPressed;
    private static bool lastSuperPressed;

    // ReSharper disable once InconsistentNaming
    private static bool rlImGuiIsControlDown() => Input.IsKeyDown(KeyboardKey.RightControl) || Input.IsKeyDown(KeyboardKey.LeftControl);

    // ReSharper disable once InconsistentNaming
    private static bool rlImGuiIsShiftDown() => Input.IsKeyDown(KeyboardKey.RightShift) || Input.IsKeyDown(KeyboardKey.LeftShift);

    // ReSharper disable once InconsistentNaming
    private static bool rlImGuiIsAltDown() => Input.IsKeyDown(KeyboardKey.RightAlt) || Input.IsKeyDown(KeyboardKey.LeftAlt);

    // ReSharper disable once InconsistentNaming
    private static bool rlImGuiIsSuperDown() => Input.IsKeyDown(KeyboardKey.RightSuper) || Input.IsKeyDown(KeyboardKey.LeftSuper);

    /// <summary>
    /// Sets up ImGui, loads fonts and themes
    /// </summary>
    public static void Setup()
    {
        BeginInitImGui();

        EndInitImGui();
    }

    /// <summary>
    /// Custom initialization. Not needed if you call Setup. Only needed if you want to add custom setup code.
    /// must be followed by EndInitImGui
    /// </summary>
    public static void BeginInitImGui()
    {
        mouseCursorMap = new Dictionary<ImGuiMouseCursor, MouseCursor>();

        lastFrameFocused = Window.IsFocused();
        lastControlPressed = false;
        lastShiftPressed = false;
        lastAltPressed = false;
        lastSuperPressed = false;

        fontTexture.Id = 0;

        SetupKeymap();

        imGuiContext = ImGui.CreateContext();
    }

    internal static void SetupKeymap()
    {
        if (RaylibKeyMap.Count > 0)
            return;

        // build up a map of raylib keys to ImGuiKeys
        RaylibKeyMap[KeyboardKey.Apostrophe] = ImGuiKey.Apostrophe;
        RaylibKeyMap[KeyboardKey.Comma] = ImGuiKey.Comma;
        RaylibKeyMap[KeyboardKey.Minus] = ImGuiKey.Minus;
        RaylibKeyMap[KeyboardKey.Period] = ImGuiKey.Period;
        RaylibKeyMap[KeyboardKey.Slash] = ImGuiKey.Slash;
        RaylibKeyMap[KeyboardKey.Zero] = ImGuiKey._0;
        RaylibKeyMap[KeyboardKey.One] = ImGuiKey._1;
        RaylibKeyMap[KeyboardKey.Two] = ImGuiKey._2;
        RaylibKeyMap[KeyboardKey.Three] = ImGuiKey._3;
        RaylibKeyMap[KeyboardKey.Four] = ImGuiKey._4;
        RaylibKeyMap[KeyboardKey.Five] = ImGuiKey._5;
        RaylibKeyMap[KeyboardKey.Six] = ImGuiKey._6;
        RaylibKeyMap[KeyboardKey.Seven] = ImGuiKey._7;
        RaylibKeyMap[KeyboardKey.Eight] = ImGuiKey._8;
        RaylibKeyMap[KeyboardKey.Nine] = ImGuiKey._9;
        RaylibKeyMap[KeyboardKey.Semicolon] = ImGuiKey.Semicolon;
        RaylibKeyMap[KeyboardKey.Equal] = ImGuiKey.Equal;
        RaylibKeyMap[KeyboardKey.A] = ImGuiKey.A;
        RaylibKeyMap[KeyboardKey.B] = ImGuiKey.B;
        RaylibKeyMap[KeyboardKey.C] = ImGuiKey.C;
        RaylibKeyMap[KeyboardKey.D] = ImGuiKey.D;
        RaylibKeyMap[KeyboardKey.E] = ImGuiKey.E;
        RaylibKeyMap[KeyboardKey.F] = ImGuiKey.F;
        RaylibKeyMap[KeyboardKey.G] = ImGuiKey.G;
        RaylibKeyMap[KeyboardKey.H] = ImGuiKey.H;
        RaylibKeyMap[KeyboardKey.I] = ImGuiKey.I;
        RaylibKeyMap[KeyboardKey.J] = ImGuiKey.J;
        RaylibKeyMap[KeyboardKey.K] = ImGuiKey.K;
        RaylibKeyMap[KeyboardKey.L] = ImGuiKey.L;
        RaylibKeyMap[KeyboardKey.M] = ImGuiKey.M;
        RaylibKeyMap[KeyboardKey.N] = ImGuiKey.N;
        RaylibKeyMap[KeyboardKey.O] = ImGuiKey.O;
        RaylibKeyMap[KeyboardKey.P] = ImGuiKey.P;
        RaylibKeyMap[KeyboardKey.Q] = ImGuiKey.Q;
        RaylibKeyMap[KeyboardKey.R] = ImGuiKey.R;
        RaylibKeyMap[KeyboardKey.S] = ImGuiKey.S;
        RaylibKeyMap[KeyboardKey.T] = ImGuiKey.T;
        RaylibKeyMap[KeyboardKey.U] = ImGuiKey.U;
        RaylibKeyMap[KeyboardKey.V] = ImGuiKey.V;
        RaylibKeyMap[KeyboardKey.W] = ImGuiKey.W;
        RaylibKeyMap[KeyboardKey.X] = ImGuiKey.X;
        RaylibKeyMap[KeyboardKey.Y] = ImGuiKey.Y;
        RaylibKeyMap[KeyboardKey.Z] = ImGuiKey.Z;
        RaylibKeyMap[KeyboardKey.Space] = ImGuiKey.Space;
        RaylibKeyMap[KeyboardKey.Escape] = ImGuiKey.Escape;
        RaylibKeyMap[KeyboardKey.Enter] = ImGuiKey.Enter;
        RaylibKeyMap[KeyboardKey.Tab] = ImGuiKey.Tab;
        RaylibKeyMap[KeyboardKey.Backspace] = ImGuiKey.Backspace;
        RaylibKeyMap[KeyboardKey.Insert] = ImGuiKey.Insert;
        RaylibKeyMap[KeyboardKey.Delete] = ImGuiKey.Delete;
        RaylibKeyMap[KeyboardKey.Right] = ImGuiKey.RightArrow;
        RaylibKeyMap[KeyboardKey.Left] = ImGuiKey.LeftArrow;
        RaylibKeyMap[KeyboardKey.Down] = ImGuiKey.DownArrow;
        RaylibKeyMap[KeyboardKey.Up] = ImGuiKey.UpArrow;
        RaylibKeyMap[KeyboardKey.PageUp] = ImGuiKey.PageUp;
        RaylibKeyMap[KeyboardKey.PageDown] = ImGuiKey.PageDown;
        RaylibKeyMap[KeyboardKey.Home] = ImGuiKey.Home;
        RaylibKeyMap[KeyboardKey.End] = ImGuiKey.End;
        RaylibKeyMap[KeyboardKey.CapsLock] = ImGuiKey.CapsLock;
        RaylibKeyMap[KeyboardKey.ScrollLock] = ImGuiKey.ScrollLock;
        RaylibKeyMap[KeyboardKey.NumLock] = ImGuiKey.NumLock;
        RaylibKeyMap[KeyboardKey.PrintScreen] = ImGuiKey.PrintScreen;
        RaylibKeyMap[KeyboardKey.Pause] = ImGuiKey.Pause;
        RaylibKeyMap[KeyboardKey.F1] = ImGuiKey.F1;
        RaylibKeyMap[KeyboardKey.F2] = ImGuiKey.F2;
        RaylibKeyMap[KeyboardKey.F3] = ImGuiKey.F3;
        RaylibKeyMap[KeyboardKey.F4] = ImGuiKey.F4;
        RaylibKeyMap[KeyboardKey.F5] = ImGuiKey.F5;
        RaylibKeyMap[KeyboardKey.F6] = ImGuiKey.F6;
        RaylibKeyMap[KeyboardKey.F7] = ImGuiKey.F7;
        RaylibKeyMap[KeyboardKey.F8] = ImGuiKey.F8;
        RaylibKeyMap[KeyboardKey.F9] = ImGuiKey.F9;
        RaylibKeyMap[KeyboardKey.F10] = ImGuiKey.F10;
        RaylibKeyMap[KeyboardKey.F11] = ImGuiKey.F11;
        RaylibKeyMap[KeyboardKey.F12] = ImGuiKey.F12;
        RaylibKeyMap[KeyboardKey.LeftShift] = ImGuiKey.LeftShift;
        RaylibKeyMap[KeyboardKey.LeftControl] = ImGuiKey.LeftCtrl;
        RaylibKeyMap[KeyboardKey.LeftAlt] = ImGuiKey.LeftAlt;
        RaylibKeyMap[KeyboardKey.LeftSuper] = ImGuiKey.LeftSuper;
        RaylibKeyMap[KeyboardKey.RightShift] = ImGuiKey.RightShift;
        RaylibKeyMap[KeyboardKey.RightControl] = ImGuiKey.RightCtrl;
        RaylibKeyMap[KeyboardKey.RightAlt] = ImGuiKey.RightAlt;
        RaylibKeyMap[KeyboardKey.RightSuper] = ImGuiKey.RightSuper;
        RaylibKeyMap[KeyboardKey.KeyboardMenu] = ImGuiKey.Menu;
        RaylibKeyMap[KeyboardKey.LeftBracket] = ImGuiKey.LeftBracket;
        RaylibKeyMap[KeyboardKey.Backslash] = ImGuiKey.Backslash;
        RaylibKeyMap[KeyboardKey.RightBracket] = ImGuiKey.RightBracket;
        RaylibKeyMap[KeyboardKey.Grave] = ImGuiKey.GraveAccent;
        RaylibKeyMap[KeyboardKey.Kp0] = ImGuiKey.Keypad0;
        RaylibKeyMap[KeyboardKey.Kp1] = ImGuiKey.Keypad1;
        RaylibKeyMap[KeyboardKey.Kp2] = ImGuiKey.Keypad2;
        RaylibKeyMap[KeyboardKey.Kp3] = ImGuiKey.Keypad3;
        RaylibKeyMap[KeyboardKey.Kp4] = ImGuiKey.Keypad4;
        RaylibKeyMap[KeyboardKey.Kp5] = ImGuiKey.Keypad5;
        RaylibKeyMap[KeyboardKey.Kp6] = ImGuiKey.Keypad6;
        RaylibKeyMap[KeyboardKey.Kp7] = ImGuiKey.Keypad7;
        RaylibKeyMap[KeyboardKey.Kp8] = ImGuiKey.Keypad8;
        RaylibKeyMap[KeyboardKey.Kp9] = ImGuiKey.Keypad9;
        RaylibKeyMap[KeyboardKey.KpDecimal] = ImGuiKey.KeypadDecimal;
        RaylibKeyMap[KeyboardKey.KpDivide] = ImGuiKey.KeypadDivide;
        RaylibKeyMap[KeyboardKey.KpMultiply] = ImGuiKey.KeypadMultiply;
        RaylibKeyMap[KeyboardKey.KpSubtract] = ImGuiKey.KeypadSubtract;
        RaylibKeyMap[KeyboardKey.KpAdd] = ImGuiKey.KeypadAdd;
        RaylibKeyMap[KeyboardKey.KpEnter] = ImGuiKey.KeypadEnter;
        RaylibKeyMap[KeyboardKey.KpEqual] = ImGuiKey.KeypadEqual;
    }

    private static void SetupMouseCursors()
    {
        mouseCursorMap.Clear();
        mouseCursorMap[ImGuiMouseCursor.Arrow] = MouseCursor.Arrow;
        mouseCursorMap[ImGuiMouseCursor.TextInput] = MouseCursor.IBeam;
        mouseCursorMap[ImGuiMouseCursor.Hand] = MouseCursor.PointingHand;
        mouseCursorMap[ImGuiMouseCursor.ResizeAll] = MouseCursor.ResizeAll;
        mouseCursorMap[ImGuiMouseCursor.ResizeEW] = MouseCursor.ResizeEw;
        mouseCursorMap[ImGuiMouseCursor.ResizeNESW] = MouseCursor.ResizeNesw;
        mouseCursorMap[ImGuiMouseCursor.ResizeNS] = MouseCursor.ResizeNs;
        mouseCursorMap[ImGuiMouseCursor.ResizeNWSE] = MouseCursor.ResizeNwse;
        mouseCursorMap[ImGuiMouseCursor.NotAllowed] = MouseCursor.NotAllowed;
    }

    /// <summary>
    /// Forces the font texture atlas to be recomputed and re-cached
    /// </summary>
    public static unsafe void ReloadFonts()
    {
        ImGui.SetCurrentContext(imGuiContext);
        var io = ImGui.GetIO();

        io.Fonts.GetTexDataAsRGBA32(out byte* pixels, out var width, out var height, out _);

        var image = new Image
        {
            Data = new IntPtr(pixels),
            Width = width,
            Height = height,
            Mipmaps = 1,
            Format = PixelFormat.UncompressedR8G8B8A8,
        };

        if (fontTexture.IsReady())
            fontTexture.Unload();

        fontTexture = Texture2D.LoadFromImage(image);

        io.Fonts.SetTexID(new IntPtr(fontTexture.Id));
    }

    // ReSharper disable once InconsistentNaming
    private static unsafe sbyte* rlImGuiGetClipText(IntPtr userData)
    {
        var bytes = Encoding.ASCII.GetBytes(Window.GetClipboardText());

        fixed (byte* p = bytes)
            return (sbyte*)p;
    }

    // ReSharper disable once InconsistentNaming
    private static unsafe void rlImGuiSetClipText(IntPtr userData, sbyte* text)
    {
        try
        {
            Window.SetClipboardText(text->ToString());
        }
        catch (Exception e)
        {
            Log.Exception(e);
        }
    }

    private unsafe delegate sbyte* GetClipTextCallback(IntPtr userData);

    private unsafe delegate void SetClipTextCallback(IntPtr userData, sbyte* text);

    private static GetClipTextCallback getClipCallback = null!;
    private static SetClipTextCallback setClipCallback = null!;

    /// <summary>
    /// End Custom initialization. Not needed if you call Setup. Only needed if you want to add custom setup code.
    /// must be proceeded by BeginInitImGui
    /// </summary>
    public static void EndInitImGui()
    {
        SetupMouseCursors();

        ImGui.SetCurrentContext(imGuiContext);
        
        CopperImGui.LoadFonts();

        var io = ImGui.GetIO();

        io.BackendFlags |= ImGuiBackendFlags.HasMouseCursors | ImGuiBackendFlags.HasSetMousePos | ImGuiBackendFlags.HasGamepad;

        io.MousePos.X = 0;
        io.MousePos.Y = 0;

        // copy/paste callbacks
        unsafe
        {
            setClipCallback = rlImGuiSetClipText;
            io.SetClipboardTextFn = Marshal.GetFunctionPointerForDelegate(setClipCallback);

            getClipCallback = rlImGuiGetClipText;
            io.GetClipboardTextFn = Marshal.GetFunctionPointerForDelegate(getClipCallback);
        }

        io.ClipboardUserData = IntPtr.Zero;
        ReloadFonts();
    }

    private static void SetMouseEvent(ImGuiIOPtr io, MouseButton rayMouse, ImGuiMouseButton imGuiMouse)
    {
        if (Input.IsMouseButtonPressed(rayMouse))
            io.AddMouseButtonEvent((int)imGuiMouse, true);
        else if (Input.IsMouseButtonReleased(rayMouse))
            io.AddMouseButtonEvent((int)imGuiMouse, false);
    }

    private static void NewFrame(float dt = -1)
    {
        var io = ImGui.GetIO();

        if (Window.IsFullscreen())
        {
            var monitor = Window.GetCurrentMonitor();
            io.DisplaySize = new Vector2(Window.GetMonitorWidth(monitor), Window.GetMonitorHeight(monitor));
        }
        else
        {
            io.DisplaySize = new Vector2(Window.GetScreenWidth(), Window.GetScreenHeight());
        }

        io.DisplayFramebufferScale = new Vector2(1, 1);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || Window.IsState(ConfigFlags.HighDpiWindow))
            io.DisplayFramebufferScale = Window.GetScaleDPI();

        io.DeltaTime = dt >= 0 ? dt : Time.GetFrameTime();

        if (io.WantSetMousePos)
        {
            Input.SetMousePosition((int)io.MousePos.X, (int)io.MousePos.Y);
        }
        else
        {
            io.AddMousePosEvent(Input.GetMouseX(), Input.GetMouseY());
        }

        SetMouseEvent(io, MouseButton.Left, ImGuiMouseButton.Left);
        SetMouseEvent(io, MouseButton.Right, ImGuiMouseButton.Right);
        SetMouseEvent(io, MouseButton.Middle, ImGuiMouseButton.Middle);
        SetMouseEvent(io, MouseButton.Forward, ImGuiMouseButton.Middle + 1);
        SetMouseEvent(io, MouseButton.Back, ImGuiMouseButton.Middle + 2);

        var wheelMove = Input.GetMouseWheelMoveV();
        io.AddMouseWheelEvent(wheelMove.X, wheelMove.Y);

        if ((io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) != 0)
            return;

        var imguiCursor = ImGui.GetMouseCursor();

        if (imguiCursor == currentMouseCursor && !io.MouseDrawCursor)
            return;

        currentMouseCursor = imguiCursor;
        if (io.MouseDrawCursor || imguiCursor == ImGuiMouseCursor.None)
        {
            Input.HideCursor();
        }
        else
        {
            Input.ShowCursor();

            if ((io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) == 0)
                Input.SetMouseCursor(mouseCursorMap.GetValueOrDefault(imguiCursor, MouseCursor.Default));
        }
    }

    private static void FrameEvents()
    {
        var io = ImGui.GetIO();

        var focused = Window.IsFocused();
        if (focused != lastFrameFocused)
            io.AddFocusEvent(focused);
        lastFrameFocused = focused;


        // handle the modifyer key events so that shortcuts work
        var ctrlDown = rlImGuiIsControlDown();
        if (ctrlDown != lastControlPressed)
            io.AddKeyEvent(ImGuiKey.ModCtrl, ctrlDown);
        lastControlPressed = ctrlDown;

        var shiftDown = rlImGuiIsShiftDown();
        if (shiftDown != lastShiftPressed)
            io.AddKeyEvent(ImGuiKey.ModShift, shiftDown);
        lastShiftPressed = shiftDown;

        var altDown = rlImGuiIsAltDown();
        if (altDown != lastAltPressed)
            io.AddKeyEvent(ImGuiKey.ModAlt, altDown);
        lastAltPressed = altDown;

        var superDown = rlImGuiIsSuperDown();
        if (superDown != lastSuperPressed)
            io.AddKeyEvent(ImGuiKey.ModSuper, superDown);
        lastSuperPressed = superDown;

        // get the pressed keys, they are in event order
        var keyId = Input.GetKeyPressed();
        while (keyId != 0)
        {
            var key = (KeyboardKey)keyId;
            if (RaylibKeyMap.TryGetValue(key, out var value))
                io.AddKeyEvent(value, true);
            keyId = Input.GetKeyPressed();
        }

        // look for any keys that were down last frame and see if they were down and are released
        foreach (var keyItr in RaylibKeyMap)
        {
            if (Input.IsKeyReleased(keyItr.Key))
                io.AddKeyEvent(keyItr.Value, false);
        }

        // add the text input in order
        var pressed = Input.GetCharPressed();
        while (pressed != 0)
        {
            io.AddInputCharacter((uint)pressed);
            pressed = Input.GetCharPressed();
        }

        // gamepads
        if ((io.ConfigFlags & ImGuiConfigFlags.NavEnableGamepad) == 0 || !Input.IsGamepadAvailable(0))
            return;

        HandleGamepadButtonEvent(io, GamepadButton.LeftFaceUp, ImGuiKey.GamepadDpadUp);
        HandleGamepadButtonEvent(io, GamepadButton.LeftFaceRight, ImGuiKey.GamepadDpadRight);
        HandleGamepadButtonEvent(io, GamepadButton.LeftFaceDown, ImGuiKey.GamepadDpadDown);
        HandleGamepadButtonEvent(io, GamepadButton.LeftFaceLeft, ImGuiKey.GamepadDpadLeft);

        HandleGamepadButtonEvent(io, GamepadButton.RightFaceUp, ImGuiKey.GamepadFaceUp);
        HandleGamepadButtonEvent(io, GamepadButton.RightFaceRight, ImGuiKey.GamepadFaceLeft);
        HandleGamepadButtonEvent(io, GamepadButton.RightFaceDown, ImGuiKey.GamepadFaceDown);
        HandleGamepadButtonEvent(io, GamepadButton.RightFaceLeft, ImGuiKey.GamepadFaceRight);

        HandleGamepadButtonEvent(io, GamepadButton.LeftTrigger1, ImGuiKey.GamepadL1);
        HandleGamepadButtonEvent(io, GamepadButton.LeftTrigger2, ImGuiKey.GamepadL2);
        HandleGamepadButtonEvent(io, GamepadButton.RightTrigger1, ImGuiKey.GamepadR1);
        HandleGamepadButtonEvent(io, GamepadButton.RightTrigger2, ImGuiKey.GamepadR2);
        HandleGamepadButtonEvent(io, GamepadButton.LeftThumb, ImGuiKey.GamepadL3);
        HandleGamepadButtonEvent(io, GamepadButton.RightThumb, ImGuiKey.GamepadR3);

        HandleGamepadButtonEvent(io, GamepadButton.MiddleLeft, ImGuiKey.GamepadStart);
        HandleGamepadButtonEvent(io, GamepadButton.MiddleRight, ImGuiKey.GamepadBack);

        // left stick
        HandleGamepadStickEvent(io, GamepadAxis.LeftX, ImGuiKey.GamepadLStickLeft, ImGuiKey.GamepadLStickRight);
        HandleGamepadStickEvent(io, GamepadAxis.LeftY, ImGuiKey.GamepadLStickUp, ImGuiKey.GamepadLStickDown);

        // right stick
        HandleGamepadStickEvent(io, GamepadAxis.RightX, ImGuiKey.GamepadRStickLeft, ImGuiKey.GamepadRStickRight);
        HandleGamepadStickEvent(io, GamepadAxis.RightY, ImGuiKey.GamepadRStickUp, ImGuiKey.GamepadRStickDown);
    }


    private static void HandleGamepadButtonEvent(ImGuiIOPtr io, GamepadButton button, ImGuiKey key)
    {
        if (Input.IsGamepadButtonPressed(0, button))
            io.AddKeyEvent(key, true);
        else if (Input.IsGamepadButtonReleased(0, button))
            io.AddKeyEvent(key, false);
    }

    private static void HandleGamepadStickEvent(ImGuiIOPtr io, GamepadAxis axis, ImGuiKey negKey, ImGuiKey posKey)
    {
        const float deadZone = 0.20f;

        var axisValue = Input.GetGamepadAxisMovement(0, axis);

        io.AddKeyAnalogEvent(negKey, axisValue < -deadZone, axisValue < -deadZone ? -axisValue : 0);
        io.AddKeyAnalogEvent(posKey, axisValue > deadZone, axisValue > deadZone ? axisValue : 0);
    }

    /// <summary>
    /// Starts a new ImGui Frame
    /// </summary>
    /// <param name="dt">optional delta time, any value less than 0 will use raylib GetFrameTime</param>
    public static void Begin(float dt = -1)
    {
        ImGui.SetCurrentContext(imGuiContext);

        NewFrame(dt);
        FrameEvents();
        ImGui.NewFrame();
    }

    private static void EnableScissor(float x, float y, float width, float height)
    {
        RlGl.EnableScissorTest();
        var io = ImGui.GetIO();

        var scale = new Vector2(1.0f, 1.0f);
        if (Window.IsState(ConfigFlags.HighDpiWindow) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            scale = io.DisplayFramebufferScale;

        RlGl.Scissor((int)(x * scale.X), (int)((io.DisplaySize.Y - (int)(y + height)) * scale.Y), (int)(width * scale.X), (int)(height * scale.Y));
    }

    private static void TriangleVert(ImDrawVertPtr idxVert)
    {
        var color = ImGui.ColorConvertU32ToFloat4(idxVert.col);

        RlGl.Color4F(color.X, color.Y, color.Z, color.W);
        RlGl.TexCoord2F(idxVert.uv.X, idxVert.uv.Y);
        RlGl.Vertex2F(idxVert.pos.X, idxVert.pos.Y);
    }

    private static void RenderTriangles(uint count, uint indexStart, ImVector<ushort> indexBuffer, ImPtrVector<ImDrawVertPtr> vertBuffer, IntPtr texturePtr)
    {
        if (count < 3)
            return;

        uint textureId = 0;
        if (texturePtr != IntPtr.Zero)
            textureId = (uint)texturePtr.ToInt32();

        RlGl.Begin(DrawMode.Triangles);
        RlGl.SetTexture(textureId);

        for (var i = 0; i <= (count - 3); i += 3)
        {
            if (RlGl.CheckRenderBatchLimit(3))
            {
                RlGl.Begin(DrawMode.Triangles);
                RlGl.SetTexture(textureId);
            }

            var indexA = indexBuffer[(int)indexStart + i];
            var indexB = indexBuffer[(int)indexStart + i + 1];
            var indexC = indexBuffer[(int)indexStart + i + 2];

            var vertexA = vertBuffer[indexA];
            var vertexB = vertBuffer[indexB];
            var vertexC = vertBuffer[indexC];

            TriangleVert(vertexA);
            TriangleVert(vertexB);
            TriangleVert(vertexC);
        }

        RlGl.End();
    }

    private delegate void Callback(ImDrawListPtr list, ImDrawCmdPtr cmd);

    private static void RenderData()
    {
        RlGl.DrawRenderBatchActive();
        RlGl.DisableBackfaceCulling();

        var data = ImGui.GetDrawData();

        for (var l = 0; l < data.CmdListsCount; l++)
        {
            var commandList = data.CmdLists[l];

            for (var cmdIndex = 0; cmdIndex < commandList.CmdBuffer.Size; cmdIndex++)
            {
                var cmd = commandList.CmdBuffer[cmdIndex];

                EnableScissor(cmd.ClipRect.X - data.DisplayPos.X, cmd.ClipRect.Y - data.DisplayPos.Y,
                    cmd.ClipRect.Z - (cmd.ClipRect.X - data.DisplayPos.X),
                    cmd.ClipRect.W - (cmd.ClipRect.Y - data.DisplayPos.Y));

                if (cmd.UserCallback != IntPtr.Zero)
                {
                    var cb = Marshal.GetDelegateForFunctionPointer<Callback>(cmd.UserCallback);
                    cb(commandList, cmd);
                    continue;
                }

                RenderTriangles(cmd.ElemCount, cmd.IdxOffset, commandList.IdxBuffer, commandList.VtxBuffer, cmd.TextureId);

                RlGl.DrawRenderBatchActive();
            }
        }

        RlGl.SetTexture(0);
        RlGl.DisableScissorTest();
        RlGl.EnableBackfaceCulling();
    }

    /// <summary>
    /// Ends an ImGui frame and submits all ImGui drawing to raylib for processing.
    /// </summary>
    public static void End()
    {
        ImGui.SetCurrentContext(imGuiContext);
        ImGui.Render();
        RenderData();
    }

    /// <summary>
    /// Cleanup ImGui and unload font atlas
    /// </summary>
    public static void Shutdown()
    {
        fontTexture.Unload();
        ImGui.DestroyContext();
    }

    /// <summary>
    /// Draw a texture as an image in an ImGui Context
    /// Uses the current ImGui Cursor position and the full texture size.
    /// </summary>
    /// <param name="image">The raylib texture to draw</param>
    public static void Image(Texture2D image)
    {
        ImGui.Image(new IntPtr(image.Id), new Vector2(image.Width, image.Height));
    }

    /// <summary>
    /// Draw a texture as an image in an ImGui Context at a specific size
    /// Uses the current ImGui Cursor position and the specified width and height
    /// The image will be scaled up or down to fit as needed
    /// </summary>
    /// <param name="image">The raylib texture to draw</param>
    /// <param name="width">The width of the drawn image</param>
    /// <param name="height">The height of the drawn image</param>
    public static void ImageSize(Texture2D image, int width, int height)
    {
        ImGui.Image(new IntPtr(image.Id), new Vector2(width, height));
    }

    /// <summary>
    /// Draw a texture as an image in an ImGui Context at a specific size
    /// Uses the current ImGui Cursor position and the specified size
    /// The image will be scaled up or down to fit as needed
    /// </summary>
    /// <param name="image">The raylib texture to draw</param>
    /// <param name="size">The size of drawn image</param>
    public static void ImageSize(Texture2D image, Vector2 size)
    {
        ImGui.Image(new IntPtr(image.Id), size);
    }

    /// <summary>
    /// Draw a portion texture as an image in an ImGui Context at a defined size
    /// Uses the current ImGui Cursor position and the specified size
    /// The image will be scaled up or down to fit as needed
    /// </summary>
    /// <param name="image">The raylib texture to draw</param>
    /// <param name="destWidth">The width of the drawn image</param>
    /// <param name="destHeight">The height of the drawn image</param>
    /// <param name="sourceRect">The portion of the texture to draw as an image. Negative values for the width and height will flip the image</param>
    public static void ImageRect(Texture2D image, int destWidth, int destHeight, Rectangle sourceRect)
    {
        var uv0 = new Vector2();
        var uv1 = new Vector2();

        if (sourceRect.Width < 0)
        {
            uv0.X = -(sourceRect.X / image.Width);
            uv1.X = (uv0.X - Math.Abs(sourceRect.Width) / image.Width);
        }
        else
        {
            uv0.X = sourceRect.X / image.Width;
            uv1.X = uv0.X + sourceRect.Width / image.Width;
        }

        if (sourceRect.Height < 0)
        {
            uv0.Y = -(sourceRect.Y / image.Height);
            uv1.Y = uv0.Y - Math.Abs(sourceRect.Height) / image.Height;
        }
        else
        {
            uv0.Y = sourceRect.Y / image.Height;
            uv1.Y = uv0.Y + sourceRect.Height / image.Height;
        }

        ImGui.Image(new IntPtr(image.Id), new Vector2(destWidth, destHeight), uv0, uv1);
    }

    /// <summary>
    /// Draws a render texture as an image an ImGui Context, automatically flipping the Y axis so it will show correctly on screen
    /// </summary>
    /// <param name="image">The render texture to draw</param>
    public static void ImageRenderTexture(RenderTexture2D image)
    {
        ImageRect(image.Texture, image.Texture.Width, image.Texture.Height, new Rectangle(0, 0, image.Texture.Width, -image.Texture.Height));
    }

    /// <summary>
    /// Draws a render texture as an image to the current ImGui Context, flipping the Y axis so it will show correctly on the screen
    /// The texture will be scaled to fit the content are available, centered if desired
    /// </summary>
    /// <param name="image">The render texture to draw</param>
    /// <param name="center">When true the texture will be centered in the content area. When false the image will be left and top justified</param>
    public static void ImageRenderTextureFit(RenderTexture2D image, bool center = true)
    {
        var area = ImGui.GetContentRegionAvail();

        var scale = area.X / image.Texture.Width;

        var y = image.Texture.Height * scale;
        if (y > area.Y)
        {
            scale = area.Y / image.Texture.Height;
        }

        var sizeX = (int)(image.Texture.Width * scale);
        var sizeY = (int)(image.Texture.Height * scale);

        if (center)
        {
            ImGui.SetCursorPosX(0);
            ImGui.SetCursorPosX((area.X / 2f) - (sizeX / 2f));
            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + ((area.Y / 2f) - (sizeY / 2f)));
        }

        ImageRect(image.Texture, sizeX, sizeY, new Rectangle(0, 0, (image.Texture.Width), -(image.Texture.Height)));
    }

    /// <summary>
    /// Draws a texture as an image button in an ImGui context. Uses the current ImGui cursor position and the full size of the texture
    /// </summary>
    /// <param name="name">The display name and ImGui ID for the button</param>
    /// <param name="image">The texture to draw</param>
    /// <returns>True if the button was clicked</returns>
    public static bool ImageButton(string name, Texture2D image)
    {
        return ImageButtonSize(name, image, new Vector2(image.Width, image.Height));
    }

    /// <summary>
    /// Draws a texture as an image button in an ImGui context. Uses the current ImGui cursor position and the specified size.
    /// </summary>
    /// <param name="name">The display name and ImGui ID for the button</param>
    /// <param name="image">The texture to draw</param>
    /// <param name="size">The size of the button</param>
    /// <returns>True if the button was clicked</returns>
    public static bool ImageButtonSize(string name, Texture2D image, Vector2 size)
    {
        return ImGui.ImageButton(name, new IntPtr(image.Id), size);
    }
}