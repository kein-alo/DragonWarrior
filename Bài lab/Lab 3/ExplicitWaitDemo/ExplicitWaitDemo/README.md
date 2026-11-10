# Phần A – Chứng minh vấn đề đồng bộ (Explicit Wait)

## Môi trường
- Visual Studio Code
- Java JDK 21
- Apache Maven 3.9.16
- Selenium WebDriver 4.27.0
- JUnit 5.11.4
- Google Chrome

## Cấu trúc
```text
ExplicitWaitDemo/
├── pom.xml
├── README.md
├── .gitignore
├── screenshots/
└── src/
    └── test/
        └── java/
            └── ExplicitWaitTest.java
```

## Trang kiểm thử
https://the-internet.herokuapp.com/dynamic_loading/1

Lưu ý: Dùng ví dụ `/1` vì phần tử `#finish` được thêm vào DOM sau khi nhấn Start.
Điều này giúp bài không Wait có thể ghi nhận `NoSuchElementException`.
Ở ví dụ `/2`, phần tử đã có trong DOM nhưng bị ẩn, nên có thể không tạo ra lỗi này.

## Chạy trên Windows PowerShell

Mở Terminal tại thư mục chứa `pom.xml`. Maven được gọi bằng đường dẫn đã cài trên máy:

```powershell
$MAVEN = "C:\Users\khoa6\Downloads\apache-maven-3.9.16-bin\apache-maven-3.9.16\bin\mvn.cmd"
```

### 1. Chạy test không Wait
```powershell
& $MAVEN "-Dtest=ExplicitWaitTest#testKhongSuDungWait" test
```

Test này cố ý tìm `#finish` ngay sau khi nhấn Start, không chờ.
Kết quả mong đợi là JUnit báo lỗi `NoSuchElementException` và Maven kết thúc bằng `BUILD FAILURE`.
Đây là lỗi có chủ đích để làm minh chứng cho yêu cầu bài tập.

### 2. Chạy test có Explicit Wait
```powershell
& $MAVEN "-Dtest=ExplicitWaitTest#testCoSuDungWait" test
```

Kết quả mong đợi: nội dung `Hello World!` và `BUILD SUCCESS`.

### 3. Chạy cả hai test
```powershell
& $MAVEN test
```

## Giải thích hiện tượng đồng bộ (2–3 câu)
Khi nhấn Start, website cần thời gian để tải và thêm nội dung Hello World! vào trang. Nếu Selenium tìm `#finish` ngay lập tức, phần tử chưa tồn tại nên phát sinh `NoSuchElementException`. Explicit Wait với `WebDriverWait` và `until()` giúp Selenium chờ đến khi phần tử hiển thị hoặc hết thời gian chờ, nhờ đó kiểm thử ổn định hơn.

## Ảnh minh chứng cần tự chụp
- `screenshots/A1_Khong_Wait_NoSuchElementException.png`
- `screenshots/A2_Explicit_Wait_PASS.png`

Chụp ảnh kết quả thực tế trong VS Code/Terminal sau khi chạy từng lệnh. Không dùng ảnh minh họa thay cho kết quả chạy thật.
