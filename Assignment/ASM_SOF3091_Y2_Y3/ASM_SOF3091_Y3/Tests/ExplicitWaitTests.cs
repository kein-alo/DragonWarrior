using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ASM_SOF3091_Y3.Helpers;

namespace ASM_SOF3091_Y3.Tests
{
    /// <summary>
    /// Y3 - Xử lý tình huống thực tế &amp; chờ tường minh.
    /// Không dùng lệnh dừng cứng (sleep), không dùng ImplicitWait (tránh trộn implicit + explicit).
    /// </summary>
    [TestFixture]
    public class ExplicitWaitTests
    {
        private const string BaseUrl = "https://www.saucedemo.com/";
        private IWebDriver _driver = null!;
        private WaitHelper _wait = null!;

        // Locator ổn định: data-test
        private static readonly By Username   = By.CssSelector("[data-test='username']");
        private static readonly By Password   = By.CssSelector("[data-test='password']");
        private static readonly By LoginBtn   = By.CssSelector("[data-test='login-button']");
        private static readonly By Title      = By.CssSelector("[data-test='title']");
        private static readonly By Error      = By.CssSelector("[data-test='error']");
        private static readonly By ErrorClose = By.CssSelector("[data-test='error-button']");
        private static readonly By Badge      = By.CssSelector("[data-test='shopping-cart-badge']");
        private static readonly By CartLink   = By.CssSelector("[data-test='shopping-cart-link']");
        private static readonly By ItemName   = By.CssSelector("[data-test='inventory-item-name']");
        private static readonly By Checkout   = By.CssSelector("[data-test='checkout']");
        private static readonly By Backpack   = By.CssSelector("[data-test='add-to-cart-sauce-labs-backpack']");
        private static readonly By BikeLight  = By.CssSelector("[data-test='add-to-cart-sauce-labs-bike-light']");
        private static readonly By RemoveBackpack = By.CssSelector("[data-test='remove-sauce-labs-backpack']");

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            // Tắt popup "Change your password" / lưu mật khẩu của Chrome (nó che và nuốt mất click)
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddUserProfilePreference("profile.password_manager_leak_detection", false);
            options.AddArgument("--incognito");
            options.AddArgument("--disable-notifications");
            if (Environment.GetEnvironmentVariable("HEADLESS") == "1")
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--window-size=1920,1080");
            }
            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();
            _wait = new WaitHelper(_driver, 10);
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }

        private void Login(string user, string pass)
        {
            _driver.Navigate().GoToUrl(BaseUrl);
            _wait.Visible(Username).SendKeys(user);
            _wait.Visible(Password).SendKeys(pass);
            _wait.Clickable(LoginBtn).Click();
        }

        // 1) Đăng nhập + chuyển trang (SPA đổi route chứ không tải lại trang)
        [Test]
        public void Login_ValidAccount_WaitsForInventoryPage()
        {
            Login("standard_user", "secret_sauce");

            Assert.That(_wait.UrlContains("inventory.html"), Is.True);
            Assert.That(_wait.Visible(Title).Text, Is.EqualTo("Products"));
        }

        // 2) Thêm vào giỏ + kiểm tra giỏ hàng, chờ badge cập nhật
        [Test]
        public void AddToCart_ThenVerifyCartContents()
        {
            Login("standard_user", "secret_sauce");

            _wait.Clickable(Backpack).Click();
            _wait.Clickable(BikeLight).Click();

            Assert.That(_wait.TextIs(Badge, "2"), Is.True, "Badge phải đổi thành 2");

            _wait.Clickable(CartLink).Click();
            _wait.UrlContains("cart.html");

            var names = _wait.CountIs(ItemName, 2).Select(e => e.Text).ToList();
            Assert.That(names, Does.Contain("Sauce Labs Backpack"));
            Assert.That(names, Does.Contain("Sauce Labs Bike Light"));
        }

        // 3) Tình huống: phần tử biến mất sau thao tác (badge mất khi giỏ rỗng)
        [Test]
        public void RemoveLastItem_BadgeDisappears()
        {
            Login("standard_user", "secret_sauce");

            _wait.Clickable(Backpack).Click();
            Assert.That(_wait.TextIs(Badge, "1"), Is.True, "Badge phải là 1");

            _wait.Clickable(RemoveBackpack).Click();

            Assert.That(_wait.Invisible(Badge), Is.True, "Badge phải biến mất khi giỏ rỗng");
        }

        // 4) Tình huống: TRANG TẢI CHẬM - performance_glitch_user bị trễ vài giây sau khi bấm Login
        [Test]
        public void Login_SlowUser_WaitsForSlowLoad()
        {
            Login("performance_glitch_user", "secret_sauce");

            // Timeout riêng 20s cho tình huống chậm, bình thường vẫn dùng 10s
            Assert.That(_wait.UrlContains("inventory.html", TimeSpan.FromSeconds(20)), Is.True);
            Assert.That(_wait.Visible(Title, TimeSpan.FromSeconds(20)).Text, Is.EqualTo("Products"));

            _wait.Clickable(Backpack, TimeSpan.FromSeconds(20)).Click();
            Assert.That(_wait.TextIs(Badge, "1"), Is.True, "Badge phải là 1");
        }

        // 5) Tình huống: THÔNG BÁO LỖI xuất hiện trễ rồi được tắt đi
        [Test]
        public void Login_LockedOutUser_ShowsErrorThenCanBeDismissed()
        {
            Login("locked_out_user", "secret_sauce");

            var message = _wait.TextNotEmpty(Error);
            Assert.That(message, Does.Contain("locked out"));

            _wait.Clickable(ErrorClose).Click();
            Assert.That(_wait.Invisible(Error), Is.True, "Thông báo lỗi phải biến mất sau khi đóng");
        }

        // 6) Tình huống: chuyển bước (giỏ hàng -> checkout)
        [Test]
        public void Cart_ClickCheckout_WaitsForNextStep()
        {
            Login("standard_user", "secret_sauce");
            _wait.Clickable(Backpack).Click();
            _wait.Clickable(CartLink).Click();
            _wait.UrlContains("cart.html");

            _wait.Clickable(Checkout).Click();

            Assert.That(_wait.UrlContains("checkout-step-one.html"), Is.True);
            Assert.That(_wait.Visible(Title).Text, Is.EqualTo("Checkout: Your Information"));
        }
    }
}