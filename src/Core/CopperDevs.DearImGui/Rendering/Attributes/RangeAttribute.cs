namespace CopperDevs.DearImGui.Rendering;

/// <summary>
/// Applied to numerical value types to limit their range
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class RangeAttribute : Attribute
{
    /// <summary>
    /// Lower limit of the slider
    /// </summary>
    public readonly float Min;

    /// <summary>
    /// Upper limit of the slider
    /// </summary>
    public readonly float Max;

    /// <summary>
    /// Type of sliding displayed
    /// </summary>
    // ReSharper disable once FieldCanBeMadeReadOnly.Global
    // ReSharper disable once ConvertToConstant.Global
    public RangeType TargetRangeType = RangeType.Slider;

    /// <summary>
    /// How fast the slider changes when dragged
    /// </summary>
    // ReSharper disable once FieldCanBeMadeReadOnly.Global
    // ReSharper disable once ConvertToConstant.Global
    public float Speed = 1;

    /// <summary>
    /// Base constructor
    /// </summary>
    /// <param name="min">Lower limit of the slider</param>
    /// <param name="max">Upper limit of the slider</param>
    public RangeAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }

    /// <summary>
    /// Constructor with a maximum, with the minimum being zero
    /// </summary>
    /// <param name="max">Upper limit of the slider</param>
    public RangeAttribute(float max) : this(0, max)
    {
    }
}

/// <summary>
/// Types of sliders to display
/// </summary>
public enum RangeType
{
    /// <summary>
    /// Drag slider
    /// </summary>
    Drag,
    
    /// <summary>
    /// Normal slider
    /// </summary>
    Slider
}