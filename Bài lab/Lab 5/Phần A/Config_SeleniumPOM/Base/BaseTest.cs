using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SeleniumPOM_CSharp.Utilities;

namespace SeleniumPOM_CSharp.Base;

public abstract class BaseTest
{
    protected IWebDriver Driver = null!;

    [SetUp]
    public void SetUpBrowser()
    {
        TestSettings config = ConfigReader.Settings;

        if (config.Browser.Equals("Chrome", StringComparison.OrdinalIgnoreCase))
        {
            var options = new ChromeOptions();
            if (config.Headless)
                options.AddArgument("--headless=new");

            Driver = new ChromeDriver(options);
        }
        else if (config.Browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase))
        {
            var options = new FirefoxOptions();
            if (config.Headless)
                options.AddArgument("-headless");

            Driver = new FirefoxDriver(options);
        }
        else
        {
            throw new InvalidOperationException(
                $"Trình duyệt không được hỗ trợ: {config.Browser}");
        }

        Driver.Manage().Timeouts().ImplicitWait =
            TimeSpan.FromSeconds(config.Timeout);

        Driver.Navigate().GoToUrl(config.BaseUrl);
    }

    [TearDown]
    public void TearDownBrowser()
    {
        if (Driver is not null)
        {
            Driver.Quit();
            Driver.Dispose();
            Driver = null!;
        }
    }
}
