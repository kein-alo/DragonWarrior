using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FirstTests.Pages;

namespace Lab4_SOF3091.Tests
{
    [TestFixture]
    public class SortPriceTests
    {
        private IWebDriver driver = null!;

        [SetUp]
        public void SetUp() => driver = new ChromeDriver();

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void SapXepGiaTangDan_Chaining_ThanhCong()
        {
            var danhSachGia = new LoginPage(driver)
                .MoTrang("https://www.saucedemo.com/")
                .DangNhap("standard_user", "secret_sauce")
                .SapXep("lohi")
                .LayDanhSachGia();

            Assert.That(danhSachGia, Is.Ordered);
        }
    }
}
