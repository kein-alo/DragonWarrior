using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Lab5_SOF3091_phanBC.Pages;

/// <summary>Page Object cho trang danh sách sản phẩm sau đăng nhập.</summary>
public sealed class InventoryPage
{
    private readonly IWebDriver _driver;
    public InventoryPage(IWebDriver driver) => _driver = driver;

    public bool DaHienThi()
    {
        try
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            return wait.Until(d => d.FindElements(By.Id("inventory_container")).Count > 0);
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }
}
