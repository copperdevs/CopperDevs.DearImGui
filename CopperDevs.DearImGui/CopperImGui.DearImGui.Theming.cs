using CopperDevs.DearImGui.Enums;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    private static void LoadConfig()
    {
        CurrentBackend.AddConfigTheme(ConfigFlags.DockingEnable);
        CurrentBackend.AddConfigTheme(ConfigFlags.ViewportsEnable);
        CurrentBackend.AddConfigTheme(ConfigFlags.NavEnableKeyboard);
        CurrentBackend.AddConfigTheme(ConfigFlags.NavEnableGamepad);

        CurrentBackend.SetConfigWindowsMoveFromTitleBarOnly(true);

        CurrentBackend.SetWindowRounding(5);
        CurrentBackend.SetChildRounding(5);
        CurrentBackend.SetFrameRounding(5);
        CurrentBackend.SetPopupRounding(5);
        CurrentBackend.SetScrollbarRounding(5);
        CurrentBackend.SetGrabRounding(5);
        CurrentBackend.SetTabRounding(5);

        CurrentBackend.SetTabBorderSize(1);

        CurrentBackend.SetWindowTitleAlign(new Vector2(0.5f));
        CurrentBackend.SetSeparatorTextAlign(new Vector2(0.5f));
        CurrentBackend.SetSeparatorTextPadding(new Vector2(20, 5));
    }

    private static void LoadStyle()
    {
        // yummy windows
        CurrentBackend.SetThemingColor(ImGuiColors.WindowBg, new Vector4(0.1f, 0.105f, 0.11f, 1.0f));

        // Headers
        CurrentBackend.SetThemingColor(ImGuiColors.Header, new Vector4(0.2f, 0.205f, 0.21f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.HeaderHovered, new Vector4(0.3f, 0.305f, 0.31f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.HeaderActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        // Buttons
        CurrentBackend.SetThemingColor(ImGuiColors.Button, new Vector4(0.2f, 0.205f, 0.21f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.ButtonHovered, new Vector4(0.3f, 0.305f, 0.31f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.ButtonActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        // Frame BG
        CurrentBackend.SetThemingColor(ImGuiColors.FrameBg, new Vector4(0.2f, 0.205f, 0.21f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.FrameBgHovered, new Vector4(0.3f, 0.305f, 0.31f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.FrameBgActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        // Tabs
        CurrentBackend.SetThemingColor(ImGuiColors.Tab, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.TabHovered, new Vector4(0.38f, 0.3805f, 0.381f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.TabSelected, new Vector4(0.28f, 0.2805f, 0.281f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.TabSelectedOverline, new Vector4(1f, 1f, 1f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.TabDimmed, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.TabDimmedSelected, new Vector4(0.2f, 0.205f, 0.21f, 1.0f));

        // Title
        CurrentBackend.SetThemingColor(ImGuiColors.TitleBg, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.TitleBgActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.TitleBgCollapsed, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        // this crap
        CurrentBackend.SetThemingColor(ImGuiColors.CheckMark, new Vector4(1f, 1f, 1f, 1.0f));

        CurrentBackend.SetThemingColor(ImGuiColors.SliderGrab, new Vector4(0.4f, 0.4f, 0.4f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.SliderGrabActive, new Vector4(0.25f, 0.25f, 0.25f, 1.0f));

        CurrentBackend.SetThemingColor(ImGuiColors.ScrollbarGrab, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.ScrollbarGrabActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        CurrentBackend.SetThemingColor(ImGuiColors.Separator, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        CurrentBackend.SetThemingColor(ImGuiColors.SeparatorActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
    }
}