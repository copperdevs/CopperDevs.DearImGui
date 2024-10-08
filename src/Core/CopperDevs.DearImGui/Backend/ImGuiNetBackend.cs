using CopperDevs.DearImGui.Backend.Enums;
using CopperDevs.Logger;
using ImGuiNET;

namespace CopperDevs.DearImGui.Backend;

internal class ImGuiNetBackend : IImGuiBackend
{
    public bool WantCaptureMouse => ImGui.GetIO().WantCaptureMouse;

    public bool IsWindowHovered(HoveredFlags flags)
    {
        return ImGui.IsWindowHovered((ImGuiHoveredFlags)flags);
    }

    public float GetWindowWidth()
    {
        return ImGui.GetWindowWidth();
    }

    public float GetWindowHeight()
    {
        return ImGui.GetWindowHeight();
    }

    public Vector2 GetWindowPos()
    {
        return ImGui.GetWindowPos();
    }

    public Vector2 GetWindowSize()
    {
        return ImGui.GetWindowSize();
    }

    public double GetTime()
    {
        return ImGui.GetTime();
    }

    public float GetCursorXPos()
    {
        return ImGui.GetCursorPosX();
    }

    public float GetCursorYPos()
    {
        return ImGui.GetCursorPosY();
    }

    public void SetCursorYPos(float yPos)
    {
        ImGui.SetCursorPosY(yPos);
    }

    public void SetCursorXPos(float xPos)
    {
        ImGui.SetCursorPosX(xPos);
    }

    public void SeparatorText(string text)
    {
        ImGui.SeparatorText(text);
    }

    public void Dummy(Vector2 size)
    {
        ImGui.Dummy(size);
    }

    public void SameLine()
    {
        ImGui.SameLine();
    }

    public bool BeginChild(string id, Vector2 vector2, ChildFlags flags)
    {
        return ImGui.BeginChild(id, vector2, (ImGuiChildFlags)flags);
    }

    public void EndChild()
    {
        ImGui.EndChild();
    }

    public bool Selectable(string text, bool enabled)
    {
        return ImGui.Selectable(text, enabled);
    }

    public void LabelText(string title, string value)
    {
        ImGui.LabelText(title, value);
    }

    public void Text(string s)
    {
        ImGui.Text(s);
    }

    public bool DragFloat4(string label, ref Vector4 value)
    {
        return ImGui.DragFloat4(label, ref value);
    }

    public bool BeginItemTooltip()
    {
        return ImGui.BeginItemTooltip();
    }

    public float GetFontSize()
    {
        return ImGui.GetFontSize();
    }

    public void PushTextWrapPos(float getFontSize)
    {
        ImGui.PushTextWrapPos(getFontSize);
    }

    public void TextUnformatted(string? toString)
    {
        ImGui.TextUnformatted(toString);
    }

    public void PopTextWrapPos()
    {
        ImGui.PopTextWrapPos();
    }

    public void EndTooltip()
    {
        ImGui.EndTooltip();
    }

    public bool CollapsingHeader(string name)
    {
        return ImGui.CollapsingHeader(name);
    }

    public bool Button(string name)
    {
        return ImGui.Button(name);
    }

    public bool Button(string name, Vector2 value)
    {
        return ImGui.Button(name, value);
    }

    public bool Checkbox(string name, ref bool value)
    {
        return ImGui.Checkbox(name, ref value);
    }

    public bool ColorEdit4(string name, ref Vector4 color)
    {
        return ImGui.ColorEdit4(name, ref color);
    }

    public bool DragFloat(string name, ref float value)
    {
        return ImGui.DragFloat(name, ref value);
    }

    public bool DragFloat(string name, ref float value, float speed, float min, float max)
    {
        return ImGui.DragFloat(name, ref value, speed, min, max);
    }

    public bool SliderFloat(string name, ref float value, float min, float max)
    {
        return ImGui.SliderFloat(name, ref value, min, max);
    }

    public bool DragFloat2(string name, ref Vector2 value)
    {
        return ImGui.DragFloat2(name, ref value);
    }

    public bool DragFloat2(string name, ref Vector2 value, float speed, float min, float max)
    {
        return ImGui.DragFloat2(name, ref value, speed, min, max);
    }

    public bool SliderFloat2(string name, ref Vector2 value, float min, float max)
    {
        return ImGui.SliderFloat2(name, ref value, min, max);
    }

