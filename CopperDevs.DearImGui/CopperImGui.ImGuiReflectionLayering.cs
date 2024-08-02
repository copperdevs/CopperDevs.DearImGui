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

    /// <summary>
    /// Render all fields with ImGui using any registered <see cref="CopperDevs.DearImGui.ReflectionRenderers.FieldRenderer"/>.
    /// This method goes straight to using reflection, instead of attempting to use and registered <see cref="CopperDevs.DearImGui.ReflectionRenderers.FieldRenderer"/>
    /// </summary>
    /// <param name="component">Target object to render the fields of</param>
    /// <param name="id">Id of the object (Can usually be left as zero)</param>
    /// <param name="renderingType">Which fields of the object to renderer</param>
    /// <param name="valueChanged">Callback for when any field is changed</param>
    /// <typeparam name="TTargetType">The type of the object to render</typeparam>
    public static void RenderObjectValues<TTargetType>(TTargetType component, int id = 0, RenderingType renderingType = RenderingType.All, Action valueChanged = null!)
    {
        if (component is not null)
            ImGuiReflection.RenderValues(component, id, renderingType, valueChanged);
    }

    /// <summary>
    /// Render all fields with ImGui using any registered <see cref="CopperDevs.DearImGui.ReflectionRenderers.FieldRenderer"/>.
    /// This method attempts to use a registered <see cref="CopperDevs.DearImGui.ReflectionRenderers.FieldRenderer"/> before breaking the object down into its individual fields to render using other registered field renderers
    /// </summary>
    /// <param name="targetObject">Target object to render the fields of</param>
    /// <param name="id">Id of the object (Can usually be left as zero)</param>
    /// <param name="renderingType">Which fields of the object to renderer</param>
    /// <param name="valueChanged">Callback for when any field is changed</param>
    /// <typeparam name="TTargetType">The type of the object to render</typeparam>
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

    /// <summary>
    /// Get the created <see cref="CopperDevs.DearImGui.ReflectionRenderers.FieldRenderer"/> instance
    /// </summary>
    /// <typeparam name="T">The type the <see cref="CopperDevs.DearImGui.ReflectionRenderers.FieldRenderer"/> is assigned to render</typeparam>
    /// <returns>The created instance of the class</returns>
    public static FieldRenderer? GetFieldRenderer<T>()
    {
        return ImGuiReflection.GetImGuiRenderer<T>();
    }

    /// <summary>
    /// Register a new <see cref="CopperDevs.DearImGui.ReflectionRenderers.FieldRenderer"/>
    /// </summary>
    /// <typeparam name="TType">The type the <see cref="CopperDevs.DearImGui.ReflectionRenderers.FieldRenderer"/> is assigned to render</typeparam>
    /// <typeparam name="TRenderer">The actual <see cref="CopperDevs.DearImGui.ReflectionRenderers.FieldRenderer"/> class</typeparam>
    public static void RegisterFieldRenderer<TType, TRenderer>() where TRenderer : FieldRenderer, new()
    {
        ImGuiReflection.ImGuiRenderers.TryAdd(typeof(TType), new TRenderer());
    }

    /// <summary>
    /// Get all currently registered <see cref="CopperDevs.DearImGui.ReflectionRenderers.FieldRenderer"/>
    /// </summary>
    /// <returns>Every registered <see cref="CopperDevs.DearImGui.ReflectionRenderers.FieldRenderer"/>, with the key being the type it is rendering</returns>
    public static Dictionary<Type, FieldRenderer> GetAllImGuiRenderers()
    {
        return ImGuiReflection.ImGuiRenderers;
    }
}