namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    private static object currentContext = null!;

    /// <summary>
    /// Get the current ImGui context
    /// </summary>
    /// <returns>The context pointer</returns>
    public static object GetCurrentContext()
    {
        return currentContext;
    }

    /// <summary>
    /// Set the current context
    /// </summary>
    /// <param name="context">The context to set</param>
    public static void SetCurrentContext(object context)
    {
        currentContext = context;
        CurrentBackend.SetCurrentContext(context);
    }

    /// <summary>
    /// Create a new context, and optionally set it as the current one
    /// </summary>
    /// <param name="setAsCurrent">Option to set the created context as the new one</param>
    /// <returns>The created context</returns>
    public static object CreateContext(bool setAsCurrent = false)
    {
        var context = CurrentBackend.CreateContext();

        if (setAsCurrent)
            SetCurrentContext(context);

        return context;
    }

    /// <summary>
    /// Destroy the currently set context
    /// </summary>
    /// <remarks>Once destroyed it sets the current context to <see cref="IntPtr.Zero"/></remarks>
    public static void DestroyCurrentContext()
    {
        CurrentBackend.DestroyContext(currentContext);
        currentContext = IntPtr.Zero;
    }

    /// <summary>
    /// Destroys a specific context
    /// </summary>
    /// <param name="context">Context to destroy</param>
    /// <remarks>Once destroyed it sets that context to null</remarks>
    public static void DestroyContext(object context)
    {
        CurrentBackend.DestroyContext(context);
        currentContext = IntPtr.Zero;
    }
}