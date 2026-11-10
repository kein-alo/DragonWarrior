using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;

namespace FirstTests
{
    public class LoginTests
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        private void Login(string username, string password)
        {
            driver.FindElement(By.Id("user-name")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("login-button")).Click();
        }

        // ===== PHẦN B - Ca 1: luồng thuận =====
        [Test]
        public void Login_WithValidAccount_ShouldGoToInventoryPage()
        {
            Login("standard_user", "secret_sauce");

            Assert.That(driver.Url, Does.Contain("inventory.html"),
                "Đăng nhập đúng phải chuyển sang inventory.html");
        }

        // ===== PHẦN B - Ca 2: luồng nghịch =====
        [Test]
        public void Login_WithLockedOutUser_ShouldShowErrorMessage()
        {
            Login("locked_out_user", "secret_sauce");

            IWebElement error = driver.FindElement(By.CssSelector("[data-test='error']"));

            Assert.That(error.Displayed, Is.True, "Thông báo lỗi phải hiện ra");
            Assert.That(error.Text,
                Is.EqualTo("Epic sadface: Sorry, this user has been locked out."),
                "Nội dung lỗi phải đúng");
        }

        // ===== PHẦN C: thu thập dữ liệu sản phẩm =====
        [Test]
        public void Inventory_ShouldListSixProducts_WithValidNamesAndPrices()
        {
            Login("standard_user", "secret_sauce");

            IReadOnlyCollection<IWebElement> items =
                driver.FindElements(By.CssSelector("[data-test='inventory-item']"));

            var names = new List<string>();
            var prices = new List<string>();

            foreach (IWebElement item in items)
            {
                string name = item.FindElement(By.CssSelector("[data-test='inventory-item-name']")).Text;
                string price = item.FindElement(By.CssSelector("[data-test='inventory-item-price']")).Text;

                names.Add(name);
                prices.Add(price);

                TestContext.WriteLine($"{name} - {price}");
            }

            Assert.That(items.Count, Is.EqualTo(6), "Phải có đúng 6 sản phẩm");
            Assert.That(names, Does.Contain("Sauce Labs Backpack"));
            Assert.That(names.All(n => !string.IsNullOrWhiteSpace(n)), Is.True, "Không được có tên rỗng");
            Assert.That(prices.All(p => p.StartsWith("$")), Is.True, "Mọi giá phải bắt đầu bằng $");
        }
    }
}