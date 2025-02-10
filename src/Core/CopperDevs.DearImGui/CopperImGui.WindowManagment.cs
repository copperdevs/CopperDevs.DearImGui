using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Utility;
using Hexa.NET.ImGui;
using WindowCreationData = (string, bool, Hexa.NET.ImGui.ImGuiWindowFlags);

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    private static readonly Dictionary<Guid, InternalWindowData> Windows = [];

    private static Guid currentlyRenderingWindow;

    internal static WindowCreationData CurrentlyCreatingWindowData;

    public static Guid GetWindowId<T>() where T : Window, new() => GetWindowId(typeof(T));
    public static void AddWindow<T>() where T : Window, new() => AddWindow(typeof(T));
    public static void RemoveWindow<T>() where T : Window, new() => RemoveWindow(typeof(T));
    public static void RemoveWindow(Window window) => RemoveWindow(window.GetType());
    public static bool ContainsWindowType<T>() where T : Window, new() => WindowsContainsType(typeof(T));
    public static T? GetWindowInstance<T>() where T : Window, new() => (T?)GetWindowInstance(typeof(T));
    public static bool IsWindowOpen<T>() where T : Window, new() => IsWindowOpen(typeof(T));
    public static bool IsWindowOpen(Guid id) => Windows[id].IsOpen;


    private static bool CurrentlyRenderingWindowHasFlag(ImGuiWindowFlags flag) => Windows[currentlyRenderingWindow].WindowFlags.HasFlag(flag);
    private static ImGuiWindowFlags GetWindowFlags(Guid id) => Windows[id].WindowFlags;
    private static string GetWindowTitle(Guid id) => Windows[id].Title;
    private static bool IsWindowOpen(Type type) => Windows[GetWindowId(type)].IsOpen;
    private static Guid GetWindowId(Type type) => Windows.FirstOrDefault(x => x.Value.Type == type, new KeyValuePair<Guid, InternalWindowData>(Guid.Empty, null!)).Key;
    private static bool WindowsContainsType(Type type) => Windows.Values.Any(window => window.Type == type);

    private static void AddWindow(Type type)
    {
        if (type == typeof(Window))
            return;

        if (WindowsContainsType(type))
        {
            Log.Warning($"Trying to add window of type {type}, even though it was already added.");
            return;
        }

        if (type.HasAttribute<DebugOnlyAttribute>() && IsDebug)
            return;

        if (type.HasAttribute<DisabledAttribute>())
        {
            if (!UseReflectionForWindows)
                Log.Warning($"Trying to add window of type {type}, even though it was disabled.");
            return;
        }

        var createdWindow = (Window)Activator.CreateInstance(type)!;

        Windows.Add(createdWindow.GetId(), new InternalWindowData(createdWindow, type, CurrentlyCreatingWindowData));


        createdWindow.OnLoad();
    }

    private static void RemoveWindow(Type type)
    {
        if (type == typeof(Window))
            return;

        var id = GetWindowId(type);

        if (id == Guid.Empty)
            return;

        Windows[id].TargetWindow.Shutdown();

        Windows.Remove(id);
    }

    private static Window? GetWindowInstance(Type type)
    {
        if (type == typeof(Window))
            return null;

        if (!WindowsContainsType(type))
        {
            Log.Warning($"Trying to get window instance of type {type}, even though it wasn't added.");
            return null;
        }

        var id = GetWindowId(type);

        return id == Guid.Empty ? null : Windows[id].TargetWindow;
    }

    private static void LoadAllWindowsWithReflection()
    {
        if (!UseReflectionForWindows)
            return;

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        foreach (var type in assembly.GetTypes())
        {
            if (!type.IsAssignableTo(typeof(Window)))
                continue;

            AddWindow(type);
        }
    }

    private static void ShutdownAllWindows()
    {
        foreach (var window in Windows.Values)
        {
            RemoveWindow(window.TargetWindow);
        }
    }

    private static void RenderWindows()
    {
        foreach (var window in Windows.Values)
        {
            if (ShowWindowsOnMenuBar)
                if (ImGui.BeginMainMenuBar())
                {
                    if (ImGui.BeginMenu("Windows"))
                    {
                        ImGui.MenuItem(window.Title, string.Empty, ref window.IsOpen);
                        ImGui.EndMenu();
                    }

                    ImGui.EndMainMenuBar();
                }

            if (!window.IsOpen)
                continue;

            currentlyRenderingWindow = window.Id;

            if (ImGui.Begin(window.Title, ref window.IsOpen, window.WindowFlags))
                window.TargetWindow.Render();

            ImGui.End();

            currentlyRenderingWindow = Guid.Empty;
        }
    }

    private class InternalWindowData(Window window, Type type, WindowCreationData creationData)
    {
        public readonly Window TargetWindow = window;
        public readonly Type Type = type;

        public readonly string Title = creationData.Item1;
        public readonly ImGuiWindowFlags WindowFlags = creationData.Item3;

        public bool IsOpen = creationData.Item2;

        public Guid Id => TargetWindow.GetId();
    }
}