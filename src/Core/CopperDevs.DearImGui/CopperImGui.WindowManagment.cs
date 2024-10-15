using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Utility;
using Hexa.NET.ImGui;

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
            
            if (Attribute.GetCustomAttribute(type, typeof(DisabledAttribute)) is not null)
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
        Profiler.Begin("Rendering Windows");

        foreach (var window in windows)
        {
            if (showTabBar)
                if (ImGui.BeginMainMenuBar())
                {
                    if (ImGui.BeginMenu("Windows"))
                    {
                        ImGui.MenuItem(window.WindowName, string.Empty, ref window.WindowOpen);
                        ImGui.EndMenu();
                    }

                    ImGui.EndMainMenuBar();
                }

            if (!window.WindowOpen)
                continue;

            currentlyRenderingWindow = window;

            Profiler.Begin(window.WindowName.ToKebabCase());

            if (ImGui.Begin(window.WindowName, ref window.WindowOpen, window.Flags)) window.Update();

            ImGui.End();

            Profiler.End(window.WindowName.ToKebabCase());

            currentlyRenderingWindow = null;
        }

        Profiler.End("Rendering Windows");
    }

    /// <summary>
    ///     Get the loaded instance of a specific window
    /// </summary>
    /// <typeparam name="T">Type of the window you want to get</typeparam>
    /// <returns>The instance of the window</returns>
    public static WindowAttribute? GetWindow<T>() where T : class
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
    public static T? GetWindowClass<T>() where T : class
    {
        return windows.FirstOrDefault(window => window.TargetClass.GetType() == typeof(T))!.TargetClass as T;
    }

    /// <summary>
    /// Opens a target window
    /// </summary>
    /// <typeparam name="T">Type of the window</typeparam>
    public static void ShowWindow<T>() where T : class
    {
        GetWindow<T>()!.WindowOpen = true;
    }

    /// <summary>
    /// Hides a target window
    /// </summary>
    /// <typeparam name="T">Type of the window</typeparam>
    public static void HideWindow<T>() where T : class
    {
        GetWindow<T>()!.WindowOpen = false;
    }
}