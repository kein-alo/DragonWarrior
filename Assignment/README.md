# 🧪 Dự Án Kiểm Thử Tự Động - Assignment SOF3091 (Giai đoạn Y1 - Y6)

Dự án xây dựng khung kiểm thử tự động (Automation Test Framework) cho hệ thống thương mại điện tử mẫu [**SauceDemo**](https://www.saucedemo.com/) sử dụng ngôn ngữ **C#**, nền tảng **.NET SDK**, **Selenium WebDriver** và **NUnit**. 

Dự án được phân chia thành các module/project độc lập theo từng mốc yêu cầu (từ Y1 đến Y6) để các thành viên dễ dàng phát triển, kiểm thử và quản lý mã nguồn.

## 📋 Mục Lục

1. [Công nghệ sử dụng](#-công-nghệ-sử-dụng)
2. [Cấu trúc thư mục dự án](#-cấu-trúc-thư-mục-dự-án)
3. [Chi tiết các module (Y1 - Y6)](#-chi-tiết-các-module-y1---y6)
4. [Hướng dẫn cài đặt & Chạy kiểm thử](#-hướng-dẫn-cài-đặt--chạy-kiểm-thử)
5. [Báo cáo & Minh chứng](#-báo-cáo--minh-chứng)

---

## 🛠 Công Nghệ Sử Dụng

* **Ngôn ngữ lập trình:** C# (.NET 10.0 SDK)
* **Thư viện tự động hóa:** Selenium WebDriver (`Selenium.WebDriver`, `Selenium.Support`)
* **Test Framework:** NUnit 4 (`NUnit`, `NUnit3TestAdapter`)
* **Quản lý cấu hình & Dữ liệu:** `Microsoft.Extensions.Configuration.Json`, `System.Text.Json` / `Newtonsoft.Json`
* **Quản lý mã nguồn:** Git & GitHub (Phân nhánh theo từng thành viên/yêu cầu và tuân thủ Conventional Commits)

---

## 📂 Cấu Trúc Thư Mục Dự Án

Toàn bộ Solution được chia thành các thư mục project riêng biệt tương ứng với từng yêu cầu từ Y1 đến Y6:

```text
ASM_SOF3091/
├── ASM_SOF3091_Y1/              # Module Y1: Thiết lập môi trường & Test đầu tiên
├── ASM_SOF3091_Y2/              # Module Y2: Tự động hóa luồng cơ bản (Login, Cart)
├── ASM_SOF3091_Y3/              # Module Y3: Xử lý chờ tường minh (WebDriverWait)
├── ASM_SOF3091_Y4/              # Module Y4: Tái cấu trúc theo mô hình POM
├── ASM_SOF3091_Y5/              # Module Y5: Cấu hình (appsettings.json) & Data-Driven (JSON)
├── ASM_SOF3091_Y6/              # Module Y6: Logging, Báo cáo (.trx) & Chạy song song
├── screenshots/                 # Thư mục lưu toàn bộ ảnh minh chứng chạy test (Y1 - Y6)
└── README.md                    # Tài liệu hướng dẫn tổng quan dự án
```

---

## ✅ Chi Tiết Các Module (Y1 - Y6)

### 🔹 `ASM_SOF3091_Y1`: Thiết lập môi trường & Test đầu tiên
* Khởi tạo dự án NUnit trên .NET SDK, tích hợp đầy đủ các gói NuGet của Selenium.
* Viết kịch bản kiểm thử cơ bản truy cập `saucedemo.com` và xác minh tiêu đề trang.
* Áp dụng quy tắc **Conventional Commits** (`feat:`, `fix:`, `docs:`, `refactor:`).

### 🔹 `ASM_SOF3091_Y2`: Tự động hóa luồng với Selenium
* Tự động hóa hoàn chỉnh luồng **Đăng nhập** và luồng **Thêm sản phẩm vào giỏ hàng**.
* Ưu tiên sử dụng các bộ định vị (Locator) bền vững như `By.Id`, `By.CssSelector("[data-test='...']")`, hạn chế XPath phụ thuộc cấu trúc DOM.

### 🔹 `ASM_SOF3091_Y3`: Xử lý tình huống thực tế & Chờ tường minh
* **Cam kết 0% `Thread.Sleep`:** Loại bỏ hoàn toàn lệnh dừng cứng, thay thế bằng cơ chế chờ tường minh **`WebDriverWait`**.
* Đảm bảo tính ổn định: Thực thi kiểm thử **3 lần liên tiếp** đều đạt trạng thái **Passed (Xanh)**.

### 🔹 `ASM_SOF3091_Y4`: OOP & Page Object Model (POM)
* Xây dựng lớp cơ sở **`BasePage.cs`** chứa các phương thức tương tác phần tử kèm `WebDriverWait`.
* Tách biệt giao diện thành các lớp Page riêng (`LoginPage.cs`, `InventoryPage.cs`,...) với toàn bộ Locator được đóng gói ở phạm vi **`private readonly`**.
* **Tối ưu bảo trì:** Khi giao diện thay đổi thuộc tính của một phần tử, chỉ cần cập nhật duy nhất **1 dòng code** tại lớp Page tương ứng mà không phải sửa bất kỳ file Test nào.

### 🔹 `ASM_SOF3091_Y5`: Framework: Cấu hình & Data-Driven
* Tách biệt thông số môi trường (URL, trình duyệt, thời gian chờ) ra file `appsettings.json`.
* Thiết kế kiểm thử hướng dữ liệu (**Data-Driven Testing**) đọc dữ liệu từ file JSON bên ngoài thông qua thuộc tính **`[TestCaseSource]`**, bao phủ đầy đủ ca thường, ca biên và ca ngoại lệ (tối thiểu 6 test cases).

### 🔹 `ASM_SOF3091_Y6`: Test Runner, Báo cáo, Logging & Chạy song song
* Cấu hình thực thi song song theo cấp độ lớp với **`[Parallelizable(ParallelScope.Fixtures)]`** giúp tối ưu thời gian chạy bộ kiểm thử.
* Tích hợp ghi log tiến trình và **tự động chụp ảnh màn hình** lưu vào thư mục `screenshots/` khi có test case gặp lỗi (`Failed`).
* Xuất báo cáo kết quả kiểm thử tự động định dạng chuẩn `.trx`.

---

## 🚀 Hướng Dẫn Cài Đặt & Chạy Kiểm Thử

### 1. Yêu cầu hệ thống
* Đã cài đặt **.NET 10.0 SDK** (hoặc phiên bản .NET tương thích).
* Trình duyệt **Google Chrome** phiên bản mới nhất.

### 2. Các lệnh thực thi qua Terminal / CLI
Di chuyển vào thư mục của từng module (ví dụ `cd ASM_SOF3091_Y4`) hoặc chạy tại thư mục gốc của Solution:

* **Khôi phục gói thư viện & Biên dịch dự án:**
  ```bash
  dotnet restore
  dotnet build
  ```

* **Chạy toàn bộ kịch bản kiểm thử:**
  ```bash
  dotnet test
  ```

* **Chạy kiểm thử cho một module cụ thể (ví dụ Y4):**
  ```bash
  dotnet test ASM_SOF3091_Y4/ASM_SOF3091_Y4.csproj
  ```

* **Chạy kiểm thử và xuất báo cáo định dạng `.trx` (Y6):**
  ```bash
  dotnet test ASM_SOF3091_Y6/ASM_SOF3091_Y6.csproj --logger "trx;LogFileName=TestReport.trx" --results-directory ./Reports
  ```

---

## 📊 Báo Cáo & Minh Chứng

* Toàn bộ hình ảnh minh chứng quá trình thực thi (từ Y1 đến Y6), ảnh cấu trúc POM (`ASM_SOF3091_Y4`), kết quả chạy Data-Driven và bảng so sánh hiệu năng **Chạy tuần tự vs Chạy song song** được lưu trữ đầy đủ trong thư mục [`/screenshots`](./screenshots) và trình bày chi tiết trong file **Báo cáo tổng hợp (Word/PDF)** nộp kèm trên hệ thống.

*Dự án được thực hiện phục vụ môn học Kiểm thử Tự động (SOF3091).*