using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace ASM_SOF3091.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

        public BasePage(IWebDriver driver, int timeoutInSeconds = 10)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        protected IWebElement WaitAndFindElement(By locator)
        {
            return Wait.Until(d =>
            {
                var element = d.FindElement(locator);
                return element.Displayed ? element : null;
            });
        }

        protected void EnterText(By locator, string text)
        {
            var element = WaitAndFindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected void ClickElement(By locator)
        {
            var element = Wait.Until(d =>
            {
                var el = d.FindElement(locator);
                return (el.Displayed && el.Enabled) ? el : null;
            });
            element.Click();
        }

        protected string GetElementText(By locator)
        {
            return WaitAndFindElement(locator).Text;
        }
    }
}