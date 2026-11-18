using FirstTests.Pages;
using OpenQA.Selenium;

namespace Lab4_SOF3091.Pages
{
    public class LoginPage : BasePage
    {
        // Khai báo locator private readonly By đúng chuẩn POM
        private readonly By _txtUsername = By.Id("user-name");
        private readonly By _txtPassword = By.Id("password");
        private readonly By _btnLogin = By.Id("login-button");
        private readonly By _lblError = By.CssSelector("[data-test='error']");

        public LoginPage(IWebDriver driver) : base(driver) { }

        public LoginPage MoTrang(string url = "https://www.saucedemo.com/")
        {
            driver.Navigate().GoToUrl(url);
            return this;
        }

        // Đăng nhập chuyển trang -> trả về InventoryPage
        public InventoryPage DangNhap(string tk, string mk)
        {
            var userElem = Tim(_txtUsername);
            userElem.Clear();
            userElem.SendKeys(tk);

            var passElem = Tim(_txtPassword);
            passElem.Clear();
            passElem.SendKeys(mk);

            Tim(_btnLogin).Click();
            return new InventoryPage(driver);
        }

        public string LayThongBaoLoi()
        {
            return Tim(_lblError).Text;
        }
    }
}