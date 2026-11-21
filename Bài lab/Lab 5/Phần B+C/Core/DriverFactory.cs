using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Lab4_SOF3091.Config;

namespace Lab5_SOF3091_.Core;

public static class DriverFactory
{
    public static IWebDriver Create(TestConfig cfg)
    {
        if (cfg == null)
            throw new ArgumentNullException(nameof(cfg), "TestConfig không được null.");

        string browser = (cfg.Browser ?? string.Empty).Trim().ToLowerInvariant();
        int width = cfg.WindowWidth > 0 ? cfg.WindowWidth : 1920;
        int height = cfg.WindowHeight > 0 ? cfg.WindowHeight : 1080;

        return browser switch
        {
            "chrome" => CreateChromeDriver(width, height, cfg.Headless),
            "firefox" => CreateFirefoxDriver(width, height, cfg.Headless),
            _ => throw new NotSupportedException(
                $"Trình duyệt '{cfg.Browser}' không được hỗ trợ. Vui lòng dùng 'Chrome' hoặc 'Firefox'.")
        };
    }

    private static IWebDriver CreateChromeDriver(int width, int height, bool headless)
    {
        var options = new ChromeOptions();
        options.AddArgument($"--window-size={width},{height}");

        if (headless)
        {
            options.AddArgument("--headless=new");
        }

        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefoxDriver(int width, int height, bool headless)
    {
        var options = new FirefoxOptions();
        options.AddArgument($"--width={width}");
        options.AddArgument($"--height={height}");

        if (headless)
        {
            options.AddArgument("--headless");
        }

        return new FirefoxDriver(options);
    }
}