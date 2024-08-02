using ImGuiNET;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    private static readonly Dictionary<string, Action> RegisteredPopups = new();

    public static void RegisterPopup(string id, Action renderAction)
    {
        RegisteredPopups.TryAdd(id, renderAction);
    }

    public static void DeregisterPopup(string id)
    {
        RegisteredPopups.Remove(id);
    }

    public static void ShowPopup(string id)
    {
        ImGui.OpenPopup(id);
    }

    private static void RenderPopups()
    {
        foreach (var popup in RegisteredPopups.Where(popup => ImGui.BeginPopup(popup.Key)))
        {
            popup.Value?.Invoke();
            ImGui.EndPopup();
        }
    }

    public static void ForceRenderPopup(string id)
    {
        if (!RegisteredPopups.TryGetValue(id, out var popup))
            return;

        if (!ImGui.BeginPopup(id))
            return;

        popup?.Invoke();
        ImGui.EndPopup();
    }
}