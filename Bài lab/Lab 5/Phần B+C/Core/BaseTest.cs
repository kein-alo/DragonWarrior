using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Lab4_SOF3091.Config;
using Lab4_SOF3091.Utilities;

namespace Lab4_SOF3091.Core;

public abstract class BaseTest
{
    protected TestConfig Config { get; private set; } = null!;
    protected IWebDriver Driver { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        Config = ConfigReader.Load();
        Driver = DriverFactory.Create(Config);

        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

        if (!string.IsNullOrWhiteSpace(Config.BaseUrl))
        {
            Driver.Navigate().GoToUrl(Config.BaseUrl);
        }
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == TestStatus.Failed)
            {
                string testName = TestContext.CurrentContext.Test.Name;
                string savedPath = ScreenshotHelper.Capture(Driver, testName);
                if (!string.IsNullOrEmpty(savedPath))
                {
                    TestContext.AddTestAttachment(savedPath, "Failure Screenshot");
                }
            }
        }
        catch (Exception ex)
        {
            TestContext.Out.WriteLine($"Lỗi khi chụp ảnh màn hình: {ex.Message}");
        }
        finally
        {
            try
            {
                Driver?.Quit();
            }
            finally
            {
                Driver?.Dispose();
                Driver = null!;
            }
        }
    }
}
