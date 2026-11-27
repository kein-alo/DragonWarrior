using NUnit.Framework;
using Lab5_SOF3091.Config;
using Lab5_SOF3091.Core;

namespace Lab5_SOF3091.Tests;

/// <summary>PHẦN A + B - Kiểm tra cấu hình và DriverFactory. Kế thừa BaseTest như mọi file test.</summary>
public class ConfigTests : BaseTest
{
    [Test]
    public void CauHinh_PhaiHopLe()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Uri.TryCreate(Cfg.BaseUrl, UriKind.Absolute, out _), Is.True, "BaseUrl");
            Assert.That(Cfg.Browser, Is.Not.Empty, "Browser");
            Assert.That(Cfg.TimeoutSeconds, Is.GreaterThan(0), "TimeoutSeconds");
            Assert.That(Cfg.WindowWidth, Is.GreaterThan(0), "WindowWidth");
            Assert.That(Cfg.WindowHeight, Is.GreaterThan(0), "WindowHeight");
        });
    }

    [Test]
    public void TrinhDuyet_PhaiMoDungBaseUrlTrongConfig()
    {
        var host = new Uri(Cfg.BaseUrl).Host;
        Assert.That(Driver.Url, Does.Contain(host));
    }

    [TestCase("chromee")]
    [TestCase("Safari")]
    public void DriverFactory_TrinhDuyetKhongHoTro_NemLoiNeuDungTenGoSai(string browser)
    {
        var cfg = new TestConfig { BaseUrl = Cfg.BaseUrl, Browser = browser };

        var ex = Assert.Throws<NotSupportedException>(() => DriverFactory.Create(cfg));
        Assert.That(ex!.Message, Does.Contain($"'{browser}'"));
    }
}
