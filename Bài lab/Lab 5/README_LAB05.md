# LAB 05 – SOF3091

## Cấu trúc
- `Phần A/`: cấu hình Selenium/POM.
- `Phần B+C/`: project tích hợp cấu hình, WebDriver, POM, JSON và NUnit.
- `Phần E_PROMPT_AI.txt`: prompt sinh dữ liệu.
- `Phần E_KET_QUA_AI.json`: toàn bộ 10 bộ dữ liệu AI.
- `Phần E_BAO_CAO.md`: đánh giá dữ liệu, tích hợp, hướng dẫn chạy và danh sách ảnh minh chứng.
- `Screenshots/`: ảnh minh chứng đã có từ các phần trước.

## Chạy project tích hợp
Mở terminal tại `Phần B+C`:
```bash
dotnet restore
dotnet build
dotnet test --logger "console;verbosity=normal"
```
Yêu cầu .NET 8 SDK, Chrome và kết nối Internet đến SauceDemo.

**Lưu ý:** Báo cáo không điền trước số lượng test Passed/Failed. Hãy chạy trên máy và chụp ảnh kết quả thật để hoàn tất minh chứng.
