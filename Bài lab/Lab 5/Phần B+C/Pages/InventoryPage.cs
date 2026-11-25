using OpenQA.Selenium;

namespace Lab5_SOF3091_phanBC.Pages;

public class LoginPage
{
	private readonly IWebDriver _driver;

	public LoginPage(IWebDriver driver) { _driver = driver; }

	public void DangNhap(string username, string password)
	{
		Nhap(By.Id("user-name"), username);
		Nhap(By.Id("password"), password);
		_driver.FindElement(By.Id("login-button")).Click();
	}

	private void Nhap(By locator, string noiDung)
	{
		var o = _driver.FindElement(locator);
		o.Clear();
		if (!string.IsNullOrEmpty(noiDung))   // ca biên: chuỗi rỗng thì không gõ
			o.SendKeys(noiDung);
	}
}