// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Drawing;
using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Input.Extensions;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui.Renderer.OpenGl.SilkNet.Internal;

public class ImGuiController : IDisposable
{
    private GL gl = null!;
    private IView view = null!;
    private IInputContext input = null!;
    private bool frameBegun;
    private readonly List<char> pressedChars = [];
    private IKeyboard keyboard = null!;

    private int attribLocationTex;
    private int attribLocationProjMtx;
    private int attribLocationVtxPos;
    private int attribLocationVtxUv;
    private int attribLocationVtxColor;
    private uint vboHandle;
    private uint elementsHandle;
    private uint vertexArrayObject;

    private Texture fontTexture = null!;
    private Shader shader = null!;

    private int windowWidth;
    private int windowHeight;

    /// <summary>
    /// Constructs a new ImGuiController
    /// </summary>
    public ImGuiController(GL gl, IView view, IInputContext input)
    {
        Init(gl, view, input);

        var io = ImGui.GetIO();

        CopperImGui.LoadFonts();

        io.BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;

        CreateDeviceResources();

        SetPerFrameImGuiData(1f / 60f);

        BeginFrame();
    }

    private void Init(GL newGl, IView newView, IInputContext newInput)
    {
        gl = newGl;
        view = newView;
        input = newInput;
        windowWidth = newView.Size.X;
        windowHeight = newView.Size.Y;

        CopperImGui.CreateContext(true);
    }

    private void BeginFrame()
    {
        ImGui.NewFrame();
        frameBegun = true;
        keyboard = input.Keyboards[0];
        view.Resize += WindowResized;
        keyboard.KeyDown += OnKeyDown;
        keyboard.KeyUp += OnKeyUp;
        keyboard.KeyChar += OnKeyChar;
    }

    /// <summary>
    /// Delegate to receive keyboard key down events.
    /// </summary>
    /// <param name="keyboard">The keyboard context generating the event.</param>
    /// <param name="keycode">The native keycode of the pressed key.</param>
    /// <param name="scancode">The native scancode of the pressed key.</param>
    private static void OnKeyDown(IKeyboard keyboard, Key keycode, int scancode)
    {
        OnKeyEvent(keyboard, keycode, scancode, down: true);
    }

    /// <summary>
    /// Delegate to receive keyboard key up events.
    /// </summary>
    /// <param name="keyboard">The keyboard context generating the event.</param>
    /// <param name="keycode">The native keycode of the released key.</param>
    /// <param name="scancode">The native scancode of the released key.</param>
    private static void OnKeyUp(IKeyboard keyboard, Key keycode, int scancode)
    {
        OnKeyEvent(keyboard, keycode, scancode, down: false);
    }

    /// <summary>
    /// Delegate to receive keyboard key events.
    /// </summary>
    /// <param name="keyboard">The keyboard context generating the event.</param>
    /// <param name="keycode">The native keycode of the key generating the event.</param>
    /// <param name="scancode">The native scancode of the key generating the event.</param>
    /// <param name="down">True if the event is a key down event, otherwise False</param>
    private static void OnKeyEvent(IKeyboard keyboard, Key keycode, int scancode, bool down)
    {
        var io = ImGui.GetIO();
        var imGuiKey = TranslateInputKeyToImGuiKey(keycode);
        io.AddKeyEvent(imGuiKey, down);
        io.SetKeyEventNativeData(imGuiKey, (int)keycode, scancode);
    }

    private void OnKeyChar(IKeyboard arg1, char arg2)
    {
        pressedChars.Add(arg2);
    }

    private void WindowResized(Vector2D<int> size)
    {
        windowWidth = size.X;
        windowHeight = size.Y;
    }

    /// <summary>
    /// Renders the ImGui draw list data.
    /// This method requires a <see cref="GraphicsDevice"/> because it may create new DeviceBuffers if the size of vertex
    /// or index data has increased beyond the capacity of the existing buffers.
    /// A <see cref="CommandList"/> is needed to submit drawing and resource update commands.
    /// </summary>
    public void Render()
    {
        if (!frameBegun)
            return;

        var oldCtx = ImGui.GetCurrentContext();

        if (oldCtx != CopperImGui.GetCurrentContext())
        {
            CopperImGui.SetCurrentContext(CopperImGui.GetCurrentContext());
        }

        frameBegun = false;
        ImGui.Render();
        RenderImDrawData(ImGui.GetDrawData());

        if (oldCtx != CopperImGui.GetCurrentContext())
        {
            CopperImGui.SetCurrentContext(oldCtx);
        }
    }

