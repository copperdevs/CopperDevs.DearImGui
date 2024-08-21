using CopperDevs.DearImGui.Enums;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    private static void LoadConfig()
    {
        currentBinding.AddConfigTheme(ConfigFlags.DockingEnable);
        currentBinding.AddConfigTheme(ConfigFlags.ViewportsEnable);
        currentBinding.AddConfigTheme(ConfigFlags.NavEnableKeyboard);
        currentBinding.AddConfigTheme(ConfigFlags.NavEnableGamepad);

        currentBinding.SetConfigWindowsMoveFromTitleBarOnly(true);

        currentBinding.SetWindowRounding(5);
        currentBinding.SetChildRounding(5);
        currentBinding.SetFrameRounding(5);
        currentBinding.SetPopupRounding(5);
        currentBinding.SetScrollbarRounding(5);
        currentBinding.SetGrabRounding(5);
        currentBinding.SetTabRounding(5);

        currentBinding.SetTabBorderSize(1);

        currentBinding.SetWindowTitleAlign(new Vector2(0.5f));
        currentBinding.SetSeparatorTextAlign(new Vector2(0.5f));
        currentBinding.SetSeparatorTextPadding(new Vector2(20, 5));
    }

    private static void LoadStyle()
    {
        // yummy windows
        currentBinding.SetThemingColor(ImGuiColors.WindowBg, new Vector4(0.1f, 0.105f, 0.11f, 1.0f));

        // Headers
        currentBinding.SetThemingColor(ImGuiColors.Header, new Vector4(0.2f, 0.205f, 0.21f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.HeaderHovered, new Vector4(0.3f, 0.305f, 0.31f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.HeaderActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        // Buttons
        currentBinding.SetThemingColor(ImGuiColors.Button, new Vector4(0.2f, 0.205f, 0.21f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.ButtonHovered, new Vector4(0.3f, 0.305f, 0.31f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.ButtonActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        // Frame BG
        currentBinding.SetThemingColor(ImGuiColors.FrameBg, new Vector4(0.2f, 0.205f, 0.21f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.FrameBgHovered, new Vector4(0.3f, 0.305f, 0.31f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.FrameBgActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        // Tabs
        currentBinding.SetThemingColor(ImGuiColors.Tab, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.TabHovered, new Vector4(0.38f, 0.3805f, 0.381f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.TabSelected, new Vector4(0.28f, 0.2805f, 0.281f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.TabSelectedOverline, new Vector4(1f, 1f, 1f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.TabDimmed, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.TabDimmedSelected, new Vector4(0.2f, 0.205f, 0.21f, 1.0f));

        // Title
        currentBinding.SetThemingColor(ImGuiColors.TitleBg, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.TitleBgActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.TitleBgCollapsed, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        // this crap
        currentBinding.SetThemingColor(ImGuiColors.CheckMark, new Vector4(1f, 1f, 1f, 1.0f));

        currentBinding.SetThemingColor(ImGuiColors.SliderGrab, new Vector4(0.4f, 0.4f, 0.4f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.SliderGrabActive, new Vector4(0.25f, 0.25f, 0.25f, 1.0f));

        currentBinding.SetThemingColor(ImGuiColors.ScrollbarGrab, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.ScrollbarGrabActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        currentBinding.SetThemingColor(ImGuiColors.Separator, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        currentBinding.SetThemingColor(ImGuiColors.SeparatorActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
    }
}