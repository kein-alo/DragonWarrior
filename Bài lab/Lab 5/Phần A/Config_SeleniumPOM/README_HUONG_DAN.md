# Config + kiểm thử cấu hình Selenium POM

## Nội dung
- `Config/appsettings.json`: 4 tham số BaseUrl, Browser, Timeout, Headless.
- `Utilities/ConfigReader.cs`: đọc và kiểm tra cấu hình JSON.
- `Tests/TestConfig.cs`: 5 kiểm thử cấu hình bằng NUnit.
- `Base/BaseTest.cs`: khởi tạo Chrome hoặc Firefox theo cấu hình.
- `.csproj`: tự copy JSON vào thư mục output.

## Cách sử dụng
1. Giải nén và chép các thư mục/file vào thư mục dự án Selenium POM hiện tại.
2. Nếu dự án đã có `.csproj`, KHÔNG ghi đè toàn bộ file này. Chỉ thêm ItemGroup CopyToOutputDirectory vào `.csproj` hiện tại; giữ nguyên package và TargetFramework đang dùng.
3. Nếu dự án đang dùng namespace khác `SeleniumPOM_CSharp`, sửa namespace và các using cho đồng nhất.
4. Chạy tại thư mục có `.csproj`:

```powershell
dotnet restore
dotnet build
dotnet test --logger "console;verbosity=normal"
```

## Đổi trình duyệt
Trong `Config/appsettings.json`:
- `"Browser": "Chrome"` để chạy Chrome.
- `"Browser": "Firefox"` để chạy Firefox.

Không cần sửa code C# khi đổi trình duyệt.

## Kiểm tra JSON đã được copy
Ví dụ với .NET 8:

```powershell
Test-Path ".\\bin\\Debug\\net8.0\\Config\\appsettings.json"
```

Kết quả `True` nghĩa là file đã được copy.

## Kiểm tra URL viết cứng trong C#
```powershell
Get-ChildItem -Recurse -Filter *.cs |
  Select-String "https?://"
```

Nếu dự án còn URL ở test/page khác, chuyển URL đó sang `appsettings.json` và sử dụng `ConfigReader.Settings.BaseUrl`.

Lưu ý: `BaseTest` trong bộ mẫu là lớp cơ sở. Chỉ những test cần mở trình duyệt mới kế thừa `BaseTest`; `TestConfig` không cần kế thừa lớp này.
