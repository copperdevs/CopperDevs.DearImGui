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
}