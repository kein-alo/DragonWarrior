using System;
using System.IO;
using OpenQA.Selenium;

namespace Lab4_SOF3091.Utilities;

public static class ScreenshotHelper
{
    public static string Capture(IWebDriver? driver, string testName)
    {
        if (driver == null)
            return string.Empty;

        if (driver is not ITakesScreenshot screenshotDriver)
            return string.Empty;

        string folderPath = Path.Combine(AppContext.BaseDirectory, "screenshots");
        Directory.CreateDirectory(folderPath);

        string safeTestName = string.Concat(
            testName.Split(Path.GetInvalidFileNameChars()));
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
        string filePath = Path.Combine(folderPath, $"{safeTestName}_{timestamp}.png");

        Screenshot screenshot = screenshotDriver.GetScreenshot();
        screenshot.SaveAsFile(filePath);

        return filePath;
    }
}
