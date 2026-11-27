using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using Lab5_SOF3091.Config;

namespace Lab5_SOF3091.Core;

/// <summary>
/// PHẦN B - Driver Factory. Trả về IWebDriver (đa hình) nên test không biết đang chạy trình duyệt nào.
/// Đổi trình duyệt = sửa "Browser" trong appsettings.json, không sửa code.
/// </summary>
public static class DriverFactory
{
    public static IWebDriver Create(TestConfig cfg)
    {
        ArgumentNullException.ThrowIfNull(cfg);

        string name = (cfg.Browser ?? string.Empty).Trim();

        return name.ToLowerInvariant() switch
        {
            "chrome"  => new ChromeDriver(ChromeOpts(cfg)),
            "firefox" => new FirefoxDriver(FirefoxOpts(cfg)),
            "edge"    => new EdgeDriver(EdgeOpts(cfg)),
            _ => throw new NotSupportedException(
                $"Trình duyệt '{cfg.Browser}' không được hỗ trợ. Chỉ hỗ trợ: Chrome, Firefox, Edge.")
        };
    }

    // Chrome/Edge: --headless=new và --window-size=W,H
    private static ChromeOptions ChromeOpts(TestConfig cfg)
    {
        var o = new ChromeOptions();
        if (cfg.Headless) o.AddArgument("--headless=new");
        o.AddArgument($"--window-size={cfg.WindowWidth},{cfg.WindowHeight}");
        return o;
    }

    private static EdgeOptions EdgeOpts(TestConfig cfg)
    {
        var o = new EdgeOptions();
        if (cfg.Headless) o.AddArgument("--headless=new");
        o.AddArgument($"--window-size={cfg.WindowWidth},{cfg.WindowHeight}");
        return o;
    }

    // Firefox: --headless (KHÔNG có =new) và --width / --height
    private static FirefoxOptions FirefoxOpts(TestConfig cfg)
    {
        var o = new FirefoxOptions();
        if (cfg.Headless) o.AddArgument("--headless");
        o.AddArgument($"--width={cfg.WindowWidth}");
        o.AddArgument($"--height={cfg.WindowHeight}");
        return o;
    }
}
