using FirstTests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace Lab4_SOF3091.Pages;

public class InventoryPage : BasePage
{
    private readonly By tieuDeTrang = By.CssSelector("[data-test='title']");
    private readonly By oSapXep = By.CssSelector("[data-test='product-sort-container']");
    private readonly By giaSanPham = By.CssSelector("[data-test='inventory-item-price']");

    public InventoryPage(IWebDriver driver) : base(driver) { }

    public bool DaHienThi()
    {
        try
        {
            return Tim(tieuDeTrang).Displayed;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public InventoryPage SapXep(string value)
    {
        new SelectElement(Tim(oSapXep)).SelectByValue(value);
        return this;
    }

    public List<double> LayDanhSachGia()
    {
        Tim(giaSanPham); // chờ danh sách sản phẩm tải xong
        return driver.FindElements(giaSanPham)
            .Select(e => double.Parse(e.Text.Replace("$", ""), CultureInfo.InvariantCulture))
            .ToList();
    }
}