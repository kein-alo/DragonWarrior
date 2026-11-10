using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstTests
{
    public class SortTests
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

        // Lấy danh sách giá hiện trên trang, bỏ ký tự $ và đổi sang số
        private List<decimal> GetPrices()
        {
            IReadOnlyCollection<IWebElement> priceElements =
                driver.FindElements(By.CssSelector("[data-test='inventory-item-price']"));

            return priceElements
                .Select(e => decimal.Parse(
                    e.Text.Replace("$", ""),
                    CultureInfo.InvariantCulture))
                .ToList();
        }

        // ===== PHẦN B: dropdown + Assert giá tăng dần =====
        [Test]
        public void Inventory_SortByPriceLowToHigh_ShouldListPricesAscending()
        {
            Login("standard_user", "secret_sauce");

            // Chọn sắp xếp trong dropdown
            IWebElement dropdown =
                driver.FindElement(By.CssSelector("[data-test='product-sort-container']"));
            SelectElement select = new SelectElement(dropdown);
            select.SelectByText("Price (low to high)");

            // Lấy 6 giá sau khi sắp xếp
            List<decimal> actualPrices = GetPrices();

            foreach (decimal price in actualPrices)
            {
                TestContext.Out.WriteLine($"${price}");
            }

            // Assert 1: có đủ 6 giá
            Assert.That(actualPrices.Count, Is.EqualTo(6), "Phải có đúng 6 giá");

            // Assert 2: danh sách THỰC SỰ tăng dần
            // so sánh danh sách vừa lấy với chính nó sau khi đã sắp xếp
            List<decimal> expectedPrices = actualPrices.OrderBy(p => p).ToList();
            Assert.That(actualPrices, Is.EqualTo(expectedPrices),
                "Giá phải được sắp xếp tăng dần");
        }
    }
}