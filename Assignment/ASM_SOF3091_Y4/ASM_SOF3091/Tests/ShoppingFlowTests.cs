using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ASM_SOF3091.Pages;

namespace ASM_SOF3091_Y4.Tests
{
    [TestFixture]
    public class ShoppingFlowTests
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();

            // Khởi tạo các Page Object
            _loginPage = new LoginPage(_driver);
            _inventoryPage = new InventoryPage(_driver);
        }

        [Test]
        public void LoginAndAddProductToCart_ShouldSucceed()
        {
            // 1. Mở trang và đăng nhập
            _loginPage.Open();
            _loginPage.Login("standard_user", "secret_sauce");

            // 2. Kiểm tra tiêu đề trang
            Assert.That(_inventoryPage.GetPageTitle(), Is.EqualTo("Products"));

            // 3. Thêm sản phẩm vào giỏ hàng và kiểm tra badge
            _inventoryPage.AddBackpackToCart();
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo("1"));
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }
    }
}