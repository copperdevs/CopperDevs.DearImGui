﻿using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Utility;
using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    private static WindowData? currentlyRenderingWindow = null!;

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

            if (Attribute.GetCustomAttribute(type, typeof(DebugOnlyAttribute)) is not null && !IsDebug)
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
            if (ShowTabBar)
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

            if (ImGui.Begin(window.WindowName, ref window.WindowOpen, window.WindowFlags)) window.UpdateWindow();

            ImGui.End();

            currentlyRenderingWindow = null;
        }
    }

    /// <summary>
    ///     Get the loaded instance of a specific window
    /// </summary>
    /// <typeparam name="T">Type of the window you want to get</typeparam>
    /// <returns>The instance of the window</returns>
    public static WindowData? GetWindow<T>() where T : class
    {
        return windows.FirstOrDefault(window => window.TargetClass.GetType() == typeof(T));
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
        GetWindow<T>()!.Value.WindowOpen = true;
    }

    /// <summary>
    /// Hides a target window
    /// </summary>
    /// <typeparam name="T">Type of the window</typeparam>
    public static void HideWindow<T>() where T : class
    {
        GetWindow<T>()!.Value.WindowOpen = true;
    }

    /// <summary>
    /// Load a window to be rendered
    /// </summary>
    /// <typeparam name="T">Type of window to load</typeparam>
    public static void RegisterWindow<T>() where T : BaseWindow, new()
    {
        var window = new WindowData(new T());
        window.StartWindow();
        windows.Add(window);
    }
}