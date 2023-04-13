namespace Modules.Utilities
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string value)
        {
            return string.Concat(value.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }
    }
}
