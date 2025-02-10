using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using Bliss.CSharp.Interact;
using Bliss.CSharp.Interact.Gamepads;
using Bliss.CSharp.Interact.Keyboards;
using Bliss.CSharp.Interact.Mice;
using Bliss.CSharp.Interact.Mice.Cursors;
using Bliss.CSharp.Textures;
using Bliss.CSharp.Windowing;
using CopperDevs.Logger;
using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui.Renderer.Bliss;

// ReSharper disable once InconsistentNaming
internal static class BlissImGui
{
    private static ImGuiContextPtr imGuiContext = ImGuiContextPtr.Null;

    private static ImGuiMouseCursor currentMouseCursor = ImGuiMouseCursor.Count;
    private static Dictionary<ImGuiMouseCursor, SystemCursor> mouseCursorMap = [];
    private static Texture2D fontTexture;

    private static readonly Dictionary<KeyboardKey, ImGuiKey> BlissKeyMap = [];

    private static bool lastFrameFocused;

    private static bool lastControlPressed;
    private static bool lastShiftPressed;
    private static bool lastAltPressed;
    private static bool lastSuperPressed;

    // ReSharper disable once InconsistentNaming
    private static bool IsControlDown() => Input.IsKeyDown(KeyboardKey.ControlRight) || Input.IsKeyDown(KeyboardKey.ControlLeft);

    // ReSharper disable once InconsistentNaming
    private static bool IsShiftDown() => Input.IsKeyDown(KeyboardKey.ShiftRight) || Input.IsKeyDown(KeyboardKey.ShiftLeft);

    // ReSharper disable once InconsistentNaming
    private static bool IsAltDown() => Input.IsKeyDown(KeyboardKey.AltRight) || Input.IsKeyDown(KeyboardKey.AltLeft);

    // ReSharper disable once InconsistentNaming
    private static bool IsSuperDown() => Input.IsKeyDown(KeyboardKey.WinRight) || Input.IsKeyDown(KeyboardKey.WinLeft);

    /// <summary>
    /// Sets up ImGui, loads fonts and themes
    /// </summary>
    public static void Setup()
    {
        BeginInitImGui();

        EndInitImGui();
    }

    public static void BeginInitImGui()
    {
        mouseCursorMap = new Dictionary<ImGuiMouseCursor, SystemCursor>();
        
        lastFrameFocused = BlissRenderer.Window.IsFocused;
        lastControlPressed = false;
        lastShiftPressed = false;
        lastAltPressed = false;
        lastSuperPressed = false;

        SetupKeymap();

        imGuiContext = CopperImGui.CreateContext(true);
    }

