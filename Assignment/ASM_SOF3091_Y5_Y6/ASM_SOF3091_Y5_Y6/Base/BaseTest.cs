using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ASM_SOF3091.Config;
using System;
using System.IO;

namespace ASM_SOF3091.Base
{
    public class BaseTest
    {
        protected IWebDriver Driver;
        protected AppSettings Settings;
        private static readonly object LockObject = new object();

        [SetUp]
        public void SetUp()
        {
            Settings = ConfigReader.LoadSettings();

            var options = new ChromeOptions();
            if (Settings.Headless)
            {
                options.AddArgument("--headless=new");
            }

            Driver = new ChromeDriver(options);
            Driver.Manage().Window.Maximize();

            WriteLog($"[START] Bắt đầu chạy test: {TestContext.CurrentContext.Test.Name}");
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var testName = TestContext.CurrentContext.Test.Name;

            if (status == TestStatus.Failed)
            {
                CaptureScreenshot(testName);
                WriteLog($"[FAILED] Test lỗi: {testName} | Lỗi: {TestContext.CurrentContext.Result.Message}");
            }
            else
            {
                WriteLog($"[PASSED] Hoàn thành test: {testName}");
            }

            Driver?.Quit();
            Driver?.Dispose();
        }

        private void CaptureScreenshot(string testName)
        {
            try
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                Directory.CreateDirectory(folderPath);

                // Làm sạch tên file tránh ký tự đặc biệt
                string safeName = string.Join("_", testName.Split(Path.GetInvalidFileNameChars()));
                string filePath = Path.Combine(folderPath, $"{safeName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                screenshot.SaveAsFile(filePath);
                WriteLog($"[SCREENSHOT] Đã lưu ảnh lỗi tại: {filePath}");
            }
            catch (Exception ex)
            {
                WriteLog($"[ERROR] Không thể chụp ảnh màn hình: {ex.Message}");
            }
        }

        protected void WriteLog(string message)
        {
            lock (LockObject) // Khóa luồng để tránh xung đột ghi file log khi chạy song song
            {
                try
                {
                    string logFolder = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
                    Directory.CreateDirectory(logFolder);
                    string logFile = Path.Combine(logFolder, "test_execution.log");

                    string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
                    TestContext.Progress.WriteLine(logLine);
                    File.AppendAllText(logFile, logLine + Environment.NewLine);
                }
                catch
                {
                    // Bỏ qua ngoại lệ nhỏ nếu có tranh chấp I/O để không làm rớt test
                }
            }
        }
    }
}