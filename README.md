
## 📁 1. Cấu trúc Tổng quan Repository Toàn môn

Repository này được tổ chức để lưu trữ toàn bộ các bài tập thực hành (Lab), bài tập lớn và đồ án (ASM) xuyên suốt quá trình học tập:

```text
Repository-Root/
├── .gitignore                      # File cấu hình bỏ qua các thư mục tạm (bin/, obj/, .vs)
├── README.md                       # Tài liệu báo cáo & Hướng dẫn sử dụng chung toàn môn
├── screenshots/                    # Thư mục lưu trữ ảnh minh chứng hệ thống cho các bài
├── Lab1-Selenium-CSharp/           # Phần thực hành: Cài đặt môi trường, Selenium WebDriver & NUnit
├── ASM1-FastFood-Web/              # Đồ án phần 1: Xây dựng ứng dụng Website ASP.NET Core MVC & API
└── ASM2-Testing-Reports/           # Đồ án phần 2: Lập Test Plan, 307 Test Cases & Quản lý Bug trên Jira
```

---

## 🚀 2. Hướng dẫn Chung & Cách Vận hành Dự án

### Điều kiện tiên quyết:
- Đã cài đặt **.NET SDK** (phiên bản 8.0 trở lên).
- Sử dụng IDE: **Visual Studio 2022** hoặc **VS Code** (kèm C# Dev Kit).
- Cài đặt **Microsoft SQL Server** (dùng cho các bài tập lớn/đồ án có kết nối cơ sở dữ liệu).

### Các bước chạy kiểm thử tự động (Selenium / NUnit):
1. Clone repository về máy tính:
   ```bash
   git clone https://github.com/<tai-khoan-cua-ban>/<ten-repository>.git
   ```
2. Di chuyển đến thư mục chứa project cần chạy (Ví dụ: `Lab1-Selenium-CSharp/FirstTests`):
   ```bash
   cd Lab1-Selenium-CSharp/FirstTests
   ```
3. Khôi phục các gói thư viện phụ thuộc:
   ```bash
   dotnet restore
   ```
4. Thực thi kịch bản kiểm thử:
   ```bash
   dotnet test
   ```

---

## 📋 3. Tóm tắt Nội dung & Sản phẩm Đạt được qua các Giai đoạn

- **Phần Thực hành Lab:** Cài đặt, cấu hình thành công môi trường kiểm thử tự động, viết kịch bản kiểm thử giao diện web sử dụng Selenium WebDriver và NUnit, kết hợp quản lý mã nguồn bằng Git/GitHub.
- **Đồ án Đặt tả & Xây dựng Hệ thống (ASM 1):** Xây dựng hoàn chỉnh ứng dụng website thương mại điện tử / đặt đồ ăn nhanh (`ASM_FastFood`) trên nền tảng ASP.NET Core MVC và Minimal API.
- **Đồ án Kiểm thử Chất lượng (ASM 2):** Thiết lập tài liệu Kế hoạch kiểm thử (`Test Plan`), xây dựng bộ 307 Test Cases bao phủ toàn diện từ Front-end đến Back-end, kết hợp sử dụng **Atlassian Jira Scrum Board** để quản lý tiến độ và báo cáo lỗi (Bug List).