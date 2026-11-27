// PHẦN D - Data-Driven Testing: 1 hàm sinh N ca test từ Data/users.json (kế thừa BaseTest của Phần C)
using NUnit.Framework;
using Lab5_SOF3091.Core;
using Lab5_SOF3091.Pages;
using Lab5_SOF3091.Utilities;

namespace Lab5_SOF3091.Tests;

public class LoginTests : BaseTest
{
    public static IEnumerable<TestCaseData> DuLieuDangNhap()
    {
        var users = JsonDataReader.DocUsers();
        for (int i = 0; i < users.Count; i++)
        {
            var u = users[i];
            yield return new TestCaseData(u.Username, u.Password, u.ThanhCong)
                .SetName($"DangNhap_{i:00}_{u.GhiChu}");
        }
    }

    [TestCaseSource(nameof(DuLieuDangNhap))]
    public void DangNhap_TheoDuLieu(string username, string password, bool thanhCong)
    {
        new LoginPage(Driver).DangNhap(username, password);

        Assert.That(new InventoryPage(Driver).DaHienThi(), Is.EqualTo(thanhCong),
            $"Username='{username}' - kỳ vọng ThanhCong={thanhCong}");
    }
}