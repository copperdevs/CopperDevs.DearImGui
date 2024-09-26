namespace CopperDevs.DearImGui.Attributes;

/// <summary>
/// Attribute for deciding if a class should a window
/// </summary>
/// <param name="windowName">Display name of the window</param>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class WindowAttribute(string windowName) : Attribute
{
    /// <summary>
    /// Name of the window
    /// </summary>
    public readonly string WindowName = windowName;
    
    /// <summary>
    /// Currently state of window 
    /// </summary>
    public bool WindowOpen = false;

    internal object TargetClass = null!;

    private bool startMethodFound;
    private MethodInfo? startMethod;

    private bool updateMethodFound;
    private MethodInfo? updateMethod;

    private bool stopMethodFound;
    private MethodInfo? stopMethod;

    internal void GetMethods(object target)
    {
        TargetClass = target;

        startMethod = target.GetType().GetMethod("WindowStart");
        startMethodFound = startMethod is not null;

        updateMethod = target.GetType().GetMethod("WindowUpdate");
        updateMethodFound = updateMethod is not null;

        stopMethod = target.GetType().GetMethod("WindowStop");
        stopMethodFound = stopMethod is not null;
    }

    internal void Start()
    {
        if (startMethodFound)
            startMethod?.Invoke(TargetClass, []);
    }

    internal void Update()
    {
        if (updateMethodFound)
            updateMethod?.Invoke(TargetClass, []);
    }

    internal void Stop()
    {
        if (stopMethodFound)
            stopMethod?.Invoke(TargetClass, []);
    }
}