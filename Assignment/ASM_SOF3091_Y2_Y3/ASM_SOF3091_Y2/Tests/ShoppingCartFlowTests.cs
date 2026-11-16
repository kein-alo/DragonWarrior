using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ASM_SOF3091_Y2.Tests
{
    /// <summary>
    /// Y2 - Tự động hóa luồng với Selenium (chưa dùng POM, chưa dùng WebDriverWait).
    /// Locator ổn định: By.Id / [data-test='...'] - KHÔNG dùng XPath dài.
    /// Chờ tạm thời bằng ImplicitWait (cấu hình 1 lần); sang Y3 sẽ thay bằng WebDriverWait.
    /// </summary>
    [TestFixture]
    public class ShoppingCartFlowTests
    {
        private const string BaseUrl = "https://www.saucedemo.com/";
        private IWebDriver _driver = null!;

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
            
            // Nếu muốn chạy ẩn (Headless) thì bật biến môi trường HEADLESS=1. 
            // Để chụp ảnh thì ĐỪNG bật biến này.
            if (Environment.GetEnvironmentVariable("HEADLESS") == "1")
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--window-size=1920,1080");
            }
            
            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();
            
            // Cấu hình ImplicitWait theo yêu cầu đề bài Y2
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
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
            _driver.FindElement(By.CssSelector("[data-test='username']")).SendKeys(user);
            _driver.FindElement(By.CssSelector("[data-test='password']")).SendKeys(pass);
            _driver.FindElement(By.CssSelector("[data-test='login-button']")).Click();
        }

        // ---------- Luồng 1: Đăng nhập ----------
        [Test]
        public void Login_ValidAccount_GoesToInventoryPage()
        {
            Login("standard_user", "secret_sauce");

            Assert.That(_driver.Url, Does.Contain("inventory.html"));
            Assert.That(_driver.FindElement(By.CssSelector("[data-test='title']")).Text,
                Is.EqualTo("Products"));
        }

        // ---------- Luồng 2: Thêm sản phẩm vào giỏ & kiểm tra giỏ hàng ----------
        [Test]
        public void AddProductsToCart_ThenOpenCart_ShowsCorrectProducts()
        {
            Login("standard_user", "secret_sauce");

            // 1. Thêm Backpack
            _driver.FindElement(By.CssSelector("[data-test='add-to-cart-sauce-labs-backpack']")).Click();
            // Nút đổi thành "Remove" => thao tác thêm đã được ghi nhận (ImplicitWait tự thử lại)
            _driver.FindElement(By.CssSelector("[data-test='remove-sauce-labs-backpack']"));

            // 2. Thêm Bike Light
            _driver.FindElement(By.CssSelector("[data-test='add-to-cart-sauce-labs-bike-light']")).Click();
            _driver.FindElement(By.CssSelector("[data-test='remove-sauce-labs-bike-light']"));

            // 3. Kiểm tra badge giỏ hàng
            Assert.That(_driver.FindElement(By.CssSelector("[data-test='shopping-cart-badge']")).Text,
                Is.EqualTo("2"), "Badge phải hiển thị 2 sản phẩm");

            // 4. Mở giỏ hàng
            _driver.FindElement(By.CssSelector("[data-test='shopping-cart-link']")).Click();

            // Chờ tên sản phẩm trong giỏ xuất hiện (ImplicitWait tự thử lại) rồi mới kiểm tra URL
            _driver.FindElement(By.CssSelector("[data-test='inventory-item-name']"));
            Assert.That(_driver.Url, Does.Contain("cart.html"));

            // 5. Lấy danh sách tên sản phẩm và kiểm tra
            var names = _driver.FindElements(By.CssSelector("[data-test='inventory-item-name']"))
                               .Select(e => e.Text).ToList();

            Assert.That(names, Has.Count.EqualTo(2));
            Assert.That(names, Does.Contain("Sauce Labs Backpack"));
            Assert.That(names, Does.Contain("Sauce Labs Bike Light"));

            // ==============================================================
            // MẸO: Dòng dưới đây giúp giữ trình duyệt mở 5 giây để bạn kịp 
            // dùng Snipping Tool chụp ảnh màn hình chèn vào báo cáo Word.
            // Chụp xong bạn có thể xóa dòng này đi (hoặc comment lại).
            // ==============================================================
            
        }
    }
}