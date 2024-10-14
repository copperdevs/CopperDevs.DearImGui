using System.Diagnostics;
using CopperDevs.Logger;
using Silk.NET.OpenGL;

namespace CopperDevs.DearImGui.Renderer.OpenGl.SilkNet.Internal;

internal static class Extensions
{
    [Conditional("DEBUG")]
    public static void CheckGlError(this GL gl, string title)
    {
        var error = gl.GetError();

        if (error != GLEnum.NoError)
            Log.Error($"{title}: {error}");
    }
}