    public bool DragInt2(string name, ref int value, int speed, int min, int max)
    {
        return ImGui.DragInt2(name, ref value, speed, min, max);
    }

    public bool DragInt2(string name, ref int value)
    {
        return ImGui.DragInt2(name, ref value);
    }

    public bool SliderInt2(string name, ref int value, int min, int max)
    {
        return ImGui.SliderInt2(name, ref value, min, max);
    }

    public bool DragFloat3(string name, ref Vector3 value)
    {
        return ImGui.DragFloat3(name, ref value);
    }

    public bool DragFloat3(string name, ref Vector3 value, float speed, float min, float max)
    {
        return ImGui.DragFloat3(name, ref value, speed, min, max);
    }

    public bool SliderFloat3(string name, ref Vector3 value, float min, float max)
    {
        return ImGui.SliderFloat3(name, ref value, min, max);
    }

    public bool DragFloat4(string label, ref Vector4 value, float speed, float min, float max)
    {
        return ImGui.DragFloat4(label, ref value, speed, min, max);
    }

    public bool SliderFloat4(string name, ref Vector4 value, float min, float max)
    {
        return ImGui.SliderFloat4(name, ref value, min, max);
    }

    public bool DragInt(string name, ref int value)
    {
        return ImGui.DragInt(name, ref value);
    }

    public bool DragInt(string name, ref int value, int speed, int min, int max)
    {
        return ImGui.DragInt(name, ref value, speed, min, max);
    }

    public bool SliderInt(string name, ref int value, int min, int max)
    {
        return ImGui.SliderInt(name, ref value, min, max);
    }

    public bool InputText(string name, ref string value, uint maxLength)
    {
        return ImGui.InputText(name, ref value, maxLength);
    }

    public bool BeginTabBar(string id, TabBarFlags flags)
    {
        return ImGui.BeginTabBar(id, (ImGuiTabBarFlags)flags);
    }

    public bool BeginTabItem(string id)
    {
        return ImGui.BeginTabBar(id);
    }

    public void EndTabItem()
    {
        ImGui.EndTabItem();
    }

    public void EndTabBar()
    {
        ImGui.EndTabBar();
    }

    public bool MenuItem(string text, string? shortcut, ref bool enabled)
    {
        return ImGui.MenuItem(text, shortcut, ref enabled);
    }

    public bool BeginMainMenuBar()
    {
        return ImGui.BeginMainMenuBar();
    }

    public bool BeginMenuBar()
    {
        return ImGui.BeginMenuBar();
    }

    public bool BeginMenu(string label)
    {
        return ImGui.BeginMenu(label);
    }

    public void EndMenu()
    {
        ImGui.EndMenu();
    }

    public void EndMainMenuBar()
    {
        ImGui.EndMainMenuBar();
    }

    public void EndMenuBar()
    {
        ImGui.EndMenuBar();
    }

    public bool Begin(string title, WindowFlags flags)
    {
        return ImGui.Begin(title, (ImGuiWindowFlags)flags);
    }

    public void End()
    {
        ImGui.End();
    }

    public bool Begin(string title, ref bool isOpen, WindowFlags flags = WindowFlags.None)
    {
        return ImGui.Begin(title, ref isOpen, (ImGuiWindowFlags)flags);
    }

    public void OpenPopup(string id)
    {
        ImGui.OpenPopup(id);
    }

    public void EndPopup()
    {
        ImGui.EndPopup();
    }

    public bool BeginPopup(string id)
    {
        return ImGui.BeginPopup(id);
    }

    public void DockSpaceOverMainViewport()
    {
        ImGui.DockSpaceOverViewport(0, ImGui.GetMainViewport(), ImGuiDockNodeFlags.PassthruCentralNode | ImGuiDockNodeFlags.AutoHideTabBar);
    }

    public void ShowAboutWindow(ref bool showDearImGuiAboutWindow)
    {
        ImGui.ShowAboutWindow(ref showDearImGuiAboutWindow);
    }

    public void ShowDemoWindow(ref bool showDearImGuiDemoWindow)
    {
        ImGui.ShowDemoWindow(ref showDearImGuiDemoWindow);
    }

    public void ShowMetricsWindow(ref bool showDearImGuiMetricsWindow)
    {
        ImGui.ShowMetricsWindow(ref showDearImGuiMetricsWindow);
    }

