using OpenQA.Selenium;

namespace ASM_SOF3091.Pages
{
    public class InventoryPage : BasePage
    {
        private readonly By _pageTitle = By.ClassName("title");
        private readonly By _addBackpackBtn = By.Id("add-to-cart-sauce-labs-backpack");
        private readonly By _cartBadge = By.ClassName("shopping_cart_badge");

        public InventoryPage(IWebDriver driver) : base(driver) { }

        public string GetPageTitle()
        {
            return GetElementText(_pageTitle);
        }

        public void AddBackpackToCart()
        {
            ClickElement(_addBackpackBtn);
        }

        public string GetCartItemCount()
        {
            return GetElementText(_cartBadge);
        }
    }
}