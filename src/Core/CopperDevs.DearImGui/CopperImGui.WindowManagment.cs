using CopperDevs.DearImGui.Attributes;
using CopperDevs.DearImGui.Utility;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    private static WindowAttribute? currentlyRenderingWindow = null!;

    private static List<WindowAttribute> LoadWindows()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var targetAttribute = typeof(WindowAttribute);

        var createdObjects = new List<WindowAttribute>();

        foreach (var assembly in assemblies)
        foreach (var type in assembly.GetTypes())
        {
            if (type.GetCustomAttributes(targetAttribute, true).Length <= 0)
                continue;

            var attribute = (WindowAttribute)type.GetCustomAttribute(targetAttribute)!;
            attribute.GetMethods(Activator.CreateInstance(type)!);
            Log.Debug($"Found and loaded {type.FullName} window");
            createdObjects.Add(attribute);
        }

        return createdObjects;
    }

    private static void RenderWindows()
    {
        foreach (var window in windows)
        {
            if (showTabBar)
                if (CurrentBackend.BeginMainMenuBar())
                {
                    if (CurrentBackend.BeginMenu("Windows"))
                    {
                        CurrentBackend.MenuItem(window.WindowName, null, ref window.WindowOpen);
                        CurrentBackend.EndMenu();
                    }

                    CurrentBackend.EndMainMenuBar();
                }

            if (!window.WindowOpen)
                continue;

            currentlyRenderingWindow = window;

            if (!CurrentBackend.Begin(window.WindowName, ref window.WindowOpen, window.Flags))
                continue;

            window.Update();
            CurrentBackend.End();

            currentlyRenderingWindow = null;
        }
    }

    /// <summary>
    ///     Get the loaded instance of a specific window
    /// </summary>
    /// <typeparam name="T">Type of the window you want to get</typeparam>
    /// <returns>The instance of the window</returns>
    public static WindowAttribute? GetWindow<T>()
    {
        var windowAttribute = windows.FirstOrDefault(window => window.TargetClass.GetType() == typeof(T));

        if (windowAttribute is null)
            throw new NullReferenceException($"Window {typeof(T).FullName} not found");

        return windowAttribute;
    }

    /// <summary>
    /// Returns the created instance of the window 
    /// </summary>
    /// <typeparam name="T">Type of the window you want to get</typeparam>
    /// <returns>Instance of the target window</returns>
    /// <remarks>Use <see cref="GetWindow{T}"/> instead. This is here for legacy reasons.</remarks>
    [Obsolete("Since windows have been moved from a class inheritance solution to using attributes, there is no reason to get the class. Use GetWindow<T> instead.")]
    public static object? GetWindowClass<T>()
    {
        return windows.FirstOrDefault(window => window.TargetClass.GetType() == typeof(T))!.TargetClass;
    }

    /// <summary>
    /// Opens a target window
    /// </summary>
    /// <typeparam name="T">Type of the window</typeparam>
    public static void ShowWindow<T>()
    {
        GetWindow<T>()!.WindowOpen = true;
    }

    /// <summary>
    /// Hides a target window
    /// </summary>
    /// <typeparam name="T">Type of the window</typeparam>
    public static void HideWindow<T>()
    {
        GetWindow<T>()!.WindowOpen = false;
    }
}