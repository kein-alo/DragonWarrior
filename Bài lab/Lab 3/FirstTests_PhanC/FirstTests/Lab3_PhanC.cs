using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace FirstTests
{
    public class Lab3_PartC
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
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

        // ==========================================
        // PHẦN C1: XỬ LÝ ALERT, CONFIRM, PROMPT
        // ==========================================
        [Test]
        public void TestJavaScriptAlerts()
        {
            // Mở trang web chứa các nút Alert
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

            // --- 1. ALERT (Chỉ có nút OK) ---
            driver.FindElement(By.CssSelector("button[onclick='jsAlert()']")).Click();

            // Chuyển sang Alert và kiểm tra nội dung trước khi Accept
            IAlert jsAlert = driver.SwitchTo().Alert();
            Assert.That(jsAlert.Text, Is.EqualTo("I am a JS Alert"), "Sai nội dung JS Alert");
            jsAlert.Accept(); // Bấm OK

            // Assert kết quả hiển thị ở #result
            Assert.That(driver.FindElement(By.Id("result")).Text, Is.EqualTo("You successfully clicked an alert"), "Lỗi: Không xử lý được Alert");

            // --- 2. CONFIRM (Có nút OK và Cancel) ---
            driver.FindElement(By.CssSelector("button[onclick='jsConfirm()']")).Click();

            IAlert jsConfirm = driver.SwitchTo().Alert();
            Assert.That(jsConfirm.Text, Is.EqualTo("I am a JS Confirm"), "Sai nội dung JS Confirm");
            jsConfirm.Dismiss(); // Bấm Cancel (Dismiss)

            Assert.That(driver.FindElement(By.Id("result")).Text, Is.EqualTo("You clicked: Cancel"), "Lỗi: Không xử lý được Confirm Dismiss");

            // --- 3. PROMPT (Có ô nhập chữ) ---
            driver.FindElement(By.CssSelector("button[onclick='jsPrompt()']")).Click();

            IAlert jsPrompt = driver.SwitchTo().Alert();
            Assert.That(jsPrompt.Text, Is.EqualTo("I am a JS prompt"), "Sai nội dung JS Prompt");

            // Gõ chữ vào Prompt và bấm OK
            jsPrompt.SendKeys("Hello Selenium");
            jsPrompt.Accept();

            Assert.That(driver.FindElement(By.Id("result")).Text, Is.EqualTo("You entered: Hello Selenium"), "Lỗi: Không nhập được chữ vào Prompt");
        }

        // ==========================================
        // PHẦN C2: XỬ LÝ IFRAME
        // ==========================================
        [Test]
        public void TestIframeEditor()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/iframe");

            // 1. Chuyển vào iframe (thẻ iframe ở trang này có id là "mce_0_ifr")
            driver.SwitchTo().Frame("mce_0_ifr");

            // 2. Tìm thẻ body bên trong iframe và gõ chữ (xóa chữ cũ trước)
            var editor = driver.FindElement(By.Id("tinymce"));
            editor.Clear();
            editor.SendKeys("Xin chao, day la code test iframe!");

            // 3. Phải gọi DefaultContent() để thoát khỏi iframe trở về trang chính
            driver.SwitchTo().DefaultContent();

            // 4. (Tùy chọn) Kiểm tra xem đã thoát ra ngoài thành công chưa bằng cách tìm 1 phần tử ở trang chính
            var pageHeading = driver.FindElement(By.TagName("h3"));
            Assert.That(pageHeading.Text, Is.EqualTo("An iFrame containing the TinyMCE WYSIWYG Editor"), "Lỗi: Chưa thoát ra khỏi iframe");
        }
    }
}