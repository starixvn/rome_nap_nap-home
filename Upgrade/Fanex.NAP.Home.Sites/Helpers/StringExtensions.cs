namespace Fanex.NAP.Home.Core.Helpers;
public static class StringExtensions
{
    public static string Truncate(this string value, int length)
    {
        const string ect = "...";
        length -= ect.Length;
        value = value ?? string.Empty;
        value = value.Trim();
        value = value.Length > length ? value.Substring(0, length) + "..." : value;

        return value;
    }

    public static string TrimOrEmpty(string value)
    {
        return string.IsNullOrEmpty(value) ? string.Empty : value.Trim();
    }
}
