using CopperDevs.Core.Data;
using CopperDevs.DearImGui.ReflectionRenderers;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    static CopperImGui()
    {
        RegisterFieldRenderer<bool, BoolFieldRenderer>();
        RegisterFieldRenderer<Enum, EnumFieldRenderer>();
        RegisterFieldRenderer<float, FloatFieldRenderer>();
        RegisterFieldRenderer<Guid, GuidFieldRenderer>();
        RegisterFieldRenderer<int, IntFieldRenderer>();
        RegisterFieldRenderer<Quaternion, QuaternionFieldRenderer>();
        RegisterFieldRenderer<string, StringFieldRenderer>();
        RegisterFieldRenderer<Vector2, Vector2FieldRenderer>();
        RegisterFieldRenderer<Vector2Int, Vector2IntFieldRenderer>();
        RegisterFieldRenderer<Vector4, Vector4FieldRenderer>();
        RegisterFieldRenderer<Vector3, Vector3FieldRenderer>();
    }

    public static void RenderObjectValues<TTargetType>(TTargetType component, int id = 0, RenderingType renderingType = RenderingType.All, Action valueChanged = null!)
    {
        ImGuiReflection.RenderValues(component, id, renderingType, valueChanged);
    }

    public static void RenderObjectValues<TTargetType>(ref TTargetType targetObject, int id = 0, RenderingType renderingType = RenderingType.All, Action valueChanged = null!)
    {
        var renderer = ImGuiReflection.GetImGuiRenderer<TTargetType>();

        if (renderer is not null)
        {
            var targetObjectCasted = (object)targetObject!;
            renderer.ValueRenderer(ref targetObjectCasted, id, valueChanged);
            targetObject = (TTargetType)targetObjectCasted;
        }
        else
        {
            ImGuiReflection.RenderValues(targetObject!, id, renderingType, valueChanged);
        }
    }

    public static FieldRenderer? GetFieldRenderer<T>()
    {
        return ImGuiReflection.GetImGuiRenderer<T>();
    }

    public static void RegisterFieldRenderer<TType, TRenderer>() where TRenderer : FieldRenderer, new()
    {
        ImGuiReflection.ImGuiRenderers.TryAdd(typeof(TType), new TRenderer());
    }

    public static Dictionary<Type, FieldRenderer> GetAllImGuiRenderers()
    {
        return ImGuiReflection.ImGuiRenderers;
    }
}