    public void ShowDebugLogWindow(ref bool showDearImGuiDebugLogWindow)
    {
        ImGui.ShowDebugLogWindow(ref showDearImGuiDebugLogWindow);
    }

    public void ShowIdStackToolWindow(ref bool showDearImGuiIdStackToolWindow)
    {
        ImGui.ShowIDStackToolWindow(ref showDearImGuiIdStackToolWindow);
    }

    public void BeginDisabled()
    {
        ImGui.BeginDisabled();
    }

    public void EndDisabled()
    {
        ImGui.EndDisabled();
    }

    public void Indent()
    {
        ImGui.Indent();
    }

    public void Unindent()
    {
        ImGui.Unindent();
    }

    public void Image(IntPtr imageId, Vector2 size)
    {
        ImGui.Image(imageId, size);
    }

    public void Image(IntPtr imageId, Vector2 size, Vector2 uv0, Vector2 uv1)
    {
        ImGui.Image(imageId, size, uv0, uv1);
    }

    public bool ImageButton(string name, IntPtr imageId, Vector2 size)
    {
        return ImGui.ImageButton(name, imageId, size);
    }


    public void SetThemingColor(ColorTypes colorType, Vector4 color)
    {
        ImGui.GetStyle().Colors[(int)colorType] = color;
    }

    public void AddConfigTheme(ConfigFlags configFlags)
    {
        ImGui.GetIO().ConfigFlags |= (ImGuiConfigFlags)configFlags;
    }

    public void RemoveConfigTheme(ConfigFlags configFlags)
    {
        ImGui.GetIO().ConfigFlags &= ~(ImGuiConfigFlags)configFlags;
    }

    public void SetConfigWindowsMoveFromTitleBarOnly(bool value)
    {
        ImGui.GetIO().ConfigWindowsMoveFromTitleBarOnly = value;
    }

    public void SetWindowRounding(int value)
    {
        ImGui.GetStyle().WindowRounding = value;
    }

    public void SetChildRounding(int value)
    {
        ImGui.GetStyle().ChildRounding = value;
    }

    public void SetFrameRounding(int value)
    {
        ImGui.GetStyle().FrameRounding = value;
    }

    public void SetPopupRounding(int value)
    {
        ImGui.GetStyle().PopupRounding = value;
    }

    public void SetScrollbarRounding(int value)
    {
        ImGui.GetStyle().ScrollbarRounding = value;
    }

    public void SetGrabRounding(int value)
    {
        ImGui.GetStyle().GrabRounding = value;
    }

    public void SetTabRounding(int value)
    {
        ImGui.GetStyle().TabRounding = value;
    }

    public void SetTabBorderSize(int value)
    {
        ImGui.GetStyle().TabBorderSize = value;
    }

    public void SetWindowTitleAlign(Vector2 value)
    {
        ImGui.GetStyle().WindowTitleAlign = value;
    }

    public void SetSeparatorTextAlign(Vector2 value)
    {
        ImGui.GetStyle().SeparatorTextAlign = value;
    }

    public void SetSeparatorTextPadding(Vector2 value)
    {
        ImGui.GetStyle().SeparatorTextPadding = value;
    }

    public void StyleColorsDark()
    {
        ImGui.StyleColorsDark();
    }

    public void StyleColorsLight()
    {
        ImGui.StyleColorsLight();
    }

    public void LoadFont(string path, float pixelSize)
    {
        try
        {
            ImGui.GetIO().Fonts.AddFontFromFileTTF(path, pixelSize);
        }
        catch (Exception e)
        {
            Log.Exception(e);
        }
    }

    public void LoadFontFromMemory(byte[] fontData, int pixelSize, int dataSize)
    {
        try
        {
            unsafe
            {
                fixed (byte* p = fontData)
                {
                    ImGui.GetIO().Fonts.AddFontFromMemoryTTF((IntPtr)p, dataSize, pixelSize);
                }
            }
        }
        catch (Exception e)
        {
            Log.Exception(e);
        }
    }

    public void LoadDefaultFonts()
    {
        ImGui.GetIO().Fonts.AddFontDefault();
    }

    public IntPtr CreateContext()
    {
        return ImGui.CreateContext();
    }

    public void SetCurrentContext(IntPtr context)
    {
        ImGui.SetCurrentContext(context);
    }

    public void DestroyContext(IntPtr context)
    {
        ImGui.DestroyContext(context);
    }
}