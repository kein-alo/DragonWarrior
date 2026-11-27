# Demo NUnit: Category, chạy tuần tự và song song

## 1. Yêu cầu
- .NET 8 SDK
- Visual Studio hoặc VS Code

## 2. Chạy project
Mở Terminal tại thư mục chứa file `.csproj`, sau đó chạy:

```powershell
dotnet restore
dotnet test
```

## 3. Lọc Category Smoke
Với NUnit3TestAdapter, Category được ánh xạ thành `TestCategory` trong bộ lọc VSTest:

```powershell
dotnet test --filter "TestCategory=Smoke"
```

## 4. Loại trừ Category Slow

```powershell
dotnet test --filter "TestCategory!=Slow"
```

Lưu ý: Đề bài có thể ghi `Category=Smoke` và `Category!=Slow`, nhưng với NUnit chạy qua `dotnet test`, dùng `TestCategory` là cú pháp tương thích thông dụng.

## 5. Chạy tuần tự
Tạo file `sequential.runsettings` (đã có sẵn) và chạy:

```powershell
dotnet test --settings sequential.runsettings
```

## 6. Chạy song song
Cấu hình song song nằm trong `AssemblyInfo.cs`:
- `Parallelizable(ParallelScope.Fixtures)`: cho phép các TestFixture chạy song song.
- `LevelOfParallelism(4)`: tối đa 4 worker.

Chạy:

```powershell
dotnet test
```

## 7. Chụp 2 ảnh minh chứng
1. Chạy `dotnet test --filter "TestCategory=Smoke"` và chụp Terminal thấy lệnh + kết quả.
2. Chạy `dotnet test --filter "TestCategory!=Slow"` và chụp Terminal thấy lệnh + kết quả.

## 8. Kiểm tra tuần tự/song song
Chạy lần lượt hai lệnh ở mục 5 và 6. So sánh thời gian và log. Đây là project demo để kiểm tra cấu hình NUnit; khi đưa vào project Selenium thật, cần bảo đảm mỗi test dùng WebDriver riêng, không chia sẻ driver giữa các test chạy song song.
