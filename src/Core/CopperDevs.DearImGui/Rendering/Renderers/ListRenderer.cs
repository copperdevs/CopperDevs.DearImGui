using System.Diagnostics.CodeAnalysis;
using CopperDevs.Logger;

namespace CopperDevs.DearImGui.Rendering.Renderers;

[SuppressMessage("ReSharper", "AccessToModifiedClosure")]
internal static class ListRenderer
{
    internal static void Render(FieldInfo fieldInfo, object component, int id)
    {
        var value = (IList)fieldInfo.GetValue(component)!;

        CopperImGui.CollapsingHeader($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", () =>
        {
            CopperImGui.HorizontalGroup(() => { CopperImGui.Text($"{value.Count} Items"); },
                () =>
                {
                    CopperImGui.Button($"+##{fieldInfo.Name}{id}",
                        () => { value.Add(value.Count > 0 ? value[^1] : Activator.CreateInstance(value.GetType().GenericTypeArguments[0])); });
                },
                () =>
                {
                    CopperImGui.Button($"-##{fieldInfo.Name}{id}", () =>
                    {
                        if (value.Count - 1 > -1)
                            value.RemoveAt(value.Count - 1);
                    });
                });

            CopperImGui.Separator();

            for (var i = 0; i < value.Count; i++)
            {
                var item = value[i];

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var itemType = item.GetType();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                if (itemType.IsEnum)
                {
                    ImGuiReflection.GetImGuiRenderer<Enum>()?.ValueRenderer(ref item, id);
                }
                else if (ImGuiReflection.TryGetImGuiRenderer(itemType, out var renderer))
                {
                    renderer?.ValueRenderer(ref item, int.Parse($"{i}{id}"));
                }
                else
                {
                    try
                    {
                        CopperImGui.CollapsingHeader($"{item.GetType().Name}##{value.IndexOf(item)}",
                            () => { ImGuiReflection.RenderValues(item, (int)MathUtil.Clamp(float.Parse($"{value.IndexOf(item)}{i}{id}"), int.MinValue, int.MaxValue)); });
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
}