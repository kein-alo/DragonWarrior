using NUnit.Framework;
using Lab5_SOF3091_phanBC.Core;
using Lab5_SOF3091_phanBC.Pages;
using Lab5_SOF3091_phanBC.Utilities;

namespace Lab5_SOF3091_phanBC.Tests;

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