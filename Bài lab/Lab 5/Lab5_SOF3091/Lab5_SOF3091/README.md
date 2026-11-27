# Lab 5 – Framework: cấu trúc, cấu hình & Data-Driven Testing (SOF3091)

## Cấu trúc & đối chiếu với đề (tiền tố file = phần của đề)

| Phần | File |
|---|---|
| A – Config | `Config/appsettings.json`, `Config/A_TestConfig.cs`, `Config/A_ConfigReader.cs`, `Tests/A_ConfigTests.cs` |
| B – DriverFactory | `Core/B_DriverFactory.cs` (Chrome, Firefox, Edge; trả về `IWebDriver`) |
| C – BaseTest | `Core/C_BaseTest.cs`, `Utilities/C_ScreenshotHelper.cs` |
| D – Data-Driven | `Data/users.json`, `Data/D_UserData.cs`, `Utilities/D_JsonDataReader.cs`, `Pages/D_LoginPage.cs`, `Pages/D_InventoryPage.cs`, `Tests/D_LoginTests.cs` |
| E – Thẩm định AI | `Data/E_AI_Generated_10.json` + mục bên dưới |

> Tên file có tiền tố để phân biệt phần; tên class/namespace không đổi.

## Cách chạy
```powershell
dotnet test --logger "console;verbosity=normal"
```
- Đổi trình duyệt: sửa `"Browser"` trong `Config/appsettings.json` (`Chrome` / `Firefox` / `Edge`), không sửa code.
- Chạy ẩn: `"Headless": true`.
- Thêm ca test: thêm 1 object vào `Data/users.json`, không sửa file `.cs`.

## Ảnh minh chứng (đặt vào `screenshots/`)
- `chrome.png`, `firefox.png` – cùng code, chỉ đổi `Browser` (Phần B).
- `truoc-them-data.png`, `sau-them-data.png` – số ca test tăng khi thêm 1 bộ vào JSON (Phần D).

---

## Phần E – Thẩm định dữ liệu do AI sinh

### Prompt đã dùng
```
Hãy sinh 10 bộ dữ liệu test cho form đăng nhập https://www.saucedemo.com.
Trả về JSON mảng, mỗi phần tử đúng schema:
{ "Username": string, "Password": string, "ThanhCong": bool, "GhiChu": string }
Bao gồm cả ca hợp lệ, ca sai và ca biên.
```

### Kết quả AI trả về (toàn bộ – `Data/E_AI_Generated_10.json`)
| # | Username | Password | ThanhCong | GhiChu | Đánh giá |
|---|---|---|---|---|---|
| 1 | standard_user | secret_sauce | true | Hợp lệ | Dùng được |
| 2 | locked_out_user | secret_sauce | false | Tài khoản bị khóa | Dùng được |
| 3 | standard_user | wrong_password | false | Sai mật khẩu | Dùng được |
| 4 | unknown_user_123 | secret_sauce | false | Tài khoản không tồn tại | Dùng được |
| 5 | (rỗng) | (rỗng) | false | Bỏ trống cả hai | Dùng được |
| 6 | (rỗng) | secret_sauce | false | Bỏ trống username | Dùng được |
| 7 | standard_user | (rỗng) | false | Bỏ trống password | Dùng được |
| 8 | problem_user | secret_sauce | true | Hợp lệ problem_user | Dùng được |
| 9 | `<script>alert('x')</script>` | secret_sauce | false | Payload XSS | **KHÔNG dùng** |
| 10 | `' OR '1'='1` | anything | false | Payload SQL injection | **KHÔNG dùng** |

### Các bộ KHÔNG dùng được và lý do
- **Bộ 9 (XSS) và bộ 10 (SQLi):** đây là kiểm thử bảo mật, không thuộc phạm vi kiểm thử chức năng đăng nhập của bài này. SauceDemo là trang demo phía client, không có CSDL thật nên không thể xác nhận được "SQL injection" hay "XSS" qua việc chỉ thấy báo lỗi đăng nhập; ca test sẽ xanh nhưng không chứng minh được gì về an ninh (kết quả "false" chỉ vì tài khoản không tồn tại). Cần công cụ/quy trình pentest riêng. Vì vậy loại khỏi `users.json`.
- **Nhận xét thêm:** AI không sinh ca biên thật sự (khoảng trắng thừa, chữ hoa/thường, ký tự đặc biệt thông thường, chuỗi rất dài) và bộ 1–8 trùng với tài khoản mà trang đã cho sẵn. Các ca biên này được bổ sung thủ công vào `users.json`.

### Kết luận
Dữ liệu AI sinh chỉ là bản nháp: phải đối chiếu với hành vi thật của trang (ví dụ: SauceDemo phân biệt hoa/thường, không cắt khoảng trắng, không có khóa tài khoản sau nhiều lần sai) trước khi đưa vào bộ test.
