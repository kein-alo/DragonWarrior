using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Lab5_SOF3091.Pages;

/// <summary>PHẦN D - Page Object trang danh sách sản phẩm sau đăng nhập.</summary>
public sealed class InventoryPage
{
    private static readonly By InventoryContainer = By.Id("inventory_container");

    private readonly IWebDriver _driver;
    private readonly TimeSpan _timeout;

    // 3 giây: mọi ca nghịch đều phải chờ hết ngần này trước khi trả về false
    public InventoryPage(IWebDriver driver, int timeoutSeconds = 3)
    {
        _driver = driver;
        _timeout = TimeSpan.FromSeconds(timeoutSeconds);
    }

    /// <summary>Trả về true/false, KHÔNG ném lỗi khi không vào được trang sản phẩm.</summary>
    public bool DaHienThi()
    {
        try
        {
            var wait = new WebDriverWait(_driver, _timeout);
            // FindElements (số nhiều) trả danh sách rỗng thay vì ném NoSuchElementException
            return wait.Until(d => d.FindElements(InventoryContainer).Count > 0);
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }
}
