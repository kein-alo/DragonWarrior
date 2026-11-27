using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Lab5_SOF3091.Config;
using Lab5_SOF3091.Utilities;

namespace Lab5_SOF3091.Core;

/// <summary>PHẦN C - Lớp cơ sở gom vòng đời: đọc config -> tạo driver -> mở URL; chụp ảnh khi Failed; đóng trình duyệt.</summary>
public abstract class BaseTest
{
    protected TestConfig Cfg { get; private set; } = null!;
    protected IWebDriver Driver { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        Cfg = ConfigReader.Settings;
        Driver = DriverFactory.Create(Cfg);

        // Không dùng ImplicitWait (trộn với explicit wait gây chờ khó đoán); không dùng Maximize() (vô nghĩa khi headless)
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Cfg.TimeoutSeconds);

        Driver.Navigate().GoToUrl(Cfg.BaseUrl);
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            // Chụp ảnh trong try: lỗi khi chụp không được cản việc đóng trình duyệt
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                string path = ScreenshotHelper.Capture(Driver, TestContext.CurrentContext.Test.Name);
                if (!string.IsNullOrEmpty(path))
                    TestContext.AddTestAttachment(path, "Failure Screenshot");
            }
        }
        catch (Exception ex)
        {
            TestContext.Out.WriteLine($"Lỗi khi chụp ảnh màn hình: {ex.Message}");
        }
        finally
        {
            // Luôn đóng trình duyệt, tránh để Chrome/Firefox chạy ngầm
            try { Driver?.Quit(); }
            finally
            {
                Driver?.Dispose();
                Driver = null!;
            }
        }
    }
}
