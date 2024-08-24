using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using CopperDevs.Core;
using CopperDevs.DearImGui.Wrapping.Enums;
using Raylib_CSharp;
using Raylib_CSharp.Collision;
using Raylib_CSharp.Images;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering.Gl;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Transformations;
using Raylib_CSharp.Windowing;
using ConfigFlags = CopperDevs.DearImGui.Wrapping.Enums.ConfigFlags;
using rlConfigFlags = Raylib_CSharp.Windowing.ConfigFlags;


namespace CopperDevs.DearImGui.Renderer.Raylib.Internal.Rendering;

// ReSharper disable once InconsistentNaming
internal static class rlImGui
{
    private static CursorInput currentMouseCursor = CursorInput.COUNT;
    private static Dictionary<CursorInput, MouseCursor> mouseCursorMap = [];
    private static Texture2D fontTexture;

    private static readonly Dictionary<KeyboardKey, InputKeys> RaylibKeyMap = [];

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
        mouseCursorMap = new Dictionary<CursorInput, MouseCursor>();

        lastFrameFocused = Window.IsFocused();
        lastControlPressed = false;
        lastShiftPressed = false;
        lastAltPressed = false;
        lastSuperPressed = false;

        fontTexture.Id = 0;

        SetupKeymap();

        CopperImGui.CreateContext(true);
    }

    internal static void SetupKeymap()
    {
        if (RaylibKeyMap.Count > 0)
            return;

        // build up a map of raylib keys to ImGuiKeys
        RaylibKeyMap[KeyboardKey.Apostrophe] = InputKeys.Apostrophe;
        RaylibKeyMap[KeyboardKey.Comma] = InputKeys.Comma;
        RaylibKeyMap[KeyboardKey.Minus] = InputKeys.Minus;
        RaylibKeyMap[KeyboardKey.Period] = InputKeys.Period;
        RaylibKeyMap[KeyboardKey.Slash] = InputKeys.Slash;
        RaylibKeyMap[KeyboardKey.Zero] = InputKeys._0;
        RaylibKeyMap[KeyboardKey.One] = InputKeys._1;
        RaylibKeyMap[KeyboardKey.Two] = InputKeys._2;
        RaylibKeyMap[KeyboardKey.Three] = InputKeys._3;
        RaylibKeyMap[KeyboardKey.Four] = InputKeys._4;
        RaylibKeyMap[KeyboardKey.Five] = InputKeys._5;
        RaylibKeyMap[KeyboardKey.Six] = InputKeys._6;
        RaylibKeyMap[KeyboardKey.Seven] = InputKeys._7;
        RaylibKeyMap[KeyboardKey.Eight] = InputKeys._8;
        RaylibKeyMap[KeyboardKey.Nine] = InputKeys._9;
        RaylibKeyMap[KeyboardKey.Semicolon] = InputKeys.Semicolon;
        RaylibKeyMap[KeyboardKey.Equal] = InputKeys.Equal;
        RaylibKeyMap[KeyboardKey.A] = InputKeys.A;
        RaylibKeyMap[KeyboardKey.B] = InputKeys.B;
        RaylibKeyMap[KeyboardKey.C] = InputKeys.C;
        RaylibKeyMap[KeyboardKey.D] = InputKeys.D;
        RaylibKeyMap[KeyboardKey.E] = InputKeys.E;
        RaylibKeyMap[KeyboardKey.F] = InputKeys.F;
        RaylibKeyMap[KeyboardKey.G] = InputKeys.G;
        RaylibKeyMap[KeyboardKey.H] = InputKeys.H;
        RaylibKeyMap[KeyboardKey.I] = InputKeys.I;
        RaylibKeyMap[KeyboardKey.J] = InputKeys.J;
        RaylibKeyMap[KeyboardKey.K] = InputKeys.K;
        RaylibKeyMap[KeyboardKey.L] = InputKeys.L;
        RaylibKeyMap[KeyboardKey.M] = InputKeys.M;
        RaylibKeyMap[KeyboardKey.N] = InputKeys.N;
        RaylibKeyMap[KeyboardKey.O] = InputKeys.O;
        RaylibKeyMap[KeyboardKey.P] = InputKeys.P;
        RaylibKeyMap[KeyboardKey.Q] = InputKeys.Q;
        RaylibKeyMap[KeyboardKey.R] = InputKeys.R;
        RaylibKeyMap[KeyboardKey.S] = InputKeys.S;
        RaylibKeyMap[KeyboardKey.T] = InputKeys.T;
        RaylibKeyMap[KeyboardKey.U] = InputKeys.U;
        RaylibKeyMap[KeyboardKey.V] = InputKeys.V;
        RaylibKeyMap[KeyboardKey.W] = InputKeys.W;
        RaylibKeyMap[KeyboardKey.X] = InputKeys.X;
        RaylibKeyMap[KeyboardKey.Y] = InputKeys.Y;
        RaylibKeyMap[KeyboardKey.Z] = InputKeys.Z;
        RaylibKeyMap[KeyboardKey.Space] = InputKeys.Space;
        RaylibKeyMap[KeyboardKey.Escape] = InputKeys.Escape;
        RaylibKeyMap[KeyboardKey.Enter] = InputKeys.Enter;
        RaylibKeyMap[KeyboardKey.Tab] = InputKeys.Tab;
        RaylibKeyMap[KeyboardKey.Backspace] = InputKeys.Backspace;
        RaylibKeyMap[KeyboardKey.Insert] = InputKeys.Insert;
        RaylibKeyMap[KeyboardKey.Delete] = InputKeys.Delete;
        RaylibKeyMap[KeyboardKey.Right] = InputKeys.RightArrow;
        RaylibKeyMap[KeyboardKey.Left] = InputKeys.LeftArrow;
        RaylibKeyMap[KeyboardKey.Down] = InputKeys.DownArrow;
        RaylibKeyMap[KeyboardKey.Up] = InputKeys.UpArrow;
        RaylibKeyMap[KeyboardKey.PageUp] = InputKeys.PageUp;
        RaylibKeyMap[KeyboardKey.PageDown] = InputKeys.PageDown;
        RaylibKeyMap[KeyboardKey.Home] = InputKeys.Home;
        RaylibKeyMap[KeyboardKey.End] = InputKeys.End;
        RaylibKeyMap[KeyboardKey.CapsLock] = InputKeys.CapsLock;
        RaylibKeyMap[KeyboardKey.ScrollLock] = InputKeys.ScrollLock;
        RaylibKeyMap[KeyboardKey.NumLock] = InputKeys.NumLock;
        RaylibKeyMap[KeyboardKey.PrintScreen] = InputKeys.PrintScreen;
        RaylibKeyMap[KeyboardKey.Pause] = InputKeys.Pause;
        RaylibKeyMap[KeyboardKey.F1] = InputKeys.F1;
        RaylibKeyMap[KeyboardKey.F2] = InputKeys.F2;
        RaylibKeyMap[KeyboardKey.F3] = InputKeys.F3;
        RaylibKeyMap[KeyboardKey.F4] = InputKeys.F4;
        RaylibKeyMap[KeyboardKey.F5] = InputKeys.F5;
        RaylibKeyMap[KeyboardKey.F6] = InputKeys.F6;
        RaylibKeyMap[KeyboardKey.F7] = InputKeys.F7;
        RaylibKeyMap[KeyboardKey.F8] = InputKeys.F8;
        RaylibKeyMap[KeyboardKey.F9] = InputKeys.F9;
        RaylibKeyMap[KeyboardKey.F10] = InputKeys.F10;
        RaylibKeyMap[KeyboardKey.F11] = InputKeys.F11;
        RaylibKeyMap[KeyboardKey.F12] = InputKeys.F12;
        RaylibKeyMap[KeyboardKey.LeftShift] = InputKeys.LeftShift;
        RaylibKeyMap[KeyboardKey.LeftControl] = InputKeys.LeftCtrl;
        RaylibKeyMap[KeyboardKey.LeftAlt] = InputKeys.LeftAlt;
        RaylibKeyMap[KeyboardKey.LeftSuper] = InputKeys.LeftSuper;
        RaylibKeyMap[KeyboardKey.RightShift] = InputKeys.RightShift;
        RaylibKeyMap[KeyboardKey.RightControl] = InputKeys.RightCtrl;
        RaylibKeyMap[KeyboardKey.RightAlt] = InputKeys.RightAlt;
        RaylibKeyMap[KeyboardKey.RightSuper] = InputKeys.RightSuper;
        RaylibKeyMap[KeyboardKey.KeyboardMenu] = InputKeys.Menu;
        RaylibKeyMap[KeyboardKey.LeftBracket] = InputKeys.LeftBracket;
        RaylibKeyMap[KeyboardKey.Backslash] = InputKeys.Backslash;
        RaylibKeyMap[KeyboardKey.RightBracket] = InputKeys.RightBracket;
        RaylibKeyMap[KeyboardKey.Grave] = InputKeys.GraveAccent;
        RaylibKeyMap[KeyboardKey.Kp0] = InputKeys.Keypad0;
        RaylibKeyMap[KeyboardKey.Kp1] = InputKeys.Keypad1;
        RaylibKeyMap[KeyboardKey.Kp2] = InputKeys.Keypad2;
        RaylibKeyMap[KeyboardKey.Kp3] = InputKeys.Keypad3;
        RaylibKeyMap[KeyboardKey.Kp4] = InputKeys.Keypad4;
        RaylibKeyMap[KeyboardKey.Kp5] = InputKeys.Keypad5;
        RaylibKeyMap[KeyboardKey.Kp6] = InputKeys.Keypad6;
        RaylibKeyMap[KeyboardKey.Kp7] = InputKeys.Keypad7;
        RaylibKeyMap[KeyboardKey.Kp8] = InputKeys.Keypad8;
        RaylibKeyMap[KeyboardKey.Kp9] = InputKeys.Keypad9;
        RaylibKeyMap[KeyboardKey.KpDecimal] = InputKeys.KeypadDecimal;
        RaylibKeyMap[KeyboardKey.KpDivide] = InputKeys.KeypadDivide;
        RaylibKeyMap[KeyboardKey.KpMultiply] = InputKeys.KeypadMultiply;
        RaylibKeyMap[KeyboardKey.KpSubtract] = InputKeys.KeypadSubtract;
        RaylibKeyMap[KeyboardKey.KpAdd] = InputKeys.KeypadAdd;
        RaylibKeyMap[KeyboardKey.KpEnter] = InputKeys.KeypadEnter;
        RaylibKeyMap[KeyboardKey.KpEqual] = InputKeys.KeypadEqual;
    }

    private static void SetupMouseCursors()
    {
        mouseCursorMap.Clear();
        mouseCursorMap[CursorInput.Arrow] = MouseCursor.Arrow;
        mouseCursorMap[CursorInput.TextInput] = MouseCursor.IBeam;
        mouseCursorMap[CursorInput.Hand] = MouseCursor.PointingHand;
        mouseCursorMap[CursorInput.ResizeAll] = MouseCursor.ResizeAll;
        mouseCursorMap[CursorInput.ResizeEW] = MouseCursor.ResizeEw;
        mouseCursorMap[CursorInput.ResizeNESW] = MouseCursor.ResizeNesw;
        mouseCursorMap[CursorInput.ResizeNS] = MouseCursor.ResizeNs;
        mouseCursorMap[CursorInput.ResizeNWSE] = MouseCursor.ResizeNwse;
        mouseCursorMap[CursorInput.NotAllowed] = MouseCursor.NotAllowed;
    }

    /// <summary>
    /// Forces the font texture atlas to be recomputed and re-cached
    /// </summary>
    public static unsafe void ReloadFonts()
    {
        CopperImGui.SetCurrentContext(CopperImGui.GetCurrentContext());
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

        CopperImGui.SetCurrentContext(CopperImGui.GetCurrentContext());


        CopperImGui.LoadFonts();

        var io = ImGui.GetIO();

        io.BackendFlags |= BackendFlags.HasMouseCursors | BackendFlags.HasSetMousePos | BackendFlags.HasGamepad;

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

    private static void SetMouseEvent(ImGuiIOPtr io, MouseButton rayMouse, CursorButtonsInput imGuiMouse)
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


        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || Window.IsState(rlConfigFlags.HighDpiWindow))
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

        SetMouseEvent(io, MouseButton.Left, CursorButtonsInput.Left);
        SetMouseEvent(io, MouseButton.Right, CursorButtonsInput.Right);
        SetMouseEvent(io, MouseButton.Middle, CursorButtonsInput.Middle);
        SetMouseEvent(io, MouseButton.Forward, CursorButtonsInput.Middle + 1);
        SetMouseEvent(io, MouseButton.Back, CursorButtonsInput.Middle + 2);

        var wheelMove = Input.GetMouseWheelMoveV();
        io.AddMouseWheelEvent(wheelMove.X, wheelMove.Y);

        if (!CopperImGui.BackendHasFlag(ConfigFlags.NoMouseCursorChange))
            return;

        var imguiCursor = CopperImGui.GetMouseCursor();

        if (imguiCursor == currentMouseCursor && !io.MouseDrawCursor)
            return;

        currentMouseCursor = imguiCursor;
        if (io.MouseDrawCursor || imguiCursor == CursorInput.None)
        {
            Input.HideCursor();
        }
        else
        {
            Input.ShowCursor();

            if (CopperImGui.BackendHasFlag(ConfigFlags.NoMouseCursorChange))
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


        // handle the modifier key events so that shortcuts work
        var ctrlDown = rlImGuiIsControlDown();
        if (ctrlDown != lastControlPressed)
            io.AddKeyEvent(InputKeys.ModCtrl, ctrlDown);
        lastControlPressed = ctrlDown;

        var shiftDown = rlImGuiIsShiftDown();
        if (shiftDown != lastShiftPressed)
            io.AddKeyEvent(InputKeys.ModShift, shiftDown);
        lastShiftPressed = shiftDown;

        var altDown = rlImGuiIsAltDown();
        if (altDown != lastAltPressed)
            io.AddKeyEvent(InputKeys.ModAlt, altDown);
        lastAltPressed = altDown;

        var superDown = rlImGuiIsSuperDown();
        if (superDown != lastSuperPressed)
            io.AddKeyEvent(InputKeys.ModSuper, superDown);
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
        if (!CopperImGui.BackendHasFlag(ConfigFlags.NavEnableGamepad) || !Input.IsGamepadAvailable(0))
            return;

        HandleGamepadButtonEvent(io, GamepadButton.LeftFaceUp, InputKeys.GamepadDpadUp);
        HandleGamepadButtonEvent(io, GamepadButton.LeftFaceRight, InputKeys.GamepadDpadRight);
        HandleGamepadButtonEvent(io, GamepadButton.LeftFaceDown, InputKeys.GamepadDpadDown);
        HandleGamepadButtonEvent(io, GamepadButton.LeftFaceLeft, InputKeys.GamepadDpadLeft);

        HandleGamepadButtonEvent(io, GamepadButton.RightFaceUp, InputKeys.GamepadFaceUp);
        HandleGamepadButtonEvent(io, GamepadButton.RightFaceRight, InputKeys.GamepadFaceLeft);
        HandleGamepadButtonEvent(io, GamepadButton.RightFaceDown, InputKeys.GamepadFaceDown);
        HandleGamepadButtonEvent(io, GamepadButton.RightFaceLeft, InputKeys.GamepadFaceRight);

        HandleGamepadButtonEvent(io, GamepadButton.LeftTrigger1, InputKeys.GamepadL1);
        HandleGamepadButtonEvent(io, GamepadButton.LeftTrigger2, InputKeys.GamepadL2);
        HandleGamepadButtonEvent(io, GamepadButton.RightTrigger1, InputKeys.GamepadR1);
        HandleGamepadButtonEvent(io, GamepadButton.RightTrigger2, InputKeys.GamepadR2);
        HandleGamepadButtonEvent(io, GamepadButton.LeftThumb, InputKeys.GamepadL3);
        HandleGamepadButtonEvent(io, GamepadButton.RightThumb, InputKeys.GamepadR3);

        HandleGamepadButtonEvent(io, GamepadButton.MiddleLeft, InputKeys.GamepadStart);
        HandleGamepadButtonEvent(io, GamepadButton.MiddleRight, InputKeys.GamepadBack);

        // left stick
        HandleGamepadStickEvent(io, GamepadAxis.LeftX, InputKeys.GamepadLStickLeft, InputKeys.GamepadLStickRight);
        HandleGamepadStickEvent(io, GamepadAxis.LeftY, InputKeys.GamepadLStickUp, InputKeys.GamepadLStickDown);

        // right stick
        HandleGamepadStickEvent(io, GamepadAxis.RightX, InputKeys.GamepadRStickLeft, InputKeys.GamepadRStickRight);
        HandleGamepadStickEvent(io, GamepadAxis.RightY, InputKeys.GamepadRStickUp, InputKeys.GamepadRStickDown);
    }


    private static void HandleGamepadButtonEvent(ImGuiIOPtr io, GamepadButton button, InputKeys key)
    {
        if (Input.IsGamepadButtonPressed(0, button))
            io.AddKeyEvent(key, true);
        else if (Input.IsGamepadButtonReleased(0, button))
            io.AddKeyEvent(key, false);
    }

    private static void HandleGamepadStickEvent(ImGuiIOPtr io, GamepadAxis axis, InputKeys negKey, InputKeys posKey)
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
        CopperImGui.SetCurrentContext(CopperImGui.GetCurrentContext());

        NewFrame(dt);
        FrameEvents();
        CopperImGui.NewFrame();
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

    private static void TriangleVert(ImGuiNET.ImDrawVertPtr idxVert)
    {
        var color = CopperImGui.ColorConvertU32ToFloat4(idxVert.col);

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

        var data = CopperImGui.GetDrawData();

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
        CopperImGui.SetCurrentContext(CopperImGui.GetCurrentContext());
        CopperImGui.Render();
        RenderData();
    }

    /// <summary>
    /// Cleanup ImGui and unload font atlas
    /// </summary>
    public static void Shutdown()
    {
        fontTexture.Unload();
        CopperImGui.DestroyCurrentContext();
    }

    /// <summary>
    /// Draw a texture as an image in an ImGui Context
    /// Uses the current ImGui Cursor position and the full texture size.
    /// </summary>
    /// <param name="image">The raylib texture to draw</param>
    public static void Image(Texture2D image)
    {
        CopperImGui.Image(new IntPtr(image.Id), new Vector2(image.Width, image.Height));
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
        CopperImGui.Image(new IntPtr(image.Id), new Vector2(width, height));
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
        CopperImGui.Image(new IntPtr(image.Id), size);
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

        CopperImGui.Image(new IntPtr(image.Id), new Vector2(destWidth, destHeight), uv0, uv1);
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
        var area = CopperImGui.GetContentRegionAvail();

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
            CopperImGui.Cursor.X = 0;
            CopperImGui.Cursor.X = area.X / 2f - sizeX / 2f;
            CopperImGui.Cursor.Y += area.Y / 2f - sizeY / 2f;
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
        return CopperImGui.ImageButton(name, new IntPtr(image.Id), size);
    }
}