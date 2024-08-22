using CopperDevs.DearImGui.Enums;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace CopperDevs.DearImGui;

public interface IImGuiBackend
{
    #region data

    public bool WantCaptureMouse { get; }
    public bool IsWindowHovered(HoveredFlags flags);
    public float GetWindowWidth();
    public float GetWindowHeight();
    public Vector2 GetWindowPos();
    public Vector2 GetWindowSize();
    public double GetTime();

    #endregion

    #region ui

    public void SeparatorText(string text);
    public void Dummy(Vector2 size);
    public void SameLine();
    public bool BeginChild(string id, Vector2 vector2, ChildFlags flags);
    public void EndChild();
    public bool Selectable(string text, bool enabled);
    public void LabelText(string title, string value);
    public void Text(string s);
    public bool DragFloat4(string label, ref Vector4 value);
    public bool BeginItemTooltip();
    public float GetFontSize();
    public void PushTextWrapPos(float getFontSize);
    public void TextUnformatted(string? toString);
    public void PopTextWrapPos();
    public void EndTooltip();
    public bool CollapsingHeader(string name);
    public bool Button(string name);
    public bool Button(string name, Vector2 value);
    public bool Checkbox(string name, ref bool value);
    public bool ColorEdit4(string name, ref Vector4 color);
    public bool DragFloat(string name, ref float value);
    public bool DragFloat(string name, ref float value, float speed, float min, float max);
    public bool SliderFloat(string name, ref float value, float min, float max);
    public bool DragFloat2(string name, ref Vector2 value);
    public bool DragFloat2(string name, ref Vector2 value, float speed, float min, float max);
    public bool SliderFloat2(string name, ref Vector2 value, float min, float max);
    public bool DragInt2(string name, ref int value, int speed, int min, int max);
    public bool DragInt2(string name, ref int value);
    public bool SliderInt2(string name, ref int value, int min, int max);
    public bool DragFloat3(string name, ref Vector3 value);
    public bool DragFloat3(string name, ref Vector3 value, float speed, float min, float max);
    public bool SliderFloat3(string name, ref Vector3 value, float min, float max);
    public bool DragFloat4(string label, ref Vector4 value, float speed, float min, float max);
    public bool SliderFloat4(string name, ref Vector4 value, float min, float max);
    public bool DragInt(string name, ref int value);
    public bool DragInt(string name, ref int value, int speed, int min, int max);
    public bool SliderInt(string name, ref int value, int min, int max);
    public bool InputText(string name, ref string value, uint maxLength);
    public bool BeginTabBar(string id, TabBarFlags flags);
    public bool BeginTabItem(string id);
    public void EndTabItem();
    public void EndTabBar();
    public bool MenuItem(string text, string? shortcut, ref bool enabled);
    public bool BeginMainMenuBar();
    public bool BeginMenuBar();
    public bool BeginMenu(string label);
    public void EndMenu();
    public void EndMainMenuBar();
    public void EndMenuBar();
    public bool Begin(string title, WindowFlags flags);
    public void End();
    public bool Begin(string title, ref bool isOpen, WindowFlags flags = WindowFlags.None);
    public void OpenPopup(string id);
    public void EndPopup();
    public bool BeginPopup(string id);
    public void DockSpaceOverMainViewport();
    public void ShowAboutWindow(ref bool showDearImGuiAboutWindow);
    public void ShowDemoWindow(ref bool showDearImGuiDemoWindow);
    public void ShowMetricsWindow(ref bool showDearImGuiMetricsWindow);
    public void ShowDebugLogWindow(ref bool showDearImGuiDebugLogWindow);
    public void ShowIdStackToolWindow(ref bool showDearImGuiIdStackToolWindow);
    public void BeginDisabled();
    public void EndDisabled();
    public void Indent();
    public void Unindent();

    #endregion

    #region styling

    public void SetThemingColor(ImGuiColors colorType, Vector4 color);
    public void AddConfigTheme(ConfigFlags configFlags);
    public void RemoveConfigTheme(ConfigFlags configFlags);

    public void SetConfigWindowsMoveFromTitleBarOnly(bool value);

    public void SetWindowRounding(int value);
    public void SetChildRounding(int value);
    public void SetFrameRounding(int value);
    public void SetPopupRounding(int value);
    public void SetScrollbarRounding(int value);
    public void SetGrabRounding(int value);
    public void SetTabRounding(int value);
    public void SetTabBorderSize(int value);
    public void SetWindowTitleAlign(Vector2 value);
    public void SetSeparatorTextAlign(Vector2 value);
    public void SetSeparatorTextPadding(Vector2 value);

    #endregion
}