    internal static void SetupKeymap()
    {
        if (BlissKeyMap.Count > 0)
            return;

        // BlissKeyMap[KeyboardKey.Apostrophe] = ImGuiKey.Apostrophe;
        BlissKeyMap[KeyboardKey.Comma] = ImGuiKey.Comma;
        BlissKeyMap[KeyboardKey.Minus] = ImGuiKey.Minus;
        BlissKeyMap[KeyboardKey.Period] = ImGuiKey.Period;
        BlissKeyMap[KeyboardKey.Slash] = ImGuiKey.Slash;
        BlissKeyMap[KeyboardKey.Number0] = ImGuiKey.Key0;
        BlissKeyMap[KeyboardKey.Number1] = ImGuiKey.Key1;
        BlissKeyMap[KeyboardKey.Number2] = ImGuiKey.Key2;
        BlissKeyMap[KeyboardKey.Number3] = ImGuiKey.Key3;
        BlissKeyMap[KeyboardKey.Number4] = ImGuiKey.Key4;
        BlissKeyMap[KeyboardKey.Number5] = ImGuiKey.Key5;
        BlissKeyMap[KeyboardKey.Number6] = ImGuiKey.Key6;
        BlissKeyMap[KeyboardKey.Number7] = ImGuiKey.Key7;
        BlissKeyMap[KeyboardKey.Number8] = ImGuiKey.Key8;
        BlissKeyMap[KeyboardKey.Number9] = ImGuiKey.Key9;
        BlissKeyMap[KeyboardKey.Semicolon] = ImGuiKey.Semicolon;
        BlissKeyMap[KeyboardKey.Plus] = ImGuiKey.Equal;
        BlissKeyMap[KeyboardKey.A] = ImGuiKey.A;
        BlissKeyMap[KeyboardKey.B] = ImGuiKey.B;
        BlissKeyMap[KeyboardKey.C] = ImGuiKey.C;
        BlissKeyMap[KeyboardKey.D] = ImGuiKey.D;
        BlissKeyMap[KeyboardKey.E] = ImGuiKey.E;
        BlissKeyMap[KeyboardKey.F] = ImGuiKey.F;
        BlissKeyMap[KeyboardKey.G] = ImGuiKey.G;
        BlissKeyMap[KeyboardKey.H] = ImGuiKey.H;
        BlissKeyMap[KeyboardKey.I] = ImGuiKey.I;
        BlissKeyMap[KeyboardKey.J] = ImGuiKey.J;
        BlissKeyMap[KeyboardKey.K] = ImGuiKey.K;
        BlissKeyMap[KeyboardKey.L] = ImGuiKey.L;
        BlissKeyMap[KeyboardKey.M] = ImGuiKey.M;
        BlissKeyMap[KeyboardKey.N] = ImGuiKey.N;
        BlissKeyMap[KeyboardKey.O] = ImGuiKey.O;
        BlissKeyMap[KeyboardKey.P] = ImGuiKey.P;
        BlissKeyMap[KeyboardKey.Q] = ImGuiKey.Q;
        BlissKeyMap[KeyboardKey.R] = ImGuiKey.R;
        BlissKeyMap[KeyboardKey.S] = ImGuiKey.S;
        BlissKeyMap[KeyboardKey.T] = ImGuiKey.T;
        BlissKeyMap[KeyboardKey.U] = ImGuiKey.U;
        BlissKeyMap[KeyboardKey.V] = ImGuiKey.V;
        BlissKeyMap[KeyboardKey.W] = ImGuiKey.W;
        BlissKeyMap[KeyboardKey.X] = ImGuiKey.X;
        BlissKeyMap[KeyboardKey.Y] = ImGuiKey.Y;
        BlissKeyMap[KeyboardKey.Z] = ImGuiKey.Z;
        BlissKeyMap[KeyboardKey.Space] = ImGuiKey.Space;
        BlissKeyMap[KeyboardKey.Escape] = ImGuiKey.Escape;
        BlissKeyMap[KeyboardKey.Enter] = ImGuiKey.Enter;
        BlissKeyMap[KeyboardKey.Tab] = ImGuiKey.Tab;
        BlissKeyMap[KeyboardKey.BackSpace] = ImGuiKey.Backspace;
        BlissKeyMap[KeyboardKey.Insert] = ImGuiKey.Insert;
        BlissKeyMap[KeyboardKey.Delete] = ImGuiKey.Delete;
        BlissKeyMap[KeyboardKey.Right] = ImGuiKey.RightArrow;
        BlissKeyMap[KeyboardKey.Left] = ImGuiKey.LeftArrow;
        BlissKeyMap[KeyboardKey.Down] = ImGuiKey.DownArrow;
        BlissKeyMap[KeyboardKey.Up] = ImGuiKey.UpArrow;
        BlissKeyMap[KeyboardKey.PageUp] = ImGuiKey.PageUp;
        BlissKeyMap[KeyboardKey.PageDown] = ImGuiKey.PageDown;
        BlissKeyMap[KeyboardKey.Home] = ImGuiKey.Home;
        BlissKeyMap[KeyboardKey.End] = ImGuiKey.End;
        BlissKeyMap[KeyboardKey.CapsLock] = ImGuiKey.CapsLock;
        BlissKeyMap[KeyboardKey.ScrollLock] = ImGuiKey.ScrollLock;
        BlissKeyMap[KeyboardKey.NumLock] = ImGuiKey.NumLock;
        BlissKeyMap[KeyboardKey.PrintScreen] = ImGuiKey.PrintScreen;
        BlissKeyMap[KeyboardKey.Pause] = ImGuiKey.Pause;
        BlissKeyMap[KeyboardKey.F1] = ImGuiKey.F1;
        BlissKeyMap[KeyboardKey.F2] = ImGuiKey.F2;
        BlissKeyMap[KeyboardKey.F3] = ImGuiKey.F3;
        BlissKeyMap[KeyboardKey.F4] = ImGuiKey.F4;
        BlissKeyMap[KeyboardKey.F5] = ImGuiKey.F5;
        BlissKeyMap[KeyboardKey.F6] = ImGuiKey.F6;
        BlissKeyMap[KeyboardKey.F7] = ImGuiKey.F7;
        BlissKeyMap[KeyboardKey.F8] = ImGuiKey.F8;
        BlissKeyMap[KeyboardKey.F9] = ImGuiKey.F9;
        BlissKeyMap[KeyboardKey.F10] = ImGuiKey.F10;
        BlissKeyMap[KeyboardKey.F11] = ImGuiKey.F11;
        BlissKeyMap[KeyboardKey.F12] = ImGuiKey.F12;
        BlissKeyMap[KeyboardKey.ShiftLeft] = ImGuiKey.LeftShift;
        BlissKeyMap[KeyboardKey.ControlLeft] = ImGuiKey.LeftCtrl;
        BlissKeyMap[KeyboardKey.AltLeft] = ImGuiKey.LeftAlt;
        BlissKeyMap[KeyboardKey.WinLeft] = ImGuiKey.LeftSuper;
        BlissKeyMap[KeyboardKey.ShiftRight] = ImGuiKey.RightShift;
        BlissKeyMap[KeyboardKey.ControlRight] = ImGuiKey.RightCtrl;
        BlissKeyMap[KeyboardKey.AltRight] = ImGuiKey.RightAlt;
        BlissKeyMap[KeyboardKey.WinRight] = ImGuiKey.RightSuper;
        BlissKeyMap[KeyboardKey.Menu] = ImGuiKey.Menu;
        BlissKeyMap[KeyboardKey.BracketLeft] = ImGuiKey.LeftBracket;
        BlissKeyMap[KeyboardKey.BackSlash] = ImGuiKey.Backslash;
        BlissKeyMap[KeyboardKey.BracketRight] = ImGuiKey.RightBracket;
        BlissKeyMap[KeyboardKey.Grave] = ImGuiKey.GraveAccent;
        BlissKeyMap[KeyboardKey.Keypad0] = ImGuiKey.Keypad0;
        BlissKeyMap[KeyboardKey.Keypad1] = ImGuiKey.Keypad1;
        BlissKeyMap[KeyboardKey.Keypad2] = ImGuiKey.Keypad2;
        BlissKeyMap[KeyboardKey.Keypad3] = ImGuiKey.Keypad3;
        BlissKeyMap[KeyboardKey.Keypad4] = ImGuiKey.Keypad4;
        BlissKeyMap[KeyboardKey.Keypad5] = ImGuiKey.Keypad5;
        BlissKeyMap[KeyboardKey.Keypad6] = ImGuiKey.Keypad6;
        BlissKeyMap[KeyboardKey.Keypad7] = ImGuiKey.Keypad7;
        BlissKeyMap[KeyboardKey.Keypad8] = ImGuiKey.Keypad8;
        BlissKeyMap[KeyboardKey.Keypad9] = ImGuiKey.Keypad9;
        BlissKeyMap[KeyboardKey.KeypadDecimal] = ImGuiKey.KeypadDecimal;
        BlissKeyMap[KeyboardKey.KeypadDivide] = ImGuiKey.KeypadDivide;
        BlissKeyMap[KeyboardKey.KeypadMultiply] = ImGuiKey.KeypadMultiply;
        BlissKeyMap[KeyboardKey.KeypadMinus] = ImGuiKey.KeypadSubtract;
        BlissKeyMap[KeyboardKey.KeypadPlus] = ImGuiKey.KeypadAdd;
        BlissKeyMap[KeyboardKey.KeypadEnter] = ImGuiKey.KeypadEnter;
        // BlissKeyMap[KeyboardKey.] = ImGuiKey.KeypadEqual;
    }

