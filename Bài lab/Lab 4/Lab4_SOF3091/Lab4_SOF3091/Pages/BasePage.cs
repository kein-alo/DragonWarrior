using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace FirstTests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;

        protected BasePage(IWebDriver driver, int timeoutSeconds = 10)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        // Hàm Tim(By) cốt lõi theo đúng yêu cầu Lab 4 Phần A
        protected IWebElement Tim(By locator)
        {
            return wait.Until(d =>
            {
                var el = d.FindElement(locator);
                return el.Displayed ? el : null;
            });
        }
    }
}