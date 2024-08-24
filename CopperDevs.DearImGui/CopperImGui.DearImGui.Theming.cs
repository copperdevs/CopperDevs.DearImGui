using CopperDevs.DearImGui.Wrapping.Enums;

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
        CurrentBackend.SetThemingColor(ColorTypes.WindowBg, new Vector4(0.1f, 0.105f, 0.11f, 1.0f));

        // Headers
        CurrentBackend.SetThemingColor(ColorTypes.Header, new Vector4(0.2f, 0.205f, 0.21f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.HeaderHovered, new Vector4(0.3f, 0.305f, 0.31f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.HeaderActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        // Buttons
        CurrentBackend.SetThemingColor(ColorTypes.Button, new Vector4(0.2f, 0.205f, 0.21f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.ButtonHovered, new Vector4(0.3f, 0.305f, 0.31f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.ButtonActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        // Frame BG
        CurrentBackend.SetThemingColor(ColorTypes.FrameBg, new Vector4(0.2f, 0.205f, 0.21f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.FrameBgHovered, new Vector4(0.3f, 0.305f, 0.31f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.FrameBgActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        // Tabs
        CurrentBackend.SetThemingColor(ColorTypes.Tab, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.TabHovered, new Vector4(0.38f, 0.3805f, 0.381f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.TabSelected, new Vector4(0.28f, 0.2805f, 0.281f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.TabSelectedOverline, new Vector4(1f, 1f, 1f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.TabDimmed, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.TabDimmedSelected, new Vector4(0.2f, 0.205f, 0.21f, 1.0f));

        // Title
        CurrentBackend.SetThemingColor(ColorTypes.TitleBg, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.TitleBgActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.TitleBgCollapsed, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        // this crap
        CurrentBackend.SetThemingColor(ColorTypes.CheckMark, new Vector4(1f, 1f, 1f, 1.0f));

        CurrentBackend.SetThemingColor(ColorTypes.SliderGrab, new Vector4(0.4f, 0.4f, 0.4f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.SliderGrabActive, new Vector4(0.25f, 0.25f, 0.25f, 1.0f));

        CurrentBackend.SetThemingColor(ColorTypes.ScrollbarGrab, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.ScrollbarGrabActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));

        CurrentBackend.SetThemingColor(ColorTypes.Separator, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
        CurrentBackend.SetThemingColor(ColorTypes.SeparatorActive, new Vector4(0.15f, 0.1505f, 0.151f, 1.0f));
    }
}