    private static void SetupMouseCursors()
    {
        mouseCursorMap.Clear();
        mouseCursorMap[ImGuiMouseCursor.Arrow] = SystemCursor.Pointer;
        mouseCursorMap[ImGuiMouseCursor.TextInput] = SystemCursor.Text;
        mouseCursorMap[ImGuiMouseCursor.Hand] = SystemCursor.NotAllowed;
        mouseCursorMap[ImGuiMouseCursor.ResizeAll] = SystemCursor.Move;
        mouseCursorMap[ImGuiMouseCursor.ResizeEw] = SystemCursor.EWResize;
        mouseCursorMap[ImGuiMouseCursor.ResizeNesw] = SystemCursor.NESWResize;
        mouseCursorMap[ImGuiMouseCursor.ResizeNs] = SystemCursor.NSResize;
        mouseCursorMap[ImGuiMouseCursor.ResizeNwse] = SystemCursor.NWSEResize;
        mouseCursorMap[ImGuiMouseCursor.NotAllowed] = SystemCursor.NotAllowed;
    }

    /// <summary>
    /// Forces the font texture atlas to be recomputed and re-cached
    /// </summary>
    public static unsafe void ReloadFonts()
    {
        CopperImGui.SetCurrentContext(imGuiContext);
        var io = ImGui.GetIO();

        byte* pixels;
        int width;
        int height;

        ImGui.GetTexDataAsRGBA32(io.Fonts, &pixels, &width, &height, null);


        // TODO: Add font support - waiting for Bliss version with full image support        
        // new Texture2D(BlissRenderer.Device);

        // fontTexture = binding.LoadFontTexture(new IntPtr(pixels), new Vector2(width, height));

        // io.Fonts.SetTexID(new ImTextureID(fontTexture.Id));
    }

