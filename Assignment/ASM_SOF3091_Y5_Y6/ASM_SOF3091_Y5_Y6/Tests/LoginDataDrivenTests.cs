using NUnit.Framework;
using ASM_SOF3091.Base;
using ASM_SOF3091.Pages;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ASM_SOF3091.Tests
{
    // Thiết lập chạy song song ở cấp độ Class (Fixtures) cho Yêu cầu 6
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class LoginDataDrivenTests : BaseTest
    {
        public class LoginTestData
        {
            public string CaseName { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public bool IsSuccess { get; set; }
            public string ExpectedMessage { get; set; }

            public override string ToString() => CaseName;
        }

        public static IEnumerable<LoginTestData> GetLoginData()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "login_data.json");
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<LoginTestData>>(json);
        }

        [Test, TestCaseSource(nameof(GetLoginData))]
        public void TestLogin_DataDriven(LoginTestData data)
        {
            var loginPage = new LoginPage(Driver);
            var inventoryPage = new InventoryPage(Driver);

            WriteLog($"Đang mở URL: {Settings.BaseUrl}");
            loginPage.Open(Settings.BaseUrl);

            WriteLog($"Đăng nhập với user: '{data.Username}'");
            loginPage.Login(data.Username, data.Password);

            if (data.IsSuccess)
            {
                Assert.That(inventoryPage.GetPageTitle(), Is.EqualTo(data.ExpectedMessage));
            }
            else
            {
                // Lưu ý: Thêm hàm GetErrorMessage() vào LoginPage từ Y4 nếu chưa có
                Assert.That(loginPage.GetErrorMessage(), Does.Contain(data.ExpectedMessage));
            }
        }
    }
}