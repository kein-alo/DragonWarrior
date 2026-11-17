using OpenQA.Selenium;

namespace ASM_SOF3091.Pages
{
    public class LoginPage : BasePage
    {
        // Locator để ở chế độ private theo đúng yêu cầu POM
        private readonly By _usernameInput = By.Id("user-name");
        private readonly By _passwordInput = By.Id("password");
        private readonly By _loginButton = By.Id("login-button");

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void Open(string url = "https://www.saucedemo.com/")
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void Login(string username, string password)
        {
            EnterText(_usernameInput, username);
            EnterText(_passwordInput, password);
            ClickElement(_loginButton);
        }
        // Thêm locator của khung báo lỗi trên trang Login của Saucedemo
        private readonly By _errorMessage = By.CssSelector("[data-test='error']");

        // Thêm phương thức này để trả về nội dung chuỗi lỗi
        public string GetErrorMessage()
        {
            return GetElementText(_errorMessage);
        }
    }
}