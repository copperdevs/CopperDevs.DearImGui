using System.Numerics;
using CopperDevs.Core.Data;

namespace CopperDevs.DearImGui.Testing;

public class ReflectionTest
{
    public MyEnum CurrentMyEnum = MyEnum.One;
    public MyEnum CurrentMyEnumButTheSecond = MyEnum.One;

    // out of sight out of mind
    public List<MyEnum> ManyMyEnums = [MyEnum.One, MyEnum.Two, MyEnum.Three];

    public enum MyEnum
    {
        One,
        Two,
        Three
    }

    public Vector2Int Vector2Int;

    public List<float> FloatList = [0, 1, 2];

    public List<int> IntList = [0, 1, 2];

    public List<Vector2> Vector2List = [Vector2.Zero, Vector2.One, Vector2.One * 2];
}