    // ReSharper disable once InconsistentNaming
    private static unsafe sbyte* GetClipboardText(IntPtr userData)
    {
        var bytes = Encoding.ASCII.GetBytes(Input.GetClipboardText());

        fixed (byte* p = bytes)
            return (sbyte*)p;
    }

    // ReSharper disable once InconsistentNaming
    private static unsafe void SetClipboardText(IntPtr userData, sbyte* text)
    {
        try
        {
            Input.SetClipboardText(text->ToString());
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

        CopperImGui.SetCurrentContext(imGuiContext);

        CopperImGui.LoadFonts();

        var io = ImGui.GetIO();
        io.BackendFlags |= ImGuiBackendFlags.HasMouseCursors | ImGuiBackendFlags.HasSetMousePos | ImGuiBackendFlags.HasGamepad;

        io.MousePos.X = 0;
        io.MousePos.Y = 0;

        var platformIO = ImGui.GetPlatformIO();

        // copy/paste callbacks
        unsafe
        {
            setClipCallback = SetClipboardText;
            platformIO.PlatformSetClipboardTextFn = (void*)Marshal.GetFunctionPointerForDelegate(setClipCallback);

            getClipCallback = GetClipboardText;
            platformIO.PlatformGetClipboardTextFn = (void*)Marshal.GetFunctionPointerForDelegate(getClipCallback);

            platformIO.PlatformClipboardUserData = null;
        }


        ReloadFonts();
    }

    private static void SetMouseEvent(ImGuiIOPtr io, MouseButton mouseButton, ImGuiMouseButton imGuiMouse)
    {
        if (Input.IsMouseButtonPressed(mouseButton))
            io.AddMouseButtonEvent((int)imGuiMouse, true);
        else if (Input.IsMouseButtonReleased(mouseButton))
            io.AddMouseButtonEvent((int)imGuiMouse, false);
    }

    private static void NewFrame(float dt = -1)
    {
        var io = ImGui.GetIO();

        if (BlissRenderer.Window.GetState().HasFlag(WindowState.FullScreen) && false)
        {
            // TODO: See if theres a way to get monitor size w/ bliss
            // var monitor = binding.WindowGetCurrentMonitor();
            // io.DisplaySize = new Vector2(binding.WindowGetMonitorWidth(monitor), binding.WindowGetMonitorHeight(monitor));
        }
        else
        {
            io.DisplaySize = new Vector2(BlissRenderer.Window.GetSize().Item1, BlissRenderer.Window.GetSize().Item2);
        }

        io.DisplayFramebufferScale = new Vector2(1, 1);
        
        // TODO: See if theres a way to do high dpi stuff w/ bliss
        // if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || binding.WindowIsState(ConfigFlags.HighDpiWindow))
        //     io.DisplayFramebufferScale = binding.WindowGetScaleDPI();

        io.DeltaTime = dt >= 0 ? dt : (float)BlissRenderer.DeltaTime;
        

        if (io.WantSetMousePos)
        {
            Input.SetMousePosition(io.MousePos);
        }
        else
        {
            io.AddMousePosEvent(Input.GetMousePosition().X, Input.GetMousePosition().Y);
        }

        SetMouseEvent(io, MouseButton.Left, ImGuiMouseButton.Left);
        SetMouseEvent(io, MouseButton.Right, ImGuiMouseButton.Right);
        SetMouseEvent(io, MouseButton.Middle, ImGuiMouseButton.Middle);
        // SetMouseEvent(io, MouseButton.Forward, ImGuiMouseButton.Middle + 1); // TODO
        // SetMouseEvent(io, MouseButton.Back, ImGuiMouseButton.Middle + 2); // TODO

        var wheelMove = Input.GetMouseDelta();
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
                binding.InputSetMouseCursor(mouseCursorMap.GetValueOrDefault(imguiCursor, SystemCursor.Default));
        }
    }

