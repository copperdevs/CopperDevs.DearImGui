using System.Diagnostics.CodeAnalysis;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.DearImGui.ReflectionRenderers;
using CopperDevs.DearImGui.Utility;

namespace CopperDevs.DearImGui;

[SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
[SuppressMessage("ReSharper", "AccessToModifiedClosure")]
internal static class ImGuiReflection
{
    internal static readonly Dictionary<Type, FieldRenderer> ImGuiRenderers = new();

    internal static FieldRenderer? GetImGuiRenderer<T>()
    {
        return ImGuiRenderers.GetValueOrDefault(typeof(T));
    }

    private static readonly Dictionary<Type, List<FieldInfo>> FieldInfoTypeDictionary = [];

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
                Render();

            var currentTooltipAttribute =
                (TooltipAttribute)Attribute.GetCustomAttribute(info, typeof(TooltipAttribute))!;

            if (currentTooltipAttribute is null)
                continue;

            CopperImGui.Tooltip(currentTooltipAttribute.Message);

            continue;

            void Render()
            {
                var isList = info.FieldType is { IsGenericType: true } &&
                             info.FieldType.GetGenericTypeDefinition() == typeof(List<>);

                if (info.FieldType.IsEnum)
                {
                    ImGuiRenderers[typeof(Enum)].ReflectionRenderer(info, component, id, valueChanged);
                }
                else if (isList)
                {
                    ListRenderer(info, component, id);
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
        }
    }

    private static void ListRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (IList)fieldInfo.GetValue(component)!;

        // foreach (var type in value.GetType().GenericTypeArguments)
        // {
        // CopperImGui.Text(type.FullName);
        // }

        CopperImGui.CollapsingHeader($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", () =>
        {
            CopperImGui.HorizontalGroup(() => { CopperImGui.Text($"{value.Count} Items"); },
                () =>
                {
                    CopperImGui.Button($"+##{fieldInfo.Name}{id}",
                        () => { value.Add(value.Count > 0 ? value[^1] : Activator.CreateInstance(value.GetType().GenericTypeArguments[0])); });
                },
                () => { CopperImGui.Button($"-##{fieldInfo.Name}{id}", () => value.RemoveAt(value.Count - 1)); });

            CopperImGui.Separator();

            for (var i = 0; i < value.Count; i++)
            {
                var item = value[i];

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var itemType = item.GetType();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                if (itemType.IsEnum)
                {
                    ImGuiRenderers[typeof(Enum)].ValueRenderer(ref item, id);
                }
                else if (ImGuiRenderers.TryGetValue(itemType, out var renderer))
                {
                    renderer.ValueRenderer(ref item, int.Parse($"{i}{id}"));
                }
                else
                {
                    try
                    {
                        CopperImGui.CollapsingHeader($"{item.GetType().Name}##{value.IndexOf(item)}",
                            () => { RenderValues(item, (int)MathUtil.Clamp(float.Parse($"{value.IndexOf(item)}{i}{id}"), int.MinValue, int.MaxValue)); });
                    }
                    catch (Exception e)
                    {
                        Log.Exception(e);
                    }
                }

                value[i] = item;
            }
        });

        fieldInfo.SetValue(component, value);
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