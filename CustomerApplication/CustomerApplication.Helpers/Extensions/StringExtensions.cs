namespace CustomerApplication.Helpers.Extensions;

public static class StringExtensions
{
    public static bool HasValue(this string value)
    {
        return !String.IsNullOrEmpty(value?.Trim());
    }

    public static string GetNumbers(this string value)
    {
        return new (value.Where(char.IsDigit).ToArray());
    }
}