    /// <summary>
    /// Updates ImGui input and IO configuration state.
    /// </summary>
    public void Update(float deltaSeconds)
    {
        var oldCtx = ImGui.GetCurrentContext();

        if (oldCtx != CopperImGui.GetCurrentContext())
        {
            ImGui.SetCurrentContext(CopperImGui.GetCurrentContext());
        }

        if (frameBegun)
        {
            ImGui.Render();
        }

        SetPerFrameImGuiData(deltaSeconds);
        UpdateImGuiInput();

        frameBegun = true;
        ImGui.NewFrame();

        if (oldCtx != CopperImGui.GetCurrentContext())
        {
            ImGui.SetCurrentContext(oldCtx);
        }
    }

    /// <summary>
    /// Sets per-frame data based on the associated window.
    /// This is called by Update(float).
    /// </summary>
    private void SetPerFrameImGuiData(float deltaSeconds)
    {
        var io = ImGui.GetIO();
        io.DisplaySize = new Vector2(windowWidth, windowHeight);

        if (windowWidth > 0 && windowHeight > 0)
        {
            io.DisplayFramebufferScale = new Vector2((float)view.FramebufferSize.X / windowWidth, (float)view.FramebufferSize.Y / windowHeight);
        }

        io.DeltaTime = deltaSeconds; // DeltaTime is in seconds.
    }

    private void UpdateImGuiInput()
    {
        var io = ImGui.GetIO();

        using var mouseState = input.Mice[0].CaptureState();

        io.MouseDown[0] = mouseState.IsButtonPressed(MouseButton.Left);
        io.MouseDown[1] = mouseState.IsButtonPressed(MouseButton.Right);
        io.MouseDown[2] = mouseState.IsButtonPressed(MouseButton.Middle);

        var point = new Point((int)mouseState.Position.X, (int)mouseState.Position.Y);
        io.MousePos = new Vector2(point.X, point.Y);

        var wheel = mouseState.GetScrollWheels()[0];
        io.MouseWheel = wheel.Y;
        io.MouseWheelH = wheel.X;

        foreach (var c in pressedChars)
        {
            io.AddInputCharacter(c);
        }

        pressedChars.Clear();

        io.KeyCtrl = keyboard.IsKeyPressed(Key.ControlLeft) || keyboard.IsKeyPressed(Key.ControlRight);
        io.KeyAlt = keyboard.IsKeyPressed(Key.AltLeft) || keyboard.IsKeyPressed(Key.AltRight);
        io.KeyShift = keyboard.IsKeyPressed(Key.ShiftLeft) || keyboard.IsKeyPressed(Key.ShiftRight);
        io.KeySuper = keyboard.IsKeyPressed(Key.SuperLeft) || keyboard.IsKeyPressed(Key.SuperRight);
    }

    internal void PressChar(char keyChar)
    {
        pressedChars.Add(keyChar);
    }

