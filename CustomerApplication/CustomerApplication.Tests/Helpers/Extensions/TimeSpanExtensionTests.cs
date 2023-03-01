using CustomerApplication.Helpers.Extensions;

namespace CustomerApplication.Tests.Helpers.Extensions;

[TestFixture]
public class TimeSpanExtensionTests
{
    [TestCase(10, 9, 11, true)]
    [TestCase(8, 9, 11, false)]
    [TestCase(12, 9, 11, false)]
    [TestCase(9, 9, 11, true)]
    [TestCase(11, 9, 11, true)]
    public void TimeSpan_Between_GivesExpectedResult(int time, int startTime, int endTime, bool result)
    {
        TimeSpan actual = new TimeSpan(time, 0, 0); 
        TimeSpan start = new TimeSpan(startTime, 0, 0); 
        TimeSpan end = new TimeSpan(endTime, 0, 0);

        Assert.That(actual.Between(start, end), Is.EqualTo(result));
    }
}
