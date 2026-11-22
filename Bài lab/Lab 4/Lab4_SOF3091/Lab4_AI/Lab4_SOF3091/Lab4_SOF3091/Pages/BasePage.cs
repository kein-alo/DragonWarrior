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

        // Tìm NHIỀU phần tử, KÈM chờ: đợi tới khi có ít nhất 1 phần tử
        protected IReadOnlyList<IWebElement> TimTatCa(By locator)
        {
            return wait.Until(d =>
            {
                var els = d.FindElements(locator);
                return els.Count > 0 ? els : null;
            });
        }
    }
}
