# Báo cáo Tổng hợp Quá trình Học tập & Dự án Môn học: Kiểm thử Nâng cao cùng AI (SOF3091)

## 📁 1. Cấu trúc Thư mục Dự án (DragonWarrior)

Repository này được tổ chức chuẩn hóa theo đúng cấu trúc thực tế của môn học:

```text
DragonWarrior/
├── .gitignore                      # File cấu hình bỏ qua các thư mục rác (bin/, obj/, .vs)
├── README.md                       # Báo cáo tổng quan & Hướng dẫn vận hành dự án
├── Assignment/                     # Đồ án Assignment kiểm thử nâng cao (ASM1 & ASM2)
└── Bài lab/                        # Thư mục lưu trữ toàn bộ 8 bài thực hành Lab
    ├── Lab 1/                      # Thiết lập môi trường .NET, Selenium WebDriver & NUnit
    ├── Lab 2/                      # Kiểm thử tự động với Selenium
    ├── Lab 3/                      # Áp dụng AI trong thiết kế kịch bản Test
    ├── Lab 4/                      # Kiểm thử REST API (Minimal API)
    ├── Lab 5/                      # Quản lý Bug & Jira Scrum Board
    ├── Lab 6/                      # Kiểm thử hiệu năng & Bảo mật cơ bản
    ├── Lab 7/                      # Kiểm thử tự động nâng cao
    └── Lab 8/                      # Tổng kết & Hoàn thiện báo cáo chất lượng
```

---

## 🚀 2. Hướng dẫn Vận hành Dự án

### Yêu cầu môi trường:
- **.NET SDK** (phiên bản 8.0 trở lên).
- **Visual Studio 2022** hoặc **VS Code** (kèm extension C# Dev Kit).
- Trình duyệt **Google Chrome** & **ChromeDriver** tương thích.
- **Microsoft SQL Server** (phục vụ các bài tập kiểm thử kết nối CSDL).

### Các bước chạy bài tập thực hành (Bài lab):
1. Clone repository về máy:
   ```bash
   git clone https://github.com/<tai-khoan-cua-ban>/DragonWarrior.git
   ```
2. Di chuyển vào thư mục bài Lab cần chạy (Ví dụ: `Bài lab/Lab 1`):
   ```bash
   cd "Bài lab/Lab 1"
   ```
3. Khôi phục các gói phụ thuộc (Dependencies):
   ```bash
   dotnet restore
   ```
4. Chạy kịch bản kiểm thử:
   ```bash
   dotnet test
   ```

---

## 📋 3. Tóm tắt Nội dung Thực hiện

- **Thư mục `Bài lab/`:** Hoàn thành trọn vẹn 8 bài lab thực hành từ việc thiết lập môi trường, viết kịch bản kiểm thử tự động với Selenium/NUnit đến kiểm thử API và ứng dụng AI vào quy trình test.
- **Thư mục `Assignment/`:** Xây dựng kế hoạch kiểm thử (`Test Plan`), bộ kịch bản test case phủ rộng các module chức năng, quản lý luồng công việc và báo cáo lỗi (Bugs) trên hệ thống **Jira Scrum Board**.