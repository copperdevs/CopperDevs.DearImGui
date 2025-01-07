using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Utility;
using Hexa.NET.ImGui;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    private static Dictionary<Guid, Window> Windows = [];
    private static Dictionary<Guid, Type> WindowsTypes = [];
    private static Dictionary<Guid, ImGuiWindowSettings> WindowSettings = [];
    private static Dictionary<Guid, bool> WindowsVisibility = [];
    
    public static Guid GetWindowId<T>() where T : Window, new()
    {
        return WindowsTypes.FirstOrDefault(x => x.Value == typeof(T), new KeyValuePair<Guid, Type>(Guid.Empty, null!)).Key;
    }

    public static void AddWindow<T>() where T : Window, new()
    {
        if (UseReflectionForWindows)
        {
            Log.Error($"Manual window addition while ${RenderingSettings.ReflectionForWindows} is enabled");
            return;
        }

        if (typeof(T) == typeof(Window))
            return;

        if (WindowsTypes.ContainsValue(typeof(T)))
        {
            Log.Warning($"Trying to add window of type {typeof(T)}, even though it was already added.");
            return;
        }

        var createdWindow = new T();

        Windows.Add(createdWindow.GetId(), createdWindow);
        WindowsTypes.Add(createdWindow.GetId(), typeof(T));

        createdWindow.OnLoad();
    }

    public static void RemoveWindow<T>() where T : Window, new()
    {
        if (typeof(T) == typeof(Window))
            return;

        if (!WindowsTypes.ContainsValue(typeof(T)))
        {
            Log.Warning($"Trying to remove window of type {typeof(T)}, even though it wasn't added.");
            return;
        }

        var id = GetWindowId<T>();

        if (id == Guid.Empty)
            return;

        Windows[id].Shutdown();

        Windows.Remove(id);
        WindowsTypes.Remove(id);
    }

    public static T? GetWindowInstance<T>() where T : Window, new()
    {
        if (typeof(T) == typeof(Window))
            return null;

        if (!WindowsTypes.ContainsValue(typeof(T)))
        {
            Log.Warning($"Trying to get window instance of type {typeof(T)}, even though it wasn't added.");
            return null;
        }

        var id = GetWindowId<T>();

        if (id == Guid.Empty)
            return null;

        return (T?)Windows[id];
    }

    public static void SetWindowSettings(Guid windowId, ImGuiWindowSettings windowSettings)
    {
        if (!WindowSettings.TryAdd(windowId, windowSettings))
            WindowSettings[windowId] = windowSettings;
    }

    private static List<Window> LoadAllWindows()
    {
        if (!UseReflectionForWindows)
            return [];

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        var createdObjects = new List<Window>();

        foreach (var assembly in assemblies)
        foreach (var type in assembly.GetTypes())
        {
            if (!type.IsAssignableTo(typeof(Window)))
                continue;

            if (Attribute.GetCustomAttribute(type, typeof(DisabledAttribute)) is not null)
                continue;

            if (Attribute.GetCustomAttribute(type, typeof(DebugOnlyAttribute)) is not null && !IsDebug)
                continue;

            var createdWindow = Activator.CreateInstance(type);

            Log.Debug($"Found and loaded {type.FullName} window");
            createdObjects.Add((Window)createdWindow!);
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
}