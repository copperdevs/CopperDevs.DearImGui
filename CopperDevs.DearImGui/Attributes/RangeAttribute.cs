namespace CopperDevs.DearImGui.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class RangeAttribute : Attribute
{
    public readonly float Min;
    public readonly float Max;
    public RangeType TargetRangeType = RangeType.Slider;
    public float Speed = 1;

    public RangeAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }

    public RangeAttribute(float max) : this(0, max)
    {
    }
}

public enum RangeType
{
    Drag,
    Slider
}