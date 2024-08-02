using CopperDevs.Core.Data;
using CopperDevs.DearImGui.Utility;
using ImGuiNET;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    public static void Separator()
    {
        if (canRender)
            ImGui.SeparatorText("");
    }

    public static void Separator(string text)
    {
        if (canRender)
            ImGui.SeparatorText(text);
    }

    public static void Space()
    {
        if (canRender)
            ImGui.Dummy(tempVec with { Y = 20 });
    }

    public static void Space(float amount)
    {
        if (canRender)
            ImGui.Dummy(tempVec with { Y = amount });
    }

    public static void HorizontalGroup(params Action[] items)
    {
        if (!canRender)
            return;

        foreach (var action in items)
        {
            action.Invoke();
            ImGui.SameLine();
        }

        ImGui.Dummy(tempVec with { X = 0, Y = 0 });
    }

    public static void Group(string id, Action group, float height = 0, float width = 0)
    {
        if (canRender)
            Group(id, group, ImGuiChildFlags.None, height, width);
    }

    public static void Group(string id, Action group, ImGuiChildFlags flags, float height = 0, float width = 0)
    {
        if (!canRender)
            return;
        if (!ImGui.BeginChild(id, tempVec with { X = width, Y = height }, flags))
            return;

        group.Invoke();
        ImGui.EndChild();
    }

    public static void Selectable(object text, Action? clickEvent = null)
    {
        if (canRender)
            Selectable($"{text}", false, clickEvent);
    }

    public static void Selectable(string text, bool enabled, Action? clickEvent = null)
    {
        if (!canRender)
            return;
        if (ImGui.Selectable(text, enabled))
            clickEvent?.Invoke();
    }

    public static void Text(object value, string title)
    {
        if (canRender)
            ImGui.LabelText(title, $"{value}");
    }

    public static void Text(object? value)
    {
        if (canRender)
            ImGui.Text($"{value}");
    }

    public static bool DragValue(string name, ref Matrix4x4 matrix)
    {
        if (!canRender)
            return false;

        var interacted = false;

        var temp = matrix;

        CollapsingHeader(name, () =>
        {
            using (new IndentScope())
            {
                interacted =
                    DragMatrix4X4Row($"Row One##{name}", ref temp.M11, ref temp.M12, ref temp.M13,
                        ref temp.M14) ||
                    DragMatrix4X4Row($"Row Two##{name}", ref temp.M21, ref temp.M22, ref temp.M23,
                        ref temp.M24) ||
                    DragMatrix4X4Row($"Row Three##{name}", ref temp.M31, ref temp.M32, ref temp.M33,
                        ref temp.M34) ||
                    DragMatrix4X4Row($"Row Four##{name}", ref temp.M41, ref temp.M42, ref temp.M43,
                        ref temp.M44);
            }
        });

        if (interacted)
            matrix = temp;

        return interacted;

        bool DragMatrix4X4Row(string rowName, ref float itemOne, ref float itemTwo, ref float itemThree,
            ref float itemFour)
        {
            var rowInteracted = false;
            var row = new Vector4(itemOne, itemTwo, itemThree, itemFour);

            if (!ImGui.DragFloat4(rowName, ref row))
                return rowInteracted;

            rowInteracted = true;
            itemOne = row.X;
            itemTwo = row.Y;
            itemThree = row.Z;
            itemFour = row.W;

            return rowInteracted;
        }
    }

    public static void Tooltip(string message)
    {
        if (!canRender)
            return;

        if (!ImGui.BeginItemTooltip())
            return;

        ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);

        ImGui.TextUnformatted(message);

        ImGui.PopTextWrapPos();

        ImGui.EndTooltip();
    }

    public static void CollapsingHeader(string name, Action group, bool indent = true)
    {
        if (!canRender)
            return;

        if (!ImGui.CollapsingHeader(name))
            return;

        using (new IndentScope(indent))
            group.Invoke();
    }

    public static void Button(string name, Action? clickEvent = null)
    {
        if (!canRender)
            return;

        if (ImGui.Button(name))
            clickEvent?.Invoke();
    }

    public static void Button(string name, float width, float height = 0, Action? clickEvent = null)
    {
        if (!canRender)
            return;

        if (ImGui.Button(name, tempVec with { X = width, Y = height }))
            clickEvent?.Invoke();
    }

    public static void Checkbox(string name, ref bool currentValue, Action<bool>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.Checkbox(name, ref currentValue))
            interacted?.Invoke(currentValue);
    }

    public static void ColorEdit(string name, ref Vector4 color, Action<Vector4>? interacted = null)
    {
        if (!canRender)
            return;

        var vectorColor = color;

        if (!ImGui.ColorEdit4(name, ref vectorColor))
            return;

        interacted?.Invoke(vectorColor);
    }

    public static void DragValue(string name, ref float value, Action<float>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragFloat(name, ref value))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref float value, float speed, float min, float max,
        Action<float>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragFloat(name, ref value, speed, min, max))
            interacted?.Invoke(value);
    }

    public static void SliderValue(string name, ref float value, float min, float max,
        Action<float>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.SliderFloat(name, ref value, min, max))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref Vector2 value, Action<Vector2>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragFloat2(name, ref value))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref Vector2 value, float speed, float min, float max,
        Action<Vector2>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragFloat2(name, ref value, speed, min, max))
            interacted?.Invoke(value);
    }

    public static void SliderValue(string name, ref Vector2 value, float min, float max,
        Action<Vector2>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.SliderFloat2(name, ref value, min, max))
            interacted?.Invoke(value);
    }


    public static void DragValue(string name, ref Vector2Int value, int speed, int min, int max,
        Action<Vector2Int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragInt2(name, ref value.X, speed, min, max))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref Vector2Int value, Action<Vector2Int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragInt2(name, ref value.X))
            interacted?.Invoke(value);
    }

    public static void SliderValue(string name, ref Vector2Int value, int min, int max,
        Action<Vector2Int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.SliderInt2(name, ref value.X, min, max))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref Vector3 value, Action<Vector3>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragFloat3(name, ref value))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref Vector3 value, float speed, float min, float max,
        Action<Vector3>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragFloat3(name, ref value, speed, min, max))
            interacted?.Invoke(value);
    }

    public static void SliderValue(string name, ref Vector3 value, float min, float max,
        Action<Vector3>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.SliderFloat3(name, ref value, min, max))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref Vector4 value, Action<Vector4>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragFloat4(name, ref value))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref Vector4 value, float speed, float min, float max,
        Action<Vector4>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragFloat4(name, ref value, speed, min, max))
            interacted?.Invoke(value);
    }

    public static void SliderValue(string name, ref Vector4 value, float min, float max,
        Action<Vector4>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.SliderFloat4(name, ref value, min, max))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref int value, Action<int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragInt(name, ref value))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref int value, int speed, int min, int max,
        Action<int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragInt(name, ref value, speed, min, max))
            interacted?.Invoke(value);
    }

    public static void SliderValue(string name, ref int value, int min, int max, Action<int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.SliderInt(name, ref value, min, max))
            interacted?.Invoke(value);
    }


    public static void Text(string name, ref string value, Action<string>? interacted = null!, uint maxLength = 64)
    {
        if (!canRender)
            return;

        if (ImGui.InputText(name, ref value, maxLength))
            interacted?.Invoke(value);
    }

    public static void TabGroup(string id, params (string, Action)[] tabs)
    {
        if (!canRender)
            return;

        if (!ImGui.BeginTabBar(id, ImGuiTabBarFlags.Reorderable))
            return;

        for (var i = 0; i < tabs.Length; i++)
        {
            var (tabTitle, tabAction) = tabs[i];

            if (!ImGui.BeginTabItem($"{tabTitle}###{id}{i}"))
                continue;

            tabAction?.Invoke();
            ImGui.EndTabItem();
        }

        ImGui.EndTabBar();
    }

    public static bool MenuItem(string text)
    {
        if (!canRender)
            return false;

        var temp = false;
        return MenuItem(text, ref temp);
    }

    public static bool MenuItem(string text, Action? action)
    {
        if (!canRender)
            return false;

        var temp = false;
        if (!MenuItem(text, ref temp))
            return false;

        action?.Invoke();
        return true;
    }

    public static bool MenuItem(string text, ref bool enabled)
    {
        return canRender && ImGui.MenuItem(text, null, ref enabled);
    }

    public static void MenuBar(params (string, Action)[] subMenus)
    {
        MenuBar(null!, false, subMenus);
    }

    public static void MenuBar(Action action = null!, bool isMainMenuBar = false, params (string, Action)[] subMenus)
    {
        if (isMainMenuBar ? !ImGui.BeginMainMenuBar() : !ImGui.BeginMenuBar())
            return;

        foreach (var subMenu in subMenus)
        {
            if (!ImGui.BeginMenu(subMenu.Item1))
                continue;

            subMenu.Item2?.Invoke();
            ImGui.EndMenu();
        }

        action?.Invoke();

        if (isMainMenuBar)
            ImGui.EndMainMenuBar();
        else
            ImGui.EndMenuBar();
    }

    public static void Window(string title, Action render, ImGuiWindowFlags flags = ImGuiWindowFlags.None)
    {
        if (!ImGui.Begin(title, flags)) 
            return;
        
        render.Invoke();
        ImGui.End();
    }

    public static void Window(string title, Action render, ref bool isOpen, ImGuiWindowFlags flags = ImGuiWindowFlags.None)
    {
        if (!isOpen) 
            return;
        
        if (!ImGui.Begin(title, ref isOpen, flags)) 
            return;
        
        render.Invoke();
        ImGui.End();
    }
}