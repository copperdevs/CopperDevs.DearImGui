namespace CopperDevs.DearImGui.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class WindowAttribute(string windowName) : Attribute
{
    public readonly string WindowName = windowName;
    public bool WindowOpen = false;

    internal object targetClass = null!;

    private bool startMethodFound;
    private MethodInfo? startMethod;

    private bool updateMethodFound;
    private MethodInfo? updateMethod;

    private bool stopMethodFound;
    private MethodInfo? stopMethod;

    internal void GetMethods(object target)
    {
        targetClass = target;

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
            startMethod?.Invoke(targetClass, []);
    }

    internal void Update()
    {
        if (updateMethodFound)
            updateMethod?.Invoke(targetClass, []);
    }

    internal void Stop()
    {
        if (stopMethodFound)
            stopMethod?.Invoke(targetClass, []);
    }
}