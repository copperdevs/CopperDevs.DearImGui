using System.Diagnostics.CodeAnalysis;
using CopperDevs.Core.Data;
using CopperDevs.DearImGui.Enums;
using CopperDevs.DearImGui.Utility;

namespace CopperDevs.DearImGui;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public static partial class CopperImGui
{
    /// <summary>
    /// Render a new horizontal line seperator
    /// </summary>
    public static void Separator()
    {
        if (canRender)
            CurrentBackend.SeparatorText("");
    }

    /// <summary>
    /// Render a new horizontal line seperator with text
    /// </summary>
    /// <param name="text">Text value to render with</param>
    public static void Separator(string text)
    {
        if (canRender)
            CurrentBackend.SeparatorText(text);
    }

    /// <summary>
    /// Render a new spacing area
    /// </summary>
    public static void Space()
    {
        if (canRender)
            CurrentBackend.Dummy(tempVec with { Y = 20 });
    }

    /// <summary>
    /// Render a new spacing area with a specific value
    /// </summary>
    /// <param name="amount">Amount of spacing</param>
    public static void Space(float amount)
    {
        if (canRender)
            CurrentBackend.Dummy(tempVec with { Y = amount });
    }

    /// <summary>
    /// Render a group of actions as a horizontal group
    /// </summary>
    /// <param name="items">Actions to render horizontally</param>
    public static void HorizontalGroup(params Action[] items)
    {
        if (!canRender)
            return;

        foreach (var action in items)
        {
            action.Invoke();
            CurrentBackend.SameLine();
        }

        CurrentBackend.Dummy(tempVec with { X = 0, Y = 0 });
    }

    /// <summary>
    /// Render a group of actions as a group
    /// </summary>
    /// <param name="id">Id of the group</param>
    /// <param name="group">Group action</param>
    /// <param name="height">Height of the group (Set as zero for it to fill as much vertical space as it can)</param>
    /// <param name="width">Width of the group (Set as zero for it to fill as much horizontal space as it can)</param>
    public static void Group(string id, Action group, float height = 0, float width = 0)
    {
        if (canRender)
            Group(id, group, ChildFlags.None, height, width);
    }

    /// <summary>
    /// Render a group of actions as a group
    /// </summary>
    /// <param name="id">Id of the group</param>
    /// <param name="group">Group action</param>
    /// <param name="flags">Any group flags you wish to use</param>
    /// <param name="height">Height of the group (Set as zero for it to fill as much vertical space as it can)</param>
    /// <param name="width">Width of the group (Set as zero for it to fill as much horizontal space as it can)</param>
    public static void Group(string id, Action group, ChildFlags flags, float height = 0, float width = 0)
    {
        if (!canRender)
            return;
        if (!CurrentBackend.BeginChild(id, tempVec with { X = width, Y = height }, flags))
            return;

        group.Invoke();
        CurrentBackend.EndChild();
    }

    /// <summary>
    /// Create a selectable region of text
    /// </summary>
    /// <param name="text">Interactable text value</param>
    /// <param name="clickEvent">Event invoked when the text is clicked</param>
    public static void Selectable(object text, Action? clickEvent = null)
    {
        if (canRender)
            Selectable($"{text}", false, clickEvent);
    }

    /// <summary>
    /// Create a selectable region of text
    /// </summary>
    /// <param name="text">Interactable text value</param>
    /// <param name="enabled">Decides if the text is disabled or not</param>
    /// <param name="clickEvent">Event invoked when the text is clicked</param>
    public static void Selectable(string text, bool enabled, Action? clickEvent = null)
    {
        if (!canRender)
            return;
        if (CurrentBackend.Selectable(text, enabled))
            clickEvent?.Invoke();
    }

    /// <summary>
    /// Render text with a label
    /// </summary>
    /// <param name="value">Text value</param>
    /// <param name="title">Label text</param>
    public static void Text(object value, string title)
    {
        if (canRender)
            CurrentBackend.LabelText(title, $"{value}");
    }

    /// <summary>
    /// Render text normally
    /// </summary>
    /// <param name="value">Text value</param>
    public static void Text(object? value)
    {
        if (canRender)
            CurrentBackend.Text($"{value}");
    }

