using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstTests.Pages
{
    public class InventoryPage : BasePage
    {
        private readonly By _cmbSapXep = By.CssSelector("[data-test='product-sort-container']");
        private readonly By _dsSanPham = By.CssSelector("[data-test='inventory-item']");
        private readonly By _lblTenSanPham = By.CssSelector("[data-test='inventory-item-name']");
        private readonly By _lblGiaSanPham = By.CssSelector("[data-test='inventory-item-price']");

        public InventoryPage(IWebDriver driver) : base(driver) { }

        public bool DaHienThi()
        {
            try { return Tim(_cmbSapXep).Displayed; }
            catch (WebDriverTimeoutException) { return false; }
        }

        // Test gốc kiểm URL chứa "inventory.html" -> giữ nguyên phép kiểm đó
        public string LayDuongDan() => driver.Url;

        // value: "az", "za", "lohi", "hilo"
        public InventoryPage SapXep(string value)
        {
            new SelectElement(Tim(_cmbSapXep)).SelectByValue(value);
            return this;
        }

        public int DemSanPham() => TimTatCa(_dsSanPham).Count;

        public List<string> LayDanhSachTen()
            => TimTatCa(_lblTenSanPham).Select(e => e.Text).ToList();

        // Giá dạng chuỗi như hiển thị ("$29.99") - để kiểm định dạng "$"
        public List<string> LayDanhSachGiaHienThi()
            => TimTatCa(_lblGiaSanPham).Select(e => e.Text).ToList();

        // Giá dạng số - dùng cho Is.Ordered
        public List<decimal> LayDanhSachGia()
            => LayDanhSachGiaHienThi()
                .Select(g => decimal.Parse(g.Replace("$", ""), CultureInfo.InvariantCulture))
                .ToList();
    }
}
