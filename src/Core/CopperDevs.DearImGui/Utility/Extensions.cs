using System.Text.RegularExpressions;

namespace CopperDevs.DearImGui.Utility;

/// <summary>
/// Utility extensions
/// </summary>
public static partial class Extensions
{
    /// <summary>
    /// Converts a string to use kebab case
    /// </summary>
    /// <param name="str">Input string</param>
    /// <returns>Input string in kebab case</returns>
    public static string ToKebabCase(this string str)
    {
        return KebabCaseRegex().Replace(str, "-$1").ToLower();
    }

    [GeneratedRegex(@"(?<!^)(?<!-)((?<=\p{Ll})\p{Lu}|\p{Lu}(?=\p{Ll}))")]
    private static partial Regex KebabCaseRegex();
    
    /// <summary>
    /// Get all public static values from a certain value
    /// </summary>
    /// <param name="type">Type of the class to get the values from</param>
    /// <typeparam name="T">Field type</typeparam>
    /// <returns>List of found types</returns>
    public static List<T> GetAllPublicConstantValues<T>(this Type type)
    {
        return type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi is { IsLiteral: true, IsInitOnly: false } && fi.FieldType == typeof(T))
            .Select(x => (T)x.GetRawConstantValue()!)
            .ToList();
    }
}