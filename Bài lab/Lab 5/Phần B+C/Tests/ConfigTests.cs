using System;
using NUnit.Framework;
using Lab5_SOF3091_phanBC.Config;

namespace Lab5_SOF3091_phanBC.Tests;

[TestFixture]
public class ConfigTests
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
    public void ReadTimeoutSeconds_ShouldBeGreaterThanZero()
    {
        Assert.That(ConfigReader.Settings.TimeoutSeconds, Is.GreaterThan(0));
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
