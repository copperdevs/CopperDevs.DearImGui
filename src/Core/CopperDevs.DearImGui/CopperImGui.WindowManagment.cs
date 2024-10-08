﻿using CopperDevs.DearImGui.Attributes;
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

            if (!ImGui.Begin(window.WindowName, ref window.WindowOpen, window.Flags))
                continue;

            window.Update();
            ImGui.End();

            currentlyRenderingWindow = null;
        }
    }

    /// <summary>
    ///     Get the loaded instance of a specific window
    /// </summary>
    /// <typeparam name="T">Type of the window you want to get</typeparam>
    /// <returns>The instance of the window</returns>
    public static T? GetWindow<T>()
    {
        foreach (var window in windows.Where(window => window.TargetClass.GetType() == typeof(T))) return (T)window.TargetClass;

        return default;
    }
}