using System.Numerics;
using CopperDevs.DearImGui.Enums;
using ImGuiNET;

namespace CopperDevs.DearImGui.Binding.ImGuiNET;

public class ImGuiNETBindings : IImGuiBinding
{
    public bool WantCaptureMouse => ImGui.GetIO().WantCaptureMouse;

    public bool IsWindowHovered(HoveredFlags flags) => ImGui.IsWindowHovered((ImGuiHoveredFlags)flags);

    public float GetWindowWidth() => ImGui.GetWindowWidth();

    public float GetWindowHeight() => ImGui.GetWindowHeight();

    public Vector2 GetWindowPos() => ImGui.GetWindowPos();

    public Vector2 GetWindowSize() => ImGui.GetWindowSize();

    public double GetTime() => ImGui.GetTime();

    public void SeparatorText(string empty)
    {
        throw new NotImplementedException();
    }

    public void Dummy(Vector2 vector2)
    {
        throw new NotImplementedException();
    }

    public void SameLine()
    {
        throw new NotImplementedException();
    }

    public bool BeginChild(string id, Vector2 vector2, ChildFlags flags)
    {
        throw new NotImplementedException();
    }

    public void EndChild()
    {
        throw new NotImplementedException();
    }

    public bool Selectable(string text, bool enabled)
    {
        throw new NotImplementedException();
    }

    public void LabelText(string title, string value)
    {
        throw new NotImplementedException();
    }

    public void Text(string s)
    {
        throw new NotImplementedException();
    }

    public bool DragFloat4(string rowName, ref Vector4 row)
    {
        throw new NotImplementedException();
    }

    public bool BeginItemTooltip()
    {
        throw new NotImplementedException();
    }

    public float GetFontSize()
    {
        throw new NotImplementedException();
    }

    public void PushTextWrapPos(float getFontSize)
    {
        throw new NotImplementedException();
    }

    public void TextUnformatted(string? toString)
    {
        throw new NotImplementedException();
    }

    public void PopTextWrapPos()
    {
        throw new NotImplementedException();
    }

    public void EndTooltip()
    {
        throw new NotImplementedException();
    }

    public bool CollapsingHeader(string name)
    {
        throw new NotImplementedException();
    }

    public bool Button(string name)
    {
        throw new NotImplementedException();
    }

    public bool Button(string name, Vector2 vector2)
    {
        throw new NotImplementedException();
    }

    public bool Checkbox(string name, ref bool currentValue)
    {
        throw new NotImplementedException();
    }

    public bool ColorEdit4(string name, ref Vector4 color)
    {
        throw new NotImplementedException();
    }

    public bool DragFloat(string name, ref float value)
    {
        throw new NotImplementedException();
    }

    public bool DragFloat(string name, ref float value, float speed, float min, float max)
    {
        throw new NotImplementedException();
    }

    public bool SliderFloat(string name, ref float value, float min, float max)
    {
        throw new NotImplementedException();
    }

    public bool DragFloat2(string name, ref Vector2 value)
    {
        throw new NotImplementedException();
    }

    public bool DragFloat2(string name, ref Vector2 value, float speed, float min, float max)
    {
        throw new NotImplementedException();
    }

    public bool SliderFloat2(string name, ref Vector2 value, float min, float max)
    {
        throw new NotImplementedException();
    }

    public bool DragInt2(string name, ref int valueX, int speed, int min, int max)
    {
        throw new NotImplementedException();
    }

    public bool DragInt2(string name, ref int valueX)
    {
        throw new NotImplementedException();
    }

    public bool SliderInt2(string name, ref int valueX, int min, int max)
    {
        throw new NotImplementedException();
    }

    public bool DragFloat3(string name, ref Vector3 value)
    {
        throw new NotImplementedException();
    }

    public bool DragFloat3(string name, ref Vector3 value, float speed, float min, float max)
    {
        throw new NotImplementedException();
    }

    public bool SliderFloat3(string name, ref Vector3 value, float min, float max)
    {
        throw new NotImplementedException();
    }

    public bool DragFloat4(string rowName, ref Vector4 value, float speed, float min, float max)
    {
        throw new NotImplementedException();
    }

    public bool SliderFloat4(string name, ref Vector4 value, float min, float max)
    {
        throw new NotImplementedException();
    }

    public bool DragInt(string name, ref int value)
    {
        throw new NotImplementedException();
    }