    private static void FrameEvents()
    {
        var io = ImGui.GetIO();

        var focused = BlissRenderer.Window.IsFocused;
        if (focused != lastFrameFocused)
            io.AddFocusEvent(focused);
        lastFrameFocused = focused;


        // handle the modifier key events so that shortcuts work
        var ctrlDown = IsControlDown();
        if (ctrlDown != lastControlPressed)
            io.AddKeyEvent(ImGuiKey.ModCtrl, ctrlDown);
        lastControlPressed = ctrlDown;

        var shiftDown = IsShiftDown();
        if (shiftDown != lastShiftPressed)
            io.AddKeyEvent(ImGuiKey.ModShift, shiftDown);
        lastShiftPressed = shiftDown;

        var altDown = IsAltDown();
        if (altDown != lastAltPressed)
            io.AddKeyEvent(ImGuiKey.ModAlt, altDown);
        lastAltPressed = altDown;

        var superDown = IsSuperDown();
        if (superDown != lastSuperPressed)
            io.AddKeyEvent(ImGuiKey.ModSuper, superDown);
        lastSuperPressed = superDown;
        
        // get the pressed keys, they are in event order
        var keyId = binding.InputGetKeyPressed();
        while (keyId != 0)
        {
            var key = keyId;
            if (BlissKeyMap.TryGetValue(key, out var value))
                io.AddKeyEvent(value, true);
            keyId = binding.InputGetKeyPressed();
        }

        // look for any keys that were down last frame and see if they were down and are released
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (var keyItr in BlissKeyMap)
        {
            if (binding.InputIsKeyReleased(keyItr.Key))
                io.AddKeyEvent(keyItr.Value, false);
        }

        // add the text input in order
        var pressed = binding.InputGetCharPressed();
        while (pressed != 0)
        {
            io.AddInputCharacter((uint)pressed);
            pressed = binding.InputGetCharPressed();
        }

        // gamepads
        if ((io.ConfigFlags & ImGuiConfigFlags.NavEnableGamepad) == 0 || !binding.InputIsGamepadAvailable(0))
            return;

        // TODO: make sure gamepad controls are correct
        HandleGamepadButtonEvent(io, GamepadButton.DpadUp, ImGuiKey.GamepadDpadUp);
        HandleGamepadButtonEvent(io, GamepadButton.DpadRight, ImGuiKey.GamepadDpadRight);
        HandleGamepadButtonEvent(io, GamepadButton.DpadDown, ImGuiKey.GamepadDpadDown);
        HandleGamepadButtonEvent(io, GamepadButton.DpadLeft, ImGuiKey.GamepadDpadLeft);

        HandleGamepadButtonEvent(io, GamepadButton.RightFaceUp, ImGuiKey.GamepadFaceUp);
        HandleGamepadButtonEvent(io, GamepadButton.RightFaceRight, ImGuiKey.GamepadFaceLeft);
        HandleGamepadButtonEvent(io, GamepadButton.RightFaceDown, ImGuiKey.GamepadFaceDown);
        HandleGamepadButtonEvent(io, GamepadButton.RightFaceLeft, ImGuiKey.GamepadFaceRight);

        HandleGamepadButtonEvent(io, GamepadButton.LeftShoulder, ImGuiKey.GamepadL1);
        HandleGamepadButtonEvent(io, GamepadButton.LeftPaddle1, ImGuiKey.GamepadL2);
        HandleGamepadButtonEvent(io, GamepadButton.RightTrigger1, ImGuiKey.GamepadR1);
        HandleGamepadButtonEvent(io, GamepadButton.RightTrigger2, ImGuiKey.GamepadR2);
        HandleGamepadButtonEvent(io, GamepadButton.LeftPaddle2, ImGuiKey.GamepadL3);
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
        if (binding.InputIsGamepadButtonPressed(0, button))
            io.AddKeyEvent(key, true);
        else if (binding.InputIsGamepadButtonReleased(0, button))
            io.AddKeyEvent(key, false);
    }

