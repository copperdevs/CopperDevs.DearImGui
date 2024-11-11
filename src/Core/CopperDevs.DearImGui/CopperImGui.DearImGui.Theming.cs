using CopperDevs.DearImGui.Utility;
using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    private static bool configLoaded = false;
    private static bool styleLoaded = false;

    private static void LoadConfig()
    {
        if (configLoaded)
            return;

        configLoaded = true;

        try
        {
            if (DockingEnabled)
                ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;
            ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;
            ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard;
            ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad;

            ImGui.GetIO().ConfigWindowsMoveFromTitleBarOnly = true;

            ImGui.GetStyle().WindowRounding = 5;
            ImGui.GetStyle().ChildRounding = 5;
            ImGui.GetStyle().FrameRounding = 5;
            ImGui.GetStyle().PopupRounding = 5;
            ImGui.GetStyle().ScrollbarRounding = 5;
            ImGui.GetStyle().GrabRounding = 5;
            ImGui.GetStyle().TabRounding = 5;

            ImGui.GetStyle().TabBorderSize = 1;

            ImGui.GetStyle().WindowTitleAlign = new Vector2(0.5f);
            ImGui.GetStyle().SeparatorTextAlign = new Vector2(0.5f);
            ImGui.GetStyle().SeparatorTextPadding = new Vector2(20, 5);
        }
        catch (Exception e)
        {
            Log.Exception(e);
        }
    }

    private static void LoadStyle()
    {
        if (styleLoaded || !settings.HasFlag(RenderingSettings.UseCustomStyling))
            return;

        styleLoaded = true;

        try
        {
            ImGui.StyleColorsDark();

            ImGui.GetStyle().Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.1f, 0.105f, 0.11f, 1.0f);

            // yummy windows
            ImGui.GetStyle().Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.1f, 0.105f, 0.11f, 1.0f);

            // Headers
            ImGui.GetStyle().Colors[(int)ImGuiCol.Header] = new Vector4(0.2f, 0.205f, 0.21f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.3f, 0.305f, 0.31f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);

            // Buttons
            ImGui.GetStyle().Colors[(int)ImGuiCol.Button] = new Vector4(0.2f, 0.205f, 0.21f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.3f, 0.305f, 0.31f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);

            // Frame BG
            ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.2f, 0.205f, 0.21f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.3f, 0.305f, 0.31f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);

            // Tabs
            ImGui.GetStyle().Colors[(int)ImGuiCol.Tab] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.38f, 0.3805f, 0.381f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.TabSelected] = new Vector4(0.28f, 0.2805f, 0.281f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.TabSelectedOverline] = new Vector4(1f, 1f, 1f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.TabDimmed] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.TabDimmedSelected] = new Vector4(0.2f, 0.205f, 0.21f, 1.0f);

            // Title
            ImGui.GetStyle().Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);

            // this crap
            ImGui.GetStyle().Colors[(int)ImGuiCol.CheckMark] = new Vector4(1f, 1f, 1f, 1.0f);

            ImGui.GetStyle().Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.25f, 0.25f, 0.25f, 1.0f);

            ImGui.GetStyle().Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);

            ImGui.GetStyle().Colors[(int)ImGuiCol.Separator] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
            ImGui.GetStyle().Colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
        }
        catch (Exception e)
        {
            Log.Exception(e);
        }
    }
}