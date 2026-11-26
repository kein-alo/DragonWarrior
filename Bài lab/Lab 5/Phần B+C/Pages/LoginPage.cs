using OpenQA.Selenium;

namespace Lab5_SOF3091_phanBC.Pages;

/// <summary>Page Object cho màn hình đăng nhập SauceDemo.</summary>
public sealed class LoginPage
{
    private readonly IWebDriver _driver;
    public LoginPage(IWebDriver driver) => _driver = driver;

    public void DangNhap(string username, string password)
    {
        Nhap(By.Id("user-name"), username);
        Nhap(By.Id("password"), password);
        _driver.FindElement(By.Id("login-button")).Click();
    }

    private void Nhap(By locator, string noiDung)
    {
        var element = _driver.FindElement(locator);
        element.Clear();
        if (!string.IsNullOrEmpty(noiDung))
            element.SendKeys(noiDung);
    }
}
