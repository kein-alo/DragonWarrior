using FirstTests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FirstTests.Tests
{
    public class LoginTests
    {
        private IWebDriver driver = null!;
        private LoginPage loginPage = null!;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            loginPage = new LoginPage(driver).MoTrang("https://www.saucedemo.com");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        // ===== PHẦN B - Ca 1: luồng thuận =====
        [Test]
        public void Login_WithValidAccount_ShouldGoToInventoryPage()
        {
            var inventoryPage = loginPage.DangNhap("standard_user", "secret_sauce");

            Assert.That(inventoryPage.LayDuongDan(), Does.Contain("inventory.html"),
                "Đăng nhập đúng phải chuyển sang inventory.html");
        }

        // ===== PHẦN B - Ca 2: luồng nghịch =====
        [Test]
        public void Login_WithLockedOutUser_ShouldShowErrorMessage()
        {
            // Đăng nhập thất bại -> vẫn ở LoginPage, nên bỏ qua InventoryPage trả về
            loginPage.DangNhap("locked_out_user", "secret_sauce");

            Assert.That(loginPage.ThongBaoLoiDaHienThi(), Is.True, "Thông báo lỗi phải hiện ra");
            Assert.That(loginPage.LayThongBaoLoi(),
                Is.EqualTo("Epic sadface: Sorry, this user has been locked out."),
                "Nội dung lỗi phải đúng");
        }

        // ===== PHẦN C: thu thập dữ liệu sản phẩm =====
        [Test]
        public void Inventory_ShouldListSixProducts_WithValidNamesAndPrices()
        {
            var inventoryPage = loginPage.DangNhap("standard_user", "secret_sauce");

            var names = inventoryPage.LayDanhSachTen();
            var prices = inventoryPage.LayDanhSachGiaHienThi();

            foreach (var (name, price) in names.Zip(prices))
            {
                TestContext.WriteLine($"{name} - {price}");
            }

            Assert.That(inventoryPage.DemSanPham(), Is.EqualTo(6), "Phải có đúng 6 sản phẩm");
            Assert.That(names, Does.Contain("Sauce Labs Backpack"));
            Assert.That(names.All(n => !string.IsNullOrWhiteSpace(n)), Is.True, "Không được có tên rỗng");
            Assert.That(prices.All(p => p.StartsWith("$")), Is.True, "Mọi giá phải bắt đầu bằng $");
        }
    }
}
