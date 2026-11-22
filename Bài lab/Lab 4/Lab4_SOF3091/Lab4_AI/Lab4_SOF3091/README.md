# Lab 4 - OOP trong kiểm thử & Page Object Model (SOF3091)

## Cấu trúc

```
Lab4_SOF3091/
├── Lab4_SOF3091.slnx
├── Lab4_SOF3091/
│   ├── Pages/    BasePage.cs, LoginPage.cs, InventoryPage.cs
│   ├── Tests/    LoginTests.cs, SortPriceTests.cs   (không còn chữ "By")
│   └── Before/   LoginTests_Before.cs                (bản gốc Lab 2, để chạy "trước refactor")
├── screenshots/  ảnh chạy test trước và sau refactor
├── chay_truoc.bat, chay_sau.bat
└── README.md
```

## Cách chạy

Cần .NET SDK 10, Google Chrome và có mạng (trang saucedemo.com).

- Trước refactor: chạy `chay_truoc.bat`
- Sau refactor: chạy `chay_sau.bat`
- Hoặc: `dotnet test --filter "FullyQualifiedName~FirstTests.Tests.LoginTests"`

---

# Phần E — Thẩm định AI (Lab 4)

## 1. File được refactor

`LoginTests.cs` của Bài 2 (3 test: đăng nhập đúng, user bị khóa, thu thập 6 sản phẩm).
Chọn file này vì có đủ 3 kiểu thao tác (chuyển trang, ở lại trang + đọc lỗi, đọc danh sách) nên dễ lộ chỗ AI làm mất hành vi.
Bản gốc giữ ở `Lab4_SOF3091/Before/LoginTests_Before.cs`.

AI dùng: Claude (Anthropic).

## 2. Prompt đã dùng

```
Refactor file test Selenium C# (NUnit) bên dưới sang Page Object Model.
Giữ NGUYÊN hành vi: cùng các test, cùng các Assert, cùng dữ liệu.

Quy ước bắt buộc:
- Pages/ dùng namespace FirstTests.Pages; Tests/ dùng using FirstTests.Pages.
- BasePage (abstract) chứa driver, WebDriverWait và hàm protected Tim(By) có chờ.
  Mọi Page kế thừa BasePage, không tự khai báo lại driver/wait.
- Locator là private readonly By; dùng Tim() thay cho driver.FindElement().
- Tên hàm theo hành vi người dùng (DangNhap, SapXep, LayDanhSachGia...), không theo thẻ HTML.
- Chuyển trang: return new TrangMoi(driver); ở lại trang: return this;
- KHÔNG có Assert trong Page Object. Page Object KHÔNG trả về IWebElement.
- File test không được còn chữ "By".
- Chỉ tạo thêm Page mới nếu thật sự cần. Nêu rõ file nào mới, file nào sửa.

Đã có BasePage.cs, LoginPage.cs (dán kèm). InventoryPage.cs chưa có, hãy tạo.
[dán BasePage.cs, LoginPage.cs]
[dán LoginTests.cs]
```

## 3. Kết quả AI trả về

| File | Thay đổi |
|---|---|
| `Pages/BasePage.cs` | Giữ `Tim()`, thêm `TimTatCa(By)` (tìm nhiều phần tử, có chờ) |
| `Pages/LoginPage.cs` | Đổi namespace sang `FirstTests.Pages`, thêm `ThongBaoLoiDaHienThi()` |
| `Pages/InventoryPage.cs` | Mới: `DaHienThi`, `SapXep`, `LayDanhSachGia`, `LayDanhSachGiaHienThi`, `LayDanhSachTen`, `DemSanPham`, `LayDuongDan` |
| `Tests/LoginTests.cs` | Test đã chuyển sang POM, giữ nguyên tên 3 test |
| `Tests/SortPriceTests.cs` | Chỉ đổi `using` sang `FirstTests.Pages` |

## 4. Thẩm định

### 4.1 Chỗ AI làm chưa đúng

