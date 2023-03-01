namespace CustomerApplication.Helpers.Extensions;

public static class TimeSpanExtensions
{
    public static bool Between(this TimeSpan self, TimeSpan start, TimeSpan end)
    {
        return self >= start && self <= end;
    }
}
