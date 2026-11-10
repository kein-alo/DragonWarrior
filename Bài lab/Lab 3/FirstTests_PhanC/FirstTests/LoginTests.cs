using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace FirstTests
{
    public class LoginTests
    {
        private IWebDriver driver;

        // PHẦN B: Setup và TearDown chuẩn chỉnh
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            Thread.Sleep(1000); // Đợi 1 giây cho web load cho chắc
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        // Hàm dùng chung để đăng nhập (tránh lặp code)
        public void Login(string username, string password)
        {
            driver.FindElement(By.Id("user-name")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("login-button")).Click();
            Thread.Sleep(1000);
        }

        // ==========================================
        // PHẦN B: CA 1 - LUỒNG THUẬN
        // ==========================================
        [Test]
        public void TestLoginThanhCong()
        {
            Login("standard_user", "secret_sauce");

            // Assert: URL phải chứa inventory.html
            Assert.That(driver.Url, Does.Contain("inventory.html"), "Lỗi: Không chuyển sang trang Inventory");
        }

        // ==========================================
        // PHẦN B: CA 2 - LUỒNG NGHỊCH
        // ==========================================
        [Test]
        public void TestLoginThatBai()
        {
            // Dùng user bị khóa (locked_out_user)
            Login("locked_out_user", "secret_sauce");

            // Tìm thẻ chứa thông báo lỗi
            var errorElement = driver.FindElement(By.CssSelector("h3[data-test='error']"));

            // Assert: Thông báo lỗi phải hiển thị và đúng nội dung
            Assert.That(errorElement.Displayed, Is.True, "Lỗi: Không thấy thông báo lỗi");
            Assert.That(errorElement.Text, Does.Contain("Sorry, this user has been locked out."), "Lỗi: Sai nội dung thông báo");
        }

        // ==========================================
        // PHẦN C: THU THẬP DỮ LIỆU SẢN PHẨM
        // ==========================================
        [Test]
        public void TestThuThapDuLieuSanPham()
        {
            // 1. Đăng nhập
            Login("standard_user", "secret_sauce");

            // 2. Lấy danh sách sản phẩm
            var products = driver.FindElements(By.ClassName("inventory_item"));

            // Assert đếm số lượng
            Assert.That(products.Count, Is.EqualTo(6), "Lỗi: Không đủ 6 sản phẩm");

            bool coSanPhamSauceLabsBackpack = false;

            // 3. Duyệt từng sản phẩm
            foreach (var item in products)
            {
                // Bắt buộc: item.FindElement
                string name = item.FindElement(By.ClassName("inventory_item_name")).Text;
                string price = item.FindElement(By.ClassName("inventory_item_price")).Text;

                // In ra màn hình
                Console.WriteLine($"Tên: {name} | Giá: {price}");

                // 4. Assert chặt chẽ
                Assert.That(string.IsNullOrEmpty(name), Is.False, "Lỗi: Có sản phẩm bị rỗng tên");
                Assert.That(price.StartsWith("$"), Is.True, $"Lỗi: Giá '{price}' không bắt đầu bằng $");

                if (name == "Sauce Labs Backpack")
                {
                    coSanPhamSauceLabsBackpack = true;
                }
            }

            // Assert kiểm tra có đúng sản phẩm cụ thể
            Assert.That(coSanPhamSauceLabsBackpack, Is.True, "Lỗi: Không tìm thấy Sauce Labs Backpack");
        }
    }
}