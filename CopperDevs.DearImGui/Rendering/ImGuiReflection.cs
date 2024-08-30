using System.Diagnostics.CodeAnalysis;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.DearImGui.Rendering.Renderers;
using CopperDevs.DearImGui.Utility;
using CopperDevs.Logger;

namespace CopperDevs.DearImGui.Rendering;

[SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
[SuppressMessage("ReSharper", "AccessToModifiedClosure")]
internal static class ImGuiReflection
{
    private static readonly Dictionary<Type, FieldRenderer> ImGuiRenderers = new();
    private static readonly Dictionary<Type, List<FieldInfo>> FieldInfoTypeDictionary = [];

    internal static void RegisterFieldRenderer<TType, TRenderer>() where TRenderer : FieldRenderer, new()
    {
        ImGuiRenderers.TryAdd(typeof(TType), new TRenderer());
    }

    internal static FieldRenderer? GetImGuiRenderer<T>()
    {
        return ImGuiRenderers.GetValueOrDefault(typeof(T));
    }

    internal static Dictionary<Type, FieldRenderer> GetAllImGuiRenderers()
    {
        return ImGuiRenderers.ToDictionary();
    }

    internal static bool TryGetImGuiRenderer<T>(out FieldRenderer? value)
    {
        return TryGetImGuiRenderer(typeof(T), out value);
    }

    internal static bool TryGetImGuiRenderer(Type type, out FieldRenderer? value)
    {
        if (ImGuiRenderers.ContainsKey(type))
        {
            value = ImGuiRenderers.GetValueOrDefault(type);
            return true;
        }

        value = null;
        return false;
    }

    internal static void RenderValues(object component, int id = 0, RenderingType renderingType = RenderingType.All, Action valueChanged = null!)
    {
        var bindingFlags = renderingType switch
        {
            RenderingType.Public => BindingFlags.Instance | BindingFlags.Public,
            RenderingType.Exposed => BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            RenderingType.All => BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            _ => throw new ArgumentOutOfRangeException(nameof(renderingType), renderingType, null)
        };

        var valueCached = FieldInfoTypeDictionary.TryGetValue(component.GetType(), out var value);

        if (!valueCached)
            FieldInfoTypeDictionary.TryAdd(component.GetType(), component.GetType().GetFields(bindingFlags).ToList());

        var fields = valueCached ? value : FieldInfoTypeDictionary[component.GetType()];

        if (fields is null)
            return;

        foreach (var info in fields)
        {
            if (renderingType == RenderingType.Exposed && !info.IsPublic)
            {
                if (Attribute.GetCustomAttribute(info, typeof(ExposedAttribute)) is null)
                    continue;
            }

            SpaceAttributeRenderer(info);
            SeperatorAttributeRenderer(info);

            if (Attribute.GetCustomAttribute(info, typeof(HideInInspectorAttribute)) is not null)
                continue;

            var currentReadOnlyAttribute =
                (ReadOnlyAttribute?)Attribute.GetCustomAttribute(info, typeof(ReadOnlyAttribute))!;

            using (new DisabledScope(currentReadOnlyAttribute is not null))
                Render(info, component, id, valueChanged, renderingType);

            var currentTooltipAttribute =
                (TooltipAttribute)Attribute.GetCustomAttribute(info, typeof(TooltipAttribute))!;

            if (currentTooltipAttribute is null)
                continue;

            CopperImGui.Tooltip(currentTooltipAttribute.Message);
        }
    }


    private static void Render(FieldInfo info, object component, int id, Action valueChanged, RenderingType renderingType)
    {
        var isList = info.FieldType is { IsGenericType: true } &&
                     info.FieldType.GetGenericTypeDefinition() == typeof(List<>);

        if (info.FieldType.IsEnum)
        {
            ImGuiRenderers[typeof(Enum)].ReflectionRenderer(info, component, id, valueChanged);
        }
        else if (isList)
        {
            ListRenderer.Render(info, component, id);
        }
        else
        {
            if (ImGuiRenderers.TryGetValue(info.FieldType, out var renderer))
                renderer.ReflectionRenderer(info, component, id, valueChanged);
            else
            {
                try
                {
                    CopperImGui.CollapsingHeader($"{info.Name.ToTitleCase()}##{id + 1}", () =>
                    {
                        var subComponent = info.GetValue(component);
                        if (subComponent is null)
                            return;

                        RenderValues(subComponent, id + info.GetHashCode(), renderingType, valueChanged);
                        info.SetValue(component, subComponent);
                    });
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                    CopperImGui.Text(info.FieldType.FullName!, "Unsupported editor value");
                }
            }
        }
    }

    private static void SpaceAttributeRenderer(MemberInfo info)
    {
        ((SpaceAttribute?)Attribute.GetCustomAttribute(info, typeof(SpaceAttribute)))?.Render();
    }

    private static void SeperatorAttributeRenderer(MemberInfo info)
    {
        ((SeperatorAttribute?)Attribute.GetCustomAttribute(info, typeof(SeperatorAttribute)))?.Render();
    }
}