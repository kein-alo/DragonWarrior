using NUnit.Framework;
using SeleniumPOM_CSharp.Utilities;

namespace SeleniumPOM_CSharp.Tests;

[TestFixture]
public class TestConfig
{
    [Test]
    public void ReadBaseUrl_ShouldReturnValidUrl()
    {
        Assert.That(ConfigReader.Settings.BaseUrl, Is.Not.Empty);
        Assert.That(
            Uri.TryCreate(ConfigReader.Settings.BaseUrl, UriKind.Absolute, out _),
            Is.True);
    }

    [Test]
    public void ReadBrowser_ShouldBeChromeOrFirefox()
    {
        Assert.That(
            ConfigReader.Settings.Browser,
            Is.AnyOf("Chrome", "Firefox").IgnoreCase);
    }

    [Test]
    public void ReadTimeout_ShouldBeGreaterThanZero()
    {
        Assert.That(ConfigReader.Settings.Timeout, Is.GreaterThan(0));
    }

    [Test]
    public void ReadHeadless_ShouldReturnBooleanValue()
    {
        Assert.That(ConfigReader.Settings.Headless, Is.TypeOf<bool>());
    }

    [TestCase("Chrome")]
    [TestCase("Firefox")]
    public void SupportedBrowser_ShouldAcceptChromeAndFirefox(string browser)
    {
        Assert.That(browser, Is.AnyOf("Chrome", "Firefox").IgnoreCase);
    }
}
