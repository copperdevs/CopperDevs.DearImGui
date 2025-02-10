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

    /// <summary>
    /// Get the <see cref="Guid"/> of a window from its type
    /// </summary>
    /// <typeparam name="T">Type of the window to check</typeparam>
    /// <returns>The found <see cref="Guid"/> of the window. Returns null if no window with the type was found</returns>
    public static Guid GetWindowId<T>() where T : Window, new() => GetWindowId(typeof(T));

    /// <summary>
    /// Add a new window
    /// </summary>
    /// <typeparam name="T">Type of the window to add</typeparam>
    public static T AddWindow<T>() where T : Window, new() => (T)AddWindow(typeof(T));

    /// <summary>
    /// Remove a window
    /// </summary>
    /// <typeparam name="T">Type of the window to remove</typeparam>
    public static void RemoveWindow<T>() where T : Window, new() => RemoveWindow(typeof(T));

    /// <summary>
    /// Remove an already created window
    /// </summary>
    /// <param name="window">Window object</param>
    public static void RemoveWindow(Window window) => RemoveWindow(window.GetType());

    /// <summary>
    /// Checks if a window type has been added
    /// </summary>
    /// <typeparam name="T">Type of the window to check</typeparam>
    /// <returns>True if the window was found</returns>
    public static bool ContainsWindowType<T>() where T : Window, new() => WindowsContainsType(typeof(T));

    /// <summary>
    /// Get the created window from its type
    /// </summary>
    /// <typeparam name="T">Type of the window to check</typeparam>
    /// <returns></returns>
    public static T? GetWindowInstance<T>() where T : Window, new() => (T?)GetWindowInstance(typeof(T));

    /// <summary>
    /// Checks if a window is currently open
    /// </summary>
    /// <typeparam name="T">Type of the window to check</typeparam>
    /// <returns>True if the window is open</returns>
    public static bool IsWindowOpen<T>() where T : Window, new() => IsWindowOpen(typeof(T));

    /// <summary>
    /// Checks if a window is currently open
    /// </summary>
    /// <param name="id"><see cref="Guid"/> of the window to check</param>
    /// <returns>True if the window is open</returns>
    public static bool IsWindowOpen(Guid id) => Windows[id].IsOpen;

    private static bool CurrentlyRenderingWindowHasFlag(ImGuiWindowFlags flag) => Windows[currentlyRenderingWindow].WindowFlags.HasFlag(flag);
    private static string GetWindowTitle(Guid id) => Windows[id].Title;
    private static bool IsWindowOpen(Type type) => Windows[GetWindowId(type)].IsOpen;
    private static Guid GetWindowId(Type type) => Windows.FirstOrDefault(x => x.Value.Type == type, new KeyValuePair<Guid, InternalWindowData>(Guid.Empty, null!)).Key;
    private static bool WindowsContainsType(Type type) => Windows.Values.Any(window => window.Type == type);

    private static Window AddWindow(Type type)
    {
        if (type == typeof(Window))
            return null!;

        if (WindowsContainsType(type))
        {
            Log.Warning($"Trying to add window of type {type}, even though it was already added.");
            return null!;
        }

        if (type.HasAttribute<DebugOnlyAttribute>() && IsDebug)
            return null!;

        if (type.HasAttribute<DisabledAttribute>())
        {
            if (!UseReflectionForWindows)
                Log.Warning($"Trying to add window of type {type}, even though it was disabled.");
            return null!;
        }

        var createdWindow = (Window)Activator.CreateInstance(type)!;

        Windows.Add(createdWindow.GetId(), new InternalWindowData(createdWindow, type, CurrentlyCreatingWindowData));


        createdWindow.OnLoad();

        return createdWindow;
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