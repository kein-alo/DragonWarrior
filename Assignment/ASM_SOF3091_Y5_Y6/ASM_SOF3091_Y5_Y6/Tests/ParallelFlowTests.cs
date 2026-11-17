using NUnit.Framework;
using ASM_SOF3091.Base;
using ASM_SOF3091.Pages;

namespace ASM_SOF3091.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class ParallelFlowTests : BaseTest
    {
        [Test]
        public void AddProductToCart_ParallelFixture()
        {
            var loginPage = new LoginPage(Driver);
            var inventoryPage = new InventoryPage(Driver);

            loginPage.Open(Settings.BaseUrl);
            loginPage.Login("standard_user", "secret_sauce");

            inventoryPage.AddBackpackToCart();
            Assert.That(inventoryPage.GetCartItemCount(), Is.EqualTo("1"));
        }
    }
}