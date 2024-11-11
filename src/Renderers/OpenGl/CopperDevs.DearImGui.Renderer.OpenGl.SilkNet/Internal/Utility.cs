using System.Reflection;
using CopperDevs.Logger;

namespace CopperDevs.DearImGui.Renderer.OpenGl.SilkNet.Internal;

internal static class Utility
{
    public static void CallPrivateStaticMethod(Type targetClass, string methodName)
    {
        var dynMethod = targetClass.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic)!;
        dynMethod.Invoke(null, []);
    }
}