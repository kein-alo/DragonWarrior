using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoTests;

public class FirstTest
{
    private IWebDriver _driver;

    [SetUp]
    public void Setup()
    {
        _driver = new ChromeDriver();
        _driver.Manage().Window.Maximize();
    }

    [Test]
    public void OpenSauceDemo_TitleIsSwagLabs()
    {
        _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        Assert.That(_driver.Title, Is.EqualTo("Swag Labs"));
    }

    [TearDown]
    public void Teardown()
    {
        _driver.Quit();
        _driver.Dispose();
    }
}