    public bool DragInt(string name, ref int value, int speed, int min, int max)
    {
        throw new NotImplementedException();
    }

    public bool SliderInt(string name, ref int value, int min, int max)
    {
        throw new NotImplementedException();
    }

    public bool InputText(string name, ref string value, uint maxLength)
    {
        throw new NotImplementedException();
    }

    public bool BeginTabBar(string id, TabBarFlags reorderable)
    {
        throw new NotImplementedException();
    }

    public bool BeginTabItem(string s)
    {
        throw new NotImplementedException();
    }

    public void EndTabItem()
    {
        throw new NotImplementedException();
    }

    public void EndTabBar()
    {
        throw new NotImplementedException();
    }

    public bool MenuItem(string text, object o, ref bool enabled)
    {
        throw new NotImplementedException();
    }

    public bool BeginMainMenuBar()
    {
        throw new NotImplementedException();
    }

    public bool BeginMenuBar()
    {
        throw new NotImplementedException();
    }

    public bool BeginMenu(string subMenuItem1)
    {
        throw new NotImplementedException();
    }

    public void EndMenu()
    {
        throw new NotImplementedException();
    }

    public void EndMainMenuBar()
    {
        throw new NotImplementedException();
    }

    public void EndMenuBar()
    {
        throw new NotImplementedException();
    }

    public bool Begin(string title, WindowFlags flags)
    {
        throw new NotImplementedException();
    }

    public void End()
    {
        throw new NotImplementedException();
    }

    public bool Begin(string title, ref bool isOpen, WindowFlags flags = WindowFlags.None)
    {
        throw new NotImplementedException();
    }

    public void OpenPopup(string id)
    {
        throw new NotImplementedException();
    }

    public void EndPopup()
    {
        throw new NotImplementedException();
    }

    public bool BeginPopup(string id)
    {
        throw new NotImplementedException();
    }

    public void DockSpaceOverMainViewport()
    {
        throw new NotImplementedException();
    }

    public void ShowAboutWindow(ref bool showDearImGuiAboutWindow)
    {
        throw new NotImplementedException();
    }

    public void ShowDemoWindow(ref bool showDearImGuiDemoWindow)
    {
        throw new NotImplementedException();
    }

    public void ShowMetricsWindow(ref bool showDearImGuiMetricsWindow)
    {
        throw new NotImplementedException();
    }

    public void ShowDebugLogWindow(ref bool showDearImGuiDebugLogWindow)
    {
        throw new NotImplementedException();
    }

    public void ShowIDStackToolWindow(ref bool showDearImGuiIdStackToolWindow)
    {
        throw new NotImplementedException();
    }

    public void BeginDisabled()
    {
        throw new NotImplementedException();
    }

    public void EndDisabled()
    {
        throw new NotImplementedException();
    }

    public void Indent()
    {
        throw new NotImplementedException();
    }

    public void Unindent()
    {
        throw new NotImplementedException();
    }

    public void SetThemingColor(ImGuiColors colorType, Vector4 color)
    {
        throw new NotImplementedException();
    }

    public void AddConfigTheme(ConfigFlags configFlags)
    {
        throw new NotImplementedException();
    }

    public void RemoveConfigTheme(ConfigFlags configFlags)
    {
        throw new NotImplementedException();
    }

    public void SetConfigWindowsMoveFromTitleBarOnly(bool value)
    {
        throw new NotImplementedException();
    }

    public void SetWindowRounding(int value)
    {
        throw new NotImplementedException();
    }

    public void SetChildRounding(int value)
    {
        throw new NotImplementedException();
    }

    public void SetFrameRounding(int value)
    {
        throw new NotImplementedException();
    }

    public void SetPopupRounding(int value)
    {
        throw new NotImplementedException();
    }

    public void SetScrollbarRounding(int value)
    {
        throw new NotImplementedException();
    }

    public void SetGrabRounding(int value)
    {
        throw new NotImplementedException();
    }

    public void SetTabRounding(int value)
    {
        throw new NotImplementedException();
    }

    public void SetTabBorderSize(int value)
    {
        throw new NotImplementedException();
    }

    public void SetWindowTitleAlign(Vector2 value)
    {
        throw new NotImplementedException();
    }

    public void SetSeparatorTextAlign(Vector2 value)
    {
        throw new NotImplementedException();
    }

    public void SetSeparatorTextPadding(Vector2 value)
    {
        throw new NotImplementedException();
    }
}