    /// <summary>
    /// Translates a Silk.NET.Input.Key to an ImGuiKey.
    /// </summary>
    /// <param name="key">The Silk.NET.Input.Key to translate.</param>
    /// <returns>The corresponding ImGuiKey.</returns>
    /// <exception cref="NotImplementedException">When the key has not been implemented yet.</exception>
    private static ImGuiKey TranslateInputKeyToImGuiKey(Key key)
    {
        return key switch
        {
            Key.Tab => ImGuiKey.Tab,
            Key.Left => ImGuiKey.LeftArrow,
            Key.Right => ImGuiKey.RightArrow,
            Key.Up => ImGuiKey.UpArrow,
            Key.Down => ImGuiKey.DownArrow,
            Key.PageUp => ImGuiKey.PageUp,
            Key.PageDown => ImGuiKey.PageDown,
            Key.Home => ImGuiKey.Home,
            Key.End => ImGuiKey.End,
            Key.Insert => ImGuiKey.Insert,
            Key.Delete => ImGuiKey.Delete,
            Key.Backspace => ImGuiKey.Backspace,
            Key.Space => ImGuiKey.Space,
            Key.Enter => ImGuiKey.Enter,
            Key.Escape => ImGuiKey.Escape,
            Key.Apostrophe => ImGuiKey.Apostrophe,
            Key.Comma => ImGuiKey.Comma,
            Key.Minus => ImGuiKey.Minus,
            Key.Period => ImGuiKey.Period,
            Key.Slash => ImGuiKey.Slash,
            Key.Semicolon => ImGuiKey.Semicolon,
            Key.Equal => ImGuiKey.Equal,
            Key.LeftBracket => ImGuiKey.LeftBracket,
            Key.BackSlash => ImGuiKey.Backslash,
            Key.RightBracket => ImGuiKey.RightBracket,
            Key.GraveAccent => ImGuiKey.GraveAccent,
            Key.CapsLock => ImGuiKey.CapsLock,
            Key.ScrollLock => ImGuiKey.ScrollLock,
            Key.NumLock => ImGuiKey.NumLock,
            Key.PrintScreen => ImGuiKey.PrintScreen,
            Key.Pause => ImGuiKey.Pause,
            Key.Keypad0 => ImGuiKey.Keypad0,
            Key.Keypad1 => ImGuiKey.Keypad1,
            Key.Keypad2 => ImGuiKey.Keypad2,
            Key.Keypad3 => ImGuiKey.Keypad3,
            Key.Keypad4 => ImGuiKey.Keypad4,
            Key.Keypad5 => ImGuiKey.Keypad5,
            Key.Keypad6 => ImGuiKey.Keypad6,
            Key.Keypad7 => ImGuiKey.Keypad7,
            Key.Keypad8 => ImGuiKey.Keypad8,
            Key.Keypad9 => ImGuiKey.Keypad9,
            Key.KeypadDecimal => ImGuiKey.KeypadDecimal,
            Key.KeypadDivide => ImGuiKey.KeypadDivide,
            Key.KeypadMultiply => ImGuiKey.KeypadMultiply,
            Key.KeypadSubtract => ImGuiKey.KeypadSubtract,
            Key.KeypadAdd => ImGuiKey.KeypadAdd,
            Key.KeypadEnter => ImGuiKey.KeypadEnter,
            Key.KeypadEqual => ImGuiKey.KeypadEqual,
            Key.ShiftLeft => ImGuiKey.LeftShift,
            Key.ControlLeft => ImGuiKey.LeftCtrl,
            Key.AltLeft => ImGuiKey.LeftAlt,
            Key.SuperLeft => ImGuiKey.LeftSuper,
            Key.ShiftRight => ImGuiKey.RightShift,
            Key.ControlRight => ImGuiKey.RightCtrl,
            Key.AltRight => ImGuiKey.RightAlt,
            Key.SuperRight => ImGuiKey.RightSuper,
            Key.Menu => ImGuiKey.Menu,
            Key.Number0 => ImGuiKey.Key0,
            Key.Number1 => ImGuiKey.Key1,
            Key.Number2 => ImGuiKey.Key2,
            Key.Number3 => ImGuiKey.Key3,
            Key.Number4 => ImGuiKey.Key4,
            Key.Number5 => ImGuiKey.Key5,
            Key.Number6 => ImGuiKey.Key6,
            Key.Number7 => ImGuiKey.Key7,
            Key.Number8 => ImGuiKey.Key8,
            Key.Number9 => ImGuiKey.Key9,
            Key.A => ImGuiKey.A,
            Key.B => ImGuiKey.B,
            Key.C => ImGuiKey.C,
            Key.D => ImGuiKey.D,
            Key.E => ImGuiKey.E,
            Key.F => ImGuiKey.F,
            Key.G => ImGuiKey.G,
            Key.H => ImGuiKey.H,
            Key.I => ImGuiKey.I,
            Key.J => ImGuiKey.J,
            Key.K => ImGuiKey.K,
            Key.L => ImGuiKey.L,
            Key.M => ImGuiKey.M,
            Key.N => ImGuiKey.N,
            Key.O => ImGuiKey.O,
            Key.P => ImGuiKey.P,
            Key.Q => ImGuiKey.Q,
            Key.R => ImGuiKey.R,
            Key.S => ImGuiKey.S,
            Key.T => ImGuiKey.T,
            Key.U => ImGuiKey.U,
            Key.V => ImGuiKey.V,
            Key.W => ImGuiKey.W,
            Key.X => ImGuiKey.X,
            Key.Y => ImGuiKey.Y,
            Key.Z => ImGuiKey.Z,
            Key.F1 => ImGuiKey.F1,
            Key.F2 => ImGuiKey.F2,
            Key.F3 => ImGuiKey.F3,
            Key.F4 => ImGuiKey.F4,
            Key.F5 => ImGuiKey.F5,
            Key.F6 => ImGuiKey.F6,
            Key.F7 => ImGuiKey.F7,
            Key.F8 => ImGuiKey.F8,
            Key.F9 => ImGuiKey.F9,
            Key.F10 => ImGuiKey.F10,
            Key.F11 => ImGuiKey.F11,
            Key.F12 => ImGuiKey.F12,
            Key.F13 => ImGuiKey.F13,
            Key.F14 => ImGuiKey.F14,
            Key.F15 => ImGuiKey.F15,
            Key.F16 => ImGuiKey.F16,
            Key.F17 => ImGuiKey.F17,
            Key.F18 => ImGuiKey.F18,
            Key.F19 => ImGuiKey.F19,
            Key.F20 => ImGuiKey.F20,
            Key.F21 => ImGuiKey.F21,
            Key.F22 => ImGuiKey.F22,
            Key.F23 => ImGuiKey.F23,
            Key.F24 => ImGuiKey.F24,
            _ => throw new NotImplementedException(),
        };
    }