    private static void HandleGamepadStickEvent(ImGuiIOPtr io, GamepadAxis axis, ImGuiKey negKey, ImGuiKey posKey)
    {
        const float deadZone = 0.20f;

        var axisValue = binding.InputGetGamepadAxisMovement(0, axis);

        io.AddKeyAnalogEvent(negKey, axisValue < -deadZone, axisValue < -deadZone ? -axisValue : 0);
        io.AddKeyAnalogEvent(posKey, axisValue > deadZone, axisValue > deadZone ? axisValue : 0);
    }

    /// <summary>
    /// Starts a new ImGui Frame
    /// </summary>
    /// <param name="dt">optional delta time, any value less than 0 will use raylib GetFrameTime</param>
    public static void Begin(float dt = -1)
    {
        CopperImGui.SetCurrentContext(imGuiContext);

        NewFrame(dt);
        FrameEvents();
        ImGui.NewFrame();
    }

    private static void EnableScissor(float x, float y, float width, float height)
    {
        binding.RlGlEnableScissorTest();
        var io = ImGui.GetIO();

        var scale = new Vector2(1.0f, 1.0f);
        // if (binding.WindowIsState(ConfigFlags.HighDpiWindow) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        //     scale = io.DisplayFramebufferScale;

        binding.RlGlScissor((int)(x * scale.X), (int)((io.DisplaySize.Y - (int)(y + height)) * scale.Y), (int)(width * scale.X), (int)(height * scale.Y));
    }

    private static void TriangleVert(ImDrawVert idxVert)
    {
        var color = ImGui.ColorConvertU32ToFloat4(idxVert.Col);

        binding.RlGlColor4F(color.X, color.Y, color.Z, color.W);
        binding.RlGlTexCoord2F(idxVert.Uv.X, idxVert.Uv.Y);
        binding.RlGlVertex2F(idxVert.Pos.X, idxVert.Pos.Y);
    }

    private static void RenderTriangles(uint count, uint indexStart, ImVector<ushort> indexBuffer, ImVector<ImDrawVert> vertBuffer, ImTextureID texturePtr)
    {
        if (count < 3)
            return;

        uint textureId = 0;
        if (texturePtr != IntPtr.Zero)
            textureId = Convert.ToUInt32(texturePtr.Handle);

        binding.RlGlBegin(4);
        binding.RlGlSetTexture(textureId);

        for (var i = 0; i <= (count - 3); i += 3)
        {
            if (binding.RlGlCheckRenderBatchLimit(3))
            {
                binding.RlGlBegin(4);
                binding.RlGlSetTexture(textureId);
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

        binding.RlGlEnd();
    }

    private delegate void Callback(ImDrawListPtr list, ImDrawCmdPtr cmd);

    private static unsafe void RenderData()
    {
        binding.RlGlDrawRenderBatchActive();
        binding.RlGlDisableBackfaceCulling();

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

                if (cmd.UserCallback != null)
                {
                    var cb = Marshal.GetDelegateForFunctionPointer<Callback>(new IntPtr(cmd.UserCallback));
                    cb(commandList, new ImDrawCmdPtr(&cmd));
                    continue;
                }

                RenderTriangles(cmd.ElemCount, cmd.IdxOffset, commandList.IdxBuffer, commandList.VtxBuffer, cmd.TextureId);

                binding.RlGlDrawRenderBatchActive();
            }
        }

        binding.RlGlSetTexture(0);
        binding.RlGlDisableScissorTest();
        binding.RlGlEnableBackfaceCulling();
    }

    /// <summary>
    /// Ends an ImGui frame and submits all ImGui drawing to raylib for processing.
    /// </summary>
    public static void End()
    {
        CopperImGui.SetCurrentContext(imGuiContext);
        ImGui.Render();
        ImGui.EndFrame();
        RenderData();

        if (ImGui.GetIO().ConfigFlags.HasFlag(ImGuiConfigFlags.ViewportsEnable))
        {
            ImGui.UpdatePlatformWindows();
            ImGui.RenderPlatformWindowsDefault();
        }
    }

    /// <summary>
    /// Cleanup ImGui and unload font atlas
    /// </summary>
    public static void Shutdown()
    {
        fontTexture.Dispose();
        CopperImGui.DestroyContext(imGuiContext);
    }
}