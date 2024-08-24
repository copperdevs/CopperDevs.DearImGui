namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    private static readonly Dictionary<string, Action> RegisteredPopups = new();

    /// <summary>
    /// Register a new popup
    /// </summary>
    /// <param name="id">Id of the popup</param>
    /// <param name="renderAction">Render action of the popup</param>
    public static void RegisterPopup(string id, Action renderAction)
    {
        RegisteredPopups.TryAdd(id, renderAction);
    }

    /// <summary>
    /// Deregister a popup of a specific type
    /// </summary>
    /// <param name="id">ID of the popup to deregister</param>
    public static void DeregisterPopup(string id)
    {
        RegisteredPopups.Remove(id);
    }

    /// <summary>
    /// Open a popup of a specific id
    /// </summary>
    /// <param name="id">ID of the popup to render</param>
    public static void ShowPopup(string id)
    {
        CurrentBackend.OpenPopup(id);
    }

    private static void RenderPopups()
    {
        foreach (var popup in RegisteredPopups)
        {
            ForceRenderPopup(popup.Key);
        }
    }

    /// <summary>
    /// Force render popup instead of waiting for the system to call it automatically
    /// </summary>
    /// <param name="id">ID of the popup to force render</param>
    /// <remarks>This is not recommended at all, but it is publicly available just in case</remarks>
    public static void ForceRenderPopup(string id)
    {
        if (!RegisteredPopups.TryGetValue(id, out var popup))
            return;

        if (!CurrentBackend.BeginPopup(id))
            return;

        popup?.Invoke();
        CurrentBackend.EndPopup();
    }
}