    private unsafe void SetupRenderState(ImDrawDataPtr drawDataPtr, int framebufferWidth, int framebufferHeight)
    {
        // Setup render state: alpha-blending enabled, no face culling, no depth testing, scissor enabled, polygon fill
        gl.Enable(GLEnum.Blend);
        gl.BlendEquation(GLEnum.FuncAdd);
        gl.BlendFuncSeparate(GLEnum.SrcAlpha, GLEnum.OneMinusSrcAlpha, GLEnum.One, GLEnum.OneMinusSrcAlpha);
        gl.Disable(GLEnum.CullFace);
        gl.Disable(GLEnum.DepthTest);
        gl.Disable(GLEnum.StencilTest);
        gl.Enable(GLEnum.ScissorTest);
        gl.Disable(GLEnum.PrimitiveRestart);
        gl.PolygonMode(GLEnum.FrontAndBack, GLEnum.Fill);

        var l = drawDataPtr.DisplayPos.X;
        var r = drawDataPtr.DisplayPos.X + drawDataPtr.DisplaySize.X;
        var T = drawDataPtr.DisplayPos.Y;
        var b = drawDataPtr.DisplayPos.Y + drawDataPtr.DisplaySize.Y;

        Span<float> orthoProjection =
        [
            2.0f / (r - l), 0.0f, 0.0f, 0.0f,
            0.0f, 2.0f / (T - b), 0.0f, 0.0f,
            0.0f, 0.0f, -1.0f, 0.0f,
            (r + l) / (l - r), (T + b) / (b - T), 0.0f, 1.0f
        ];

        shader.UseShader();
        gl.Uniform1(attribLocationTex, 0);
        gl.UniformMatrix4(attribLocationProjMtx, 1, false, orthoProjection);
        gl.CheckGlError("Projection");

        gl.BindSampler(0, 0);

        // Setup desired GL state
        // Recreate the VAO every time (this is to easily allow multiple GL contexts to be rendered to. VAO are not shared among GL contexts)
        // The renderer would actually work without any VAO bound, but then our VertexAttrib calls would overwrite the default one currently bound.
        vertexArrayObject = gl.GenVertexArray();
        gl.BindVertexArray(vertexArrayObject);
        gl.CheckGlError("VAO");

        // Bind vertex/index buffers and setup attributes for ImDrawVert
        gl.BindBuffer(GLEnum.ArrayBuffer, vboHandle);
        gl.BindBuffer(GLEnum.ElementArrayBuffer, elementsHandle);
        gl.EnableVertexAttribArray((uint)attribLocationVtxPos);
        gl.EnableVertexAttribArray((uint)attribLocationVtxUv);
        gl.EnableVertexAttribArray((uint)attribLocationVtxColor);
        gl.VertexAttribPointer((uint)attribLocationVtxPos, 2, GLEnum.Float, false, (uint)sizeof(ImDrawVert), (void*)0);
        gl.VertexAttribPointer((uint)attribLocationVtxUv, 2, GLEnum.Float, false, (uint)sizeof(ImDrawVert), (void*)8);
        gl.VertexAttribPointer((uint)attribLocationVtxColor, 4, GLEnum.UnsignedByte, true, (uint)sizeof(ImDrawVert), (void*)16);
    }

    private unsafe void RenderImDrawData(ImDrawDataPtr drawDataPtr)
    {
        var framebufferWidth = (int)(drawDataPtr.DisplaySize.X * drawDataPtr.FramebufferScale.X);
        var framebufferHeight = (int)(drawDataPtr.DisplaySize.Y * drawDataPtr.FramebufferScale.Y);
        if (framebufferWidth <= 0 || framebufferHeight <= 0)
            return;

        // Backup GL state
        gl.GetInteger(GLEnum.ActiveTexture, out var lastActiveTexture);
        gl.ActiveTexture(GLEnum.Texture0);

        gl.GetInteger(GLEnum.CurrentProgram, out var lastProgram);
        gl.GetInteger(GLEnum.TextureBinding2D, out var lastTexture);

        gl.GetInteger(GLEnum.SamplerBinding, out var lastSampler);

        gl.GetInteger(GLEnum.ArrayBufferBinding, out var lastArrayBuffer);
        gl.GetInteger(GLEnum.VertexArrayBinding, out var lastVertexArrayObject);

#if !GLES
        Span<int> lastPolygonMode = stackalloc int[2];
        gl.GetInteger(GLEnum.PolygonMode, lastPolygonMode);
#endif

        Span<int> lastScissorBox = stackalloc int[4];
        gl.GetInteger(GLEnum.ScissorBox, lastScissorBox);

        gl.GetInteger(GLEnum.BlendSrcRgb, out var lastBlendSrcRgb);
        gl.GetInteger(GLEnum.BlendDstRgb, out var lastBlendDstRgb);

        gl.GetInteger(GLEnum.BlendSrcAlpha, out var lastBlendSrcAlpha);
        gl.GetInteger(GLEnum.BlendDstAlpha, out var lastBlendDstAlpha);

        gl.GetInteger(GLEnum.BlendEquationRgb, out var lastBlendEquationRgb);
        gl.GetInteger(GLEnum.BlendEquationAlpha, out var lastBlendEquationAlpha);

        var lastEnableBlend = gl.IsEnabled(GLEnum.Blend);
        var lastEnableCullFace = gl.IsEnabled(GLEnum.CullFace);
        var lastEnableDepthTest = gl.IsEnabled(GLEnum.DepthTest);
        var lastEnableStencilTest = gl.IsEnabled(GLEnum.StencilTest);
        var lastEnableScissorTest = gl.IsEnabled(GLEnum.ScissorTest);

#if !GLES && !LEGACY
        var lastEnablePrimitiveRestart = gl.IsEnabled(GLEnum.PrimitiveRestart);
#endif

        SetupRenderState(drawDataPtr, framebufferWidth, framebufferHeight);

        // Will project scissor/clipping rectangles into framebuffer space
        var clipOff = drawDataPtr.DisplayPos; // (0,0) unless using multi-viewports
        var clipScale = drawDataPtr.FramebufferScale; // (1,1) unless using retina display which are often (2,2)

        // Render command lists
        for (var n = 0; n < drawDataPtr.CmdListsCount; n++)
        {
            var cmdListPtr = drawDataPtr.CmdLists[n];

            // Upload vertex/index buffers

            gl.BufferData(GLEnum.ArrayBuffer, (nuint)(cmdListPtr.VtxBuffer.Size * sizeof(ImDrawVert)), (void*)cmdListPtr.VtxBuffer.Data, GLEnum.StreamDraw);
            gl.CheckGlError($"Data Vert {n}");
            gl.BufferData(GLEnum.ElementArrayBuffer, (nuint)(cmdListPtr.IdxBuffer.Size * sizeof(ushort)), (void*)cmdListPtr.IdxBuffer.Data, GLEnum.StreamDraw);
            gl.CheckGlError($"Data Idx {n}");

            for (var cmdI = 0; cmdI < cmdListPtr.CmdBuffer.Size; cmdI++)
            {
                var cmdPtr = cmdListPtr.CmdBuffer[cmdI];

                if (cmdPtr.UserCallback != null)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    Vector4 clipRect;
                    clipRect.X = (cmdPtr.ClipRect.X - clipOff.X) * clipScale.X;
                    clipRect.Y = (cmdPtr.ClipRect.Y - clipOff.Y) * clipScale.Y;
                    clipRect.Z = (cmdPtr.ClipRect.Z - clipOff.X) * clipScale.X;
                    clipRect.W = (cmdPtr.ClipRect.W - clipOff.Y) * clipScale.Y;

                    if (!(clipRect.X < framebufferWidth) || !(clipRect.Y < framebufferHeight) || clipRect is not { Z: >= 0.0f, W: >= 0.0f })
                        continue;

                    // Apply scissor/clipping rectangle
                    gl.Scissor((int)clipRect.X, (int)(framebufferHeight - clipRect.W), (uint)(clipRect.Z - clipRect.X), (uint)(clipRect.W - clipRect.Y));
                    gl.CheckGlError("Scissor");

                    // Bind texture, Draw
                    gl.BindTexture(GLEnum.Texture2D, (uint)cmdPtr.TextureId.Handle);
                    gl.CheckGlError("Texture");

                    gl.DrawElementsBaseVertex(GLEnum.Triangles, cmdPtr.ElemCount, GLEnum.UnsignedShort, (void*)(cmdPtr.IdxOffset * sizeof(ushort)), (int)cmdPtr.VtxOffset);
                    gl.CheckGlError("Draw");
                }
            }
        }

        // Destroy the temporary VAO
        gl.DeleteVertexArray(vertexArrayObject);
        vertexArrayObject = 0;

        // Restore modified GL state
        gl.UseProgram((uint)lastProgram);
        gl.BindTexture(GLEnum.Texture2D, (uint)lastTexture);

        gl.BindSampler(0, (uint)lastSampler);

        gl.ActiveTexture((GLEnum)lastActiveTexture);

        gl.BindVertexArray((uint)lastVertexArrayObject);

        gl.BindBuffer(GLEnum.ArrayBuffer, (uint)lastArrayBuffer);
        gl.BlendEquationSeparate((GLEnum)lastBlendEquationRgb, (GLEnum)lastBlendEquationAlpha);
        gl.BlendFuncSeparate((GLEnum)lastBlendSrcRgb, (GLEnum)lastBlendDstRgb, (GLEnum)lastBlendSrcAlpha, (GLEnum)lastBlendDstAlpha);

        if (lastEnableBlend)
        {
            gl.Enable(GLEnum.Blend);
        }
        else
        {
            gl.Disable(GLEnum.Blend);
        }

        if (lastEnableCullFace)
        {
            gl.Enable(GLEnum.CullFace);
        }
        else
        {
            gl.Disable(GLEnum.CullFace);
        }

        if (lastEnableDepthTest)
        {
            gl.Enable(GLEnum.DepthTest);
        }
        else
        {
            gl.Disable(GLEnum.DepthTest);
        }

        if (lastEnableStencilTest)
        {
            gl.Enable(GLEnum.StencilTest);
        }
        else
        {
            gl.Disable(GLEnum.StencilTest);
        }

        if (lastEnableScissorTest)
        {
            gl.Enable(GLEnum.ScissorTest);
        }
        else
        {
            gl.Disable(GLEnum.ScissorTest);
        }

        if (lastEnablePrimitiveRestart)
        {
            gl.Enable(GLEnum.PrimitiveRestart);
        }
        else
        {
            gl.Disable(GLEnum.PrimitiveRestart);
        }

        gl.PolygonMode(GLEnum.FrontAndBack, (GLEnum)lastPolygonMode[0]);

        gl.Scissor(lastScissorBox[0], lastScissorBox[1], (uint)lastScissorBox[2], (uint)lastScissorBox[3]);
    }

    private void CreateDeviceResources()
    {
        // Backup GL state

        gl.GetInteger(GLEnum.TextureBinding2D, out var lastTexture);
        gl.GetInteger(GLEnum.ArrayBufferBinding, out var lastArrayBuffer);
        gl.GetInteger(GLEnum.VertexArrayBinding, out var lastVertexArray);

        using var vertexStream = typeof(ImGuiController).Assembly.GetManifestResourceStream("CopperDevs.DearImGui.Renderer.OpenGl.SilkNet.Resources.shader.vert");
        using var vertexReader = new StreamReader(vertexStream!);
        var vertexResult = vertexReader.ReadToEnd();

        using var fragmentStream = typeof(ImGuiController).Assembly.GetManifestResourceStream("CopperDevs.DearImGui.Renderer.OpenGl.SilkNet.Resources.shader.frag");
        using var fragmentReader = new StreamReader(fragmentStream!);
        var fragmentResult = fragmentReader.ReadToEnd();

        shader = new Shader(gl, vertexResult, fragmentResult);

        attribLocationTex = shader.GetUniformLocation("Texture");
        attribLocationProjMtx = shader.GetUniformLocation("ProjMtx");
        attribLocationVtxPos = shader.GetAttribLocation("Position");
        attribLocationVtxUv = shader.GetAttribLocation("UV");
        attribLocationVtxColor = shader.GetAttribLocation("Color");

        vboHandle = gl.GenBuffer();
        elementsHandle = gl.GenBuffer();

        RecreateFontDeviceTexture();

        // Restore modified GL state
        gl.BindTexture(GLEnum.Texture2D, (uint)lastTexture);
        gl.BindBuffer(GLEnum.ArrayBuffer, (uint)lastArrayBuffer);

        gl.BindVertexArray((uint)lastVertexArray);

        gl.CheckGlError("End of ImGui setup");
    }

    /// <summary>
    /// Creates the texture used to render text.
    /// </summary>
    private unsafe void RecreateFontDeviceTexture()
    {
        byte* pixels;
        int width;
        int height;

        // Build texture atlas
        var io = ImGui.GetIO();
        ImGui.GetTexDataAsRGBA32(io.Fonts, &pixels, &width, &height, null);
        // Load as RGBA 32-bit (75% of the memory is wasted, but default font is so small) because it is more likely to be compatible with user's existing shaders. If your ImTextureId represent a higher-level concept than just a GL texture id, consider calling GetTexDataAsAlpha8() instead to save on GPU memory.

        // Upload texture to graphics system
        gl.GetInteger(GLEnum.TextureBinding2D, out var lastTexture);

        fontTexture = new Texture(gl, width, height, *pixels);
        fontTexture.Bind();
        fontTexture.SetMagFilter(TextureMagFilter.Linear);
        fontTexture.SetMinFilter(TextureMinFilter.Linear);

        // Store our identifier
        io.Fonts.SetTexID((IntPtr)fontTexture.GlTexture);

        // Restore state
        gl.BindTexture(GLEnum.Texture2D, (uint)lastTexture);
    }

    /// <summary>
    /// Frees all graphics resources used by the renderer.
    /// </summary>
    public void Dispose()
    {
        view.Resize -= WindowResized;
        keyboard.KeyChar -= OnKeyChar;

        gl.DeleteBuffer(vboHandle);
        gl.DeleteBuffer(elementsHandle);
        gl.DeleteVertexArray(vertexArrayObject);

        fontTexture.Dispose();
        shader.Dispose();

        CopperImGui.DestroyCurrentContext();
    }
}