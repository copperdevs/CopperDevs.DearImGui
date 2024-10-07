using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    private static ImGuiContextPtr currentContext;

    /// <summary>
    ///     Get the current ImGui context
    /// </summary>
    /// <returns>The context pointer</returns>
    public static ImGuiContextPtr GetCurrentContext()
    {
        return currentContext;
    }

    /// <summary>
    ///     Set the current context
    /// </summary>
    /// <param name="context">The context to set</param>
    public static void SetCurrentContext(ImGuiContextPtr context)
    {
        currentContext = context;
        ImGui.SetCurrentContext(context);
    }

    /// <summary>
    ///     Create a new context, and optionally set it as the current one
    /// </summary>
    /// <param name="setAsCurrent">Option to set the created context as the new one</param>
    /// <returns>The created context</returns>
    public static ImGuiContextPtr CreateContext(bool setAsCurrent = false)
    {
        var context = ImGui.CreateContext();

        if (setAsCurrent)
            SetCurrentContext(context);

        return context;
    }

    /// <summary>
    ///     Destroy the currently set context
    /// </summary>
    /// <remarks>Once destroyed it sets the current context to <see cref="Hexa.NET.ImGui.ImGuiContextPtr.Null" /></remarks>
    public static void DestroyCurrentContext()
    {
        ImGui.DestroyContext(currentContext);
        currentContext = ImGuiContextPtr.Null;
    }

    /// <summary>
    ///     Destroys a specific context
    /// </summary>
    /// <param name="context">Context to destroy</param>
    /// <remarks>Once destroyed it sets that context to <see cref="Hexa.NET.ImGui.ImGuiContextPtr.Null" /></remarks>
    public static void DestroyContext(ImGuiContextPtr context)
    {
        ImGui.DestroyContext(context);
        context = ImGuiContextPtr.Null;
    }
}