    /// <summary>
    /// Render a 4x4 matrix with drag slider values
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="matrix">Target matrix</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
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
                    DragMatrix4X4Row($"Row One##{name}", ref temp.M11, ref temp.M12, ref temp.M13, ref temp.M14) ||
                    DragMatrix4X4Row($"Row Two##{name}", ref temp.M21, ref temp.M22, ref temp.M23, ref temp.M24) ||
                    DragMatrix4X4Row($"Row Three##{name}", ref temp.M31, ref temp.M32, ref temp.M33, ref temp.M34) ||
                    DragMatrix4X4Row($"Row Four##{name}", ref temp.M41, ref temp.M42, ref temp.M43, ref temp.M44);
            }
        });

        if (interacted)
            matrix = temp;

        return interacted;

        bool DragMatrix4X4Row(string rowName, ref float itemOne, ref float itemTwo, ref float itemThree, ref float itemFour)
        {
            var rowInteracted = false;
            var row = new Vector4(itemOne, itemTwo, itemThree, itemFour);

            if (!CurrentBackend.DragFloat4(rowName, ref row))
                return rowInteracted;

            rowInteracted = true;
            itemOne = row.X;
            itemTwo = row.Y;
            itemThree = row.Z;
            itemFour = row.W;

            return rowInteracted;
        }
    }

    /// <summary>
    /// Render a tooltip to the previously rendered item
    /// </summary>
    /// <param name="message">Text of the tooltip</param>
    public static void Tooltip(object message)
    {
        if (!canRender)
            return;

        if (!CurrentBackend.BeginItemTooltip())
            return;

        CurrentBackend.PushTextWrapPos(CurrentBackend.GetFontSize() * 35.0f);

        CurrentBackend.TextUnformatted(message.ToString());

        CurrentBackend.PopTextWrapPos();

        CurrentBackend.EndTooltip();
    }

    /// <summary>
    /// Create a section of ui that's under a collapsible header
    /// </summary>
    /// <param name="name">Name of the header</param>
    /// <param name="group">Section of ui to put under the header</param>
    /// <param name="indent">Should the ui be indented</param>
    public static void CollapsingHeader(string name, Action group, bool indent = true)
    {
        if (!canRender)
            return;

        if (!CurrentBackend.CollapsingHeader(name))
            return;

        using (new IndentScope(indent))
            group.Invoke();
    }

    /// <summary>
    /// Renders a button
    /// </summary>
    /// <param name="name">Name of the button</param>
    /// <param name="clickEvent">Event invoked when the button is clicked</param>
    public static void Button(string name, Action? clickEvent = null)
    {
        if (!canRender)
            return;

        if (CurrentBackend.Button(name))
            clickEvent?.Invoke();
    }

    /// <summary>
    /// Renders a button of a specific size
    /// </summary>
    /// <param name="name">Name of the button</param>
    /// <param name="width">Width of the button</param>
    /// <param name="height">Height of the button</param>
    /// <param name="clickEvent">Event invoked when the button is clicked</param>
    public static void Button(string name, float width, float height = 0, Action? clickEvent = null)
    {
        if (!canRender)
            return;

        if (CurrentBackend.Button(name, tempVec with { X = width, Y = height }))
            clickEvent?.Invoke();
    }

    /// <summary>
    /// Renders a new checkbox
    /// </summary>
    /// <param name="name">Name of the checkbox</param>
    /// <param name="currentValue">The value of the checkbox</param>
    /// <param name="interacted">Invoked when the checkbox has been interacted with. Contains the new state of the checkbox</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static bool Checkbox(string name, ref bool currentValue, Action<bool>? interacted = null!)
    {
        if (!canRender)
            return false;

        if (!CurrentBackend.Checkbox(name, ref currentValue))
            return false;

        interacted?.Invoke(currentValue);
        return true;
    }

    /// <summary>
    /// Renders a new color selector
    /// </summary>
    /// <param name="name">Name of the color selector field</param>
    /// <param name="color">Referenced color object</param>
    /// <param name="interacted">Invoked when the color selector has been interacted with. Contains the new value of the color</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static bool ColorEdit(string name, ref Vector4 color, Action<Vector4>? interacted = null)
    {
        if (!canRender)
            return false;

        if (!CurrentBackend.ColorEdit4(name, ref color))
            return false;

        interacted?.Invoke(color);
        return true;
    }

    /// <summary>
    /// Renders a float to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced float object</param>
    /// <param name="interacted">Invoked when the float selector has been interacted with. Contains the new value of the float</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void DragValue(string name, ref float value, Action<float>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.DragFloat(name, ref value))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a float to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced float object</param>
    /// <param name="speed">Amount of change when dragging the slider</param>
    /// <param name="min">Minimum range of the slider</param>
    /// <param name="max">Maximum range of the slider</param>
    /// <param name="interacted">Invoked when the float selector has been interacted with. Contains the new value of the float</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void DragValue(string name, ref float value, float speed, float min, float max, Action<float>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.DragFloat(name, ref value, speed, min, max))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a float to a normal slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced float object</param>
    /// <param name="min">Minimum range of the slider</param>
    /// <param name="max">Maximum range of the slider</param>
    /// <param name="interacted">Invoked when the float selector has been interacted with. Contains the new value of the float</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void SliderValue(string name, ref float value, float min, float max, Action<float>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.SliderFloat(name, ref value, min, max))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a Vector2 to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced vector2 object</param>
    /// <param name="interacted">Invoked when the vector2 selector has been interacted with. Contains the new value of the vector2</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void DragValue(string name, ref Vector2 value, Action<Vector2>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.DragFloat2(name, ref value))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a vector2 to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced vector2 object</param>
    /// <param name="speed">Amount of change when dragging the slider</param>
    /// <param name="min">Minimum range of the slider</param>
    /// <param name="max">Maximum range of the slider</param>
    /// <param name="interacted">Invoked when the vector2 selector has been interacted with. Contains the new value of the vector2</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void DragValue(string name, ref Vector2 value, float speed, float min, float max, Action<Vector2>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.DragFloat2(name, ref value, speed, min, max))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a vector2 to a normal slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced vector2 object</param>
    /// <param name="min">Minimum range of the slider</param>
    /// <param name="max">Maximum range of the slider</param>
    /// <param name="interacted">Invoked when the vector2 selector has been interacted with. Contains the new value of the vector2</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void SliderValue(string name, ref Vector2 value, float min, float max, Action<Vector2>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.SliderFloat2(name, ref value, min, max))
            interacted?.Invoke(value);
    }


    /// <summary>
    /// Renders a vector2 int to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced vector2 int object</param>
    /// <param name="speed">Amount of change when dragging the slider</param>
    /// <param name="min">Minimum range of the slider</param>
    /// <param name="max">Maximum range of the slider</param>
    /// <param name="interacted">Invoked when the vector2 int selector has been interacted with. Contains the new value of the vector2 int</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void DragValue(string name, ref Vector2Int value, int speed, int min, int max, Action<Vector2Int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.DragInt2(name, ref value.X, speed, min, max))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a vector2 int to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced vector2 int object</param>
    /// <param name="interacted">Invoked when the float selector has been interacted with. Contains the new value of the vector2 int</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void DragValue(string name, ref Vector2Int value, Action<Vector2Int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.DragInt2(name, ref value.X))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a vector2 int to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced vector2 int object</param>
    /// <param name="min">Minimum range of the slider</param>
    /// <param name="max">Maximum range of the slider</param>
    /// <param name="interacted">Invoked when the vector2 int selector has been interacted with. Contains the new value of the vector2 int</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void SliderValue(string name, ref Vector2Int value, int min, int max, Action<Vector2Int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.SliderInt2(name, ref value.X, min, max))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a float to a vector3 slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced vector3 object</param>
    /// <param name="interacted">Invoked when the vector3 selector has been interacted with. Contains the new value of the vector3</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void DragValue(string name, ref Vector3 value, Action<Vector3>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.DragFloat3(name, ref value))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a vector3 to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced vector3 object</param>
    /// <param name="speed">Amount of change when dragging the slider</param>
    /// <param name="min">Minimum range of the slider</param>
    /// <param name="max">Maximum range of the slider</param>
    /// <param name="interacted">Invoked when the vector3 selector has been interacted with. Contains the new value of the vector3</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void DragValue(string name, ref Vector3 value, float speed, float min, float max, Action<Vector3>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.DragFloat3(name, ref value, speed, min, max))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a vector3 to a normal slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced vector3 object</param>
    /// <param name="min">Minimum range of the slider</param>
    /// <param name="max">Maximum range of the slider</param>
    /// <param name="interacted">Invoked when the vector3 selector has been interacted with. Contains the new value of the vector3</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void SliderValue(string name, ref Vector3 value, float min, float max, Action<Vector3>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.SliderFloat3(name, ref value, min, max))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a vector4 to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced vector4 object</param>
    /// <param name="interacted">Invoked when the vector4 selector has been interacted with. Contains the new value of the vector4</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void DragValue(string name, ref Vector4 value, Action<Vector4>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.DragFloat4(name, ref value))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a vector4 to a drag slider
    /// </summary>
    /// <param name="name">Name of the vector4</param>
    /// <param name="value">Referenced float object</param>
    /// <param name="speed">Amount of change when dragging the slider</param>
    /// <param name="min">Minimum range of the slider</param>
    /// <param name="max">Maximum range of the slider</param>
    /// <param name="interacted">Invoked when the vector4 selector has been interacted with. Contains the new value of the vector4</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void DragValue(string name, ref Vector4 value, float speed, float min, float max, Action<Vector4>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.DragFloat4(name, ref value, speed, min, max))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a vector4 to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced vector4 object</param>
    /// <param name="min">Minimum range of the slider</param>
    /// <param name="max">Maximum range of the slider</param>
    /// <param name="interacted">Invoked when the vector4 selector has been interacted with. Contains the new value of the vector4</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void SliderValue(string name, ref Vector4 value, float min, float max, Action<Vector4>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.SliderFloat4(name, ref value, min, max))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a int to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced int object</param>
    /// <param name="interacted">Invoked when the int selector has been interacted with. Contains the new value of the int</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void DragValue(string name, ref int value, Action<int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.DragInt(name, ref value))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a int to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced int object</param>
    /// <param name="speed">Amount of change when dragging the slider</param>
    /// <param name="min">Minimum range of the slider</param>
    /// <param name="max">Maximum range of the slider</param>
    /// <param name="interacted">Invoked when the int selector has been interacted with. Contains the new value of the int</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void DragValue(string name, ref int value, int speed, int min, int max, Action<int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.DragInt(name, ref value, speed, min, max))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a int to a drag slider
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced int object</param>
    /// <param name="min">Minimum range of the slider</param>
    /// <param name="max">Maximum range of the slider</param>
    /// <param name="interacted">Invoked when the int selector has been interacted with. Contains the new value of the int</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static void SliderValue(string name, ref int value, int min, int max, Action<int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (CurrentBackend.SliderInt(name, ref value, min, max))
            interacted?.Invoke(value);
    }


    /// <summary>
    /// Renders an interactable text field
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Referenced string object</param>
    /// <param name="interacted">Invoked when the text field has been interacted with. Contains the new value of the string</param>
    /// <param name="maxLength">Maximum length of the text</param>
    public static void Text(string name, ref string value, Action<string>? interacted = null!, uint maxLength = 64)
    {
        if (!canRender)
            return;

        if (CurrentBackend.InputText(name, ref value, maxLength))
            interacted?.Invoke(value);
    }

    /// <summary>
    /// Renders a group of actions to a tab bar
    /// </summary>
    /// <param name="id">ID of the rendered tabs</param>
    /// <param name="tabs">The actual tabs to render</param>
    /// <remarks>The string of the tabs tuple is the tab name, while the Action is the on click action</remarks>
    public static void TabGroup(string id, params (string, Action?)[] tabs)
    {
        if (!canRender)
            return;

        if (!CurrentBackend.BeginTabBar(id, TabBarFlags.Reorderable))
            return;

        for (var i = 0; i < tabs.Length; i++)
        {
            var (tabTitle, tabAction) = tabs[i];

            if (!CurrentBackend.BeginTabItem($"{tabTitle}###{id}{i}"))
                continue;

            tabAction?.Invoke();
            CurrentBackend.EndTabItem();
        }

        CurrentBackend.EndTabBar();
    }

    /// <summary>
    /// Renders a menu item to a tab bar
    /// </summary>
    /// <param name="text">Text of the menu item</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    public static bool MenuItem(string text)
    {
        if (!canRender)
            return false;

        var temp = false;
        return MenuItem(text, ref temp);
    }

    /// <summary>
    /// Renders a menu item to a tab bar with a click action
    /// </summary>
    /// <param name="text">Text of the menu item</param>
    /// <param name="action">Invoked when the menu item is clicked</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
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

    /// <summary>
    /// Renders a menu item to the tab bar with an enabled state
    /// </summary>
    /// <param name="text">Text of the menu item</param>
    /// <param name="enabled">State of the menu item deciding if its enabled or not</param>
    /// <returns>True if any of the rendered fields were interacted with</returns>
    [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
    public static bool MenuItem(string text, ref bool enabled)
    {
        if (canRender)
            return CurrentBackend.MenuItem(text, null, ref enabled);
        return false;
    }

    /// <summary>
    /// Renders a group of actions to the menu bar
    /// </summary>
    /// <param name="subMenus">Sub menus to render</param>
    /// <remarks>The string of the subMenus tuple is the menus name, while the Action is invoked on click of that item</remarks>
    public static void MenuBar(params (string, Action?)[] subMenus)
    {
        MenuBar(false, subMenus);
    }

    /// <summary>
    /// Renders a group of actions to the menu bar
    /// </summary>
    /// <param name="isMainMenuBar">Decides if the menu bar is the main one</param>
    /// <param name="subMenus">Sub menus to render</param>
    /// <remarks>The string of the subMenus tuple is the menus name, while the Action is invoked on click of that item</remarks>
    public static void MenuBar(bool isMainMenuBar = false, params (string, Action?)[] subMenus)
    {
        if (isMainMenuBar ? !CurrentBackend.BeginMainMenuBar() : !CurrentBackend.BeginMenuBar())
            return;

        foreach (var subMenu in subMenus)
        {
            if (!CurrentBackend.BeginMenu(subMenu.Item1))
                continue;

            subMenu.Item2?.Invoke();
            CurrentBackend.EndMenu();
        }

        if (isMainMenuBar)
            CurrentBackend.EndMainMenuBar();
        else
            CurrentBackend.EndMenuBar();
    }

    /// <summary>
    /// Render an unclosable window
    /// </summary>
    /// <param name="title">Title of the window</param>
    /// <param name="render">Ui render action of the window</param>
    /// <param name="flags">Flags to render the window with</param>
    public static void Window(string title, Action? render, WindowFlags flags = WindowFlags.None)
    {
        if (!CurrentBackend.Begin(title, flags))
            return;

        render?.Invoke();
        CurrentBackend.End();
    }

    /// <summary>
    /// Render a window
    /// </summary>
    /// <param name="title">Title of the window</param>
    /// <param name="render">Ui render action of the window</param>
    /// <param name="isOpen">Referenced open state of the window</param>
    /// <param name="flags">Flags to render the window with</param>
    public static void Window(string title, Action render, ref bool isOpen, WindowFlags flags = WindowFlags.None)
    {
        if (!isOpen)
            return;

        if (!CurrentBackend.Begin(title, ref isOpen, flags))
            return;

        render.Invoke();
        CurrentBackend.End();
    }

    /// <summary>
    /// Load a font from the disc
    /// </summary>
    /// <param name="path">The path of the TTF font on disc</param>
    /// <param name="pixelSize">pixelSize</param>
    /// <remarks>Only TTF fonts are supported</remarks>
    public static void LoadFont(string path, float pixelSize)
    {
        CurrentBackend.LoadFont(path, pixelSize);
    }

    /// <summary>
    /// Load a font from memory
    /// </summary>
    /// <param name="fontData">The data of the TTF font</param>
    /// <param name="pixelSize">pixelSize</param>
    /// <param name="dataSize">dataSize</param>
    /// <remarks>Only TTF fonts are supported</remarks>
    public static void LoadFontFromMemory(byte[] fontData, int pixelSize, int dataSize)
    {
        CurrentBackend.LoadFontFromMemory(fontData, pixelSize, dataSize);
    }
}