using CustomerApplication.Helpers.Extensions;

namespace CustomerApplication.Tests.Helpers.Extensions;

[TestFixture]
public class StringExtensionTests
{
    [TestCase("Completed String", true)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("     ", false)]
    public void String_HasValue_GivesExpectedResult(string actual, bool expected)
    {
        Assert.That(actual.HasValue(), Is.EqualTo(expected));
    }

    [TestCase("all character string", "")]
    [TestCase("1234", "1234")]
    [TestCase("1-2-3-4", "1234")]
    [TestCase("(314)-123-1234", "3141231234")]
    [TestCase("(314)-abc-2-g-4-j", "31424")]
    public void String_GetNumber_GivesExpectedResult(string value, string expected)
    {
        Assert.That(value.GetNumbers(), Is.EqualTo(expected));
    }
}
