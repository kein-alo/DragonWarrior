using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests;

public class FirstSeleniumTest
{
    private IWebDriver _driver = null!;

    [SetUp]
    public void Setup()
    {
        _driver = new ChromeDriver();              // 1. Mở Chrome
    }

    [Test]
    public void OpenExamplePage_TitleShouldBeExampleDomain()
    {
        _driver.Navigate().GoToUrl("https://example.com");   // 2. Điều hướng đến trang web

        string title = _driver.Title;                        // 3. Đọc tiêu đề
        TestContext.Progress.WriteLine($"Tiêu đề trang: {title}");   // 3. In tiêu đề

        Assert.That(title, Is.EqualTo("Example Domain"));    // 4. Assert kết quả mong đợi
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();                            // 5. Đóng trình duyệt
        _driver.Dispose();
    }
}