**Chữ "Assert" nằm trong comment của `LoginPage.cs` (bản AI trả đầu tiên).**
AI viết comment "(trả bool, KHÔNG Assert)". Khi tự chấm bằng tìm chuỗi `Assert` trong `Pages/` thì ra 1 kết quả, trái tiêu chí "không có Assert trong Page Object" nếu người chấm dùng Ctrl+F. Lỗi này chỉ ở hình thức, không đổi hành vi. Đã sửa comment, tìm lại ra 0 kết quả.

**Hạn chế còn lại (không phải lỗi quy ước):** theo đề, `DangNhap()` luôn trả về `InventoryPage`, kể cả khi đăng nhập thất bại. Vì vậy test user bị khóa phải bỏ qua giá trị trả về, trông không tự nhiên.

### 4.2 Chỗ dễ làm mất hành vi (đã kiểm, bản này giữ đúng)

| Hành vi của test gốc | Cách AI giữ lại | Nếu làm ẩu sẽ mất gì |
|---|---|---|
| `driver.Url` chứa `inventory.html` | `LayDuongDan()` | Đổi sang `DaHienThi()` là đổi nội dung kiểm |
| `error.Displayed` là true | `ThongBaoLoiDaHienThi()` | `Tim()` chỉ trả phần tử đang hiện, bỏ hàm này là mất 1 Assert |
| Mọi giá bắt đầu bằng `$` | `LayDanhSachGiaHienThi()` (chuỗi) | `LayDanhSachGia()` trả `decimal`, dùng nó thì không còn kiểm `$` |
| Tổng số Assert | 7 trước, 7 sau | Test xanh nhưng kiểm ít hơn |

### 4.3 Tiêu chí đã kiểm

| Tiêu chí | Cách kiểm | Kết quả |
|---|---|---|
| Không có `By` trong `Tests/` | tìm chuỗi `By` | 0 kết quả |
| Không có `Assert` trong `Pages/` | tìm chuỗi `Assert` | 0 kết quả (sau khi sửa) |
| Không Page nào trả `IWebElement` public | tìm `public.*IWebElement` | 0 kết quả (`Tim`, `TimTatCa` là `protected`) |
| Kế thừa `BasePage`, không khai báo lại driver/wait | đọc code | Đạt |
| Locator `private readonly By` | đọc code | Đạt |
| Dùng `Tim()`/`TimTatCa()` thay `FindElement` | đọc code | Đạt (`FindElement(s)` chỉ còn trong `BasePage`) |
| Namespace `FirstTests.Pages`, test có `using FirstTests.Pages` | đọc code | Đạt |
| Chuyển trang `return new ...`, ở lại trang `return this` | đọc code | Đạt |
| Locator mới ngoài test gốc | so với Lab 2–3 | Không có locator mới |
| Biên dịch | build thử với Selenium/NUnit giả lập | 0 lỗi (cần `dotnet build` thật để chắc) |

### 4.4 Khác biệt nhỏ chấp nhận được

- Page có chờ (`Tim`), test gốc thì không chờ, nên bản mới ổn định hơn.
- Khi không có sản phẩm nào: bản gốc rớt ở Assert đếm, bản mới hết 10 giây rồi ném `WebDriverTimeoutException`. Cả hai đều rớt, chỉ khác thông báo.

## 5. Chạy trước và sau refactor

Chạy `chay_truoc.bat` (ảnh `screenshots/lab4_truoc_refactor.png`), rồi `chay_sau.bat` (ảnh `screenshots/lab4_sau_refactor.png`).
Hai lần chạy dùng cùng một project, cùng 3 test cùng tên, chỉ khác lớp test (`FirstTests.LoginTests` trước, `FirstTests.Tests.LoginTests` sau).

| Test | Trước | Sau |
|---|---|---|
| Login_WithValidAccount_ShouldGoToInventoryPage | _điền_ | _điền_ |
| Login_WithLockedOutUser_ShouldShowErrorMessage | _điền_ | _điền_ |
| Inventory_ShouldListSixProducts_WithValidNamesAndPrices | _điền_ | _điền_ |

Kết luận: _điền sau khi chạy (hai lần phải giống hệt nhau)_.

Commit gợi ý: `refactor: chuyen LoginTests sang Page Object Model`
