# PHẦN E – AI TEST DATA + TÍCH HỢP

## 1. Mục tiêu
- Dùng AI tạo 10 bộ dữ liệu kiểm thử đăng nhập theo schema `UserData`.
- Lưu prompt và toàn bộ kết quả AI.
- Đánh giá tính hợp lý, chọn dữ liệu có thể dùng và dữ liệu không phù hợp.
- Tích hợp cấu hình, POM, đọc JSON, test và screenshot vào project chung.
- Chạy toàn bộ test, lưu ảnh minh chứng và tổng hợp báo cáo.

## 2. Schema dữ liệu

| Trường | Kiểu | Ý nghĩa |
|---|---|---|
| Username | string | Tên đăng nhập |
| Password | string | Mật khẩu |
| ThanhCong | boolean | Kỳ vọng đăng nhập thành công |
| GhiChu | string | Mô tả test case |

Schema này khớp với `Data/UserData.cs`.

## 3. Prompt và kết quả AI
- Prompt đầy đủ: `Phần E_PROMPT_AI.txt`.
- Toàn bộ 10 object AI: `Phần E_KET_QUA_AI.json` và `Data/AI_Generated_10.json`.
- Mỗi object đều có đủ 4 thuộc tính, đúng kiểu dữ liệu JSON theo schema.

## 4. Đánh giá 10 bộ dữ liệu

| STT | Username | Kỳ vọng | Đánh giá |
|---:|---|---|---|
| 1 | standard_user | true | Dùng được – tài khoản mẫu hợp lệ của SauceDemo |
| 2 | locked_out_user | false | Dùng được – kiểm tra tài khoản bị khóa |
| 3 | standard_user | false | Dùng được – sai mật khẩu |
| 4 | unknown_user_123 | false | Dùng được – dữ liệu giả lập tài khoản không tồn tại |
| 5 | (rỗng) | false | Dùng được – kiểm tra bỏ trống cả hai trường |
| 6 | (rỗng) | false | Dùng được – kiểm tra bỏ trống username |
| 7 | standard_user | false | Dùng được – kiểm tra bỏ trống password |
| 8 | problem_user | true | Dùng được – tài khoản mẫu SauceDemo |
| 9 | payload HTML/JavaScript | false | Không dùng trong bộ test chức năng đăng nhập; thuộc phạm vi kiểm thử bảo mật XSS |
| 10 | payload SQL injection | false | Không dùng trong bộ test chức năng đăng nhập; thuộc phạm vi kiểm thử bảo mật SQL injection |

**Lý do loại ít nhất 2 bộ:** Bộ 9 và 10 có đủ thuộc tính và kiểu dữ liệu nhưng mục tiêu là kiểm tra payload bảo mật. Bài test hiện tại chỉ xác minh luồng đăng nhập và trạng thái hiển thị trang Inventory, không có tiêu chí bảo mật, kiểm tra mã hóa/escape hay kiểm thử phía máy chủ. Nếu đưa vào bộ test chức năng này, kết quả dễ bị diễn giải sai. Hai bộ vẫn được lưu nguyên vẹn trong file kết quả AI để minh chứng quá trình đánh giá; không đưa vào `users.json` chạy tự động.

## 5. Kiểm tra tính hợp lý
- Kiểm tra đủ 10 object và đủ 4 trường.
- `Username`, `Password`, `GhiChu` là chuỗi; `ThanhCong` là boolean.
- Dữ liệu dương sử dụng tài khoản mẫu SauceDemo đã biết: `standard_user`, `problem_user`.
- Dữ liệu âm bao gồm tài khoản khóa, sai mật khẩu, tài khoản giả lập không tồn tại và các trường rỗng.
- Hai payload bảo mật được gắn nhãn ngoài phạm vi, không chạy trong bộ test chức năng.
- Lưu ý: tài khoản mẫu và hành vi trang đích có thể thay đổi; cần chạy thực tế để xác nhận.

## 6. Tích hợp A–D vào solution chung
Project tích hợp nằm tại `Phần B+C/`:
- A – `Config/`, `ConfigReader.cs`, `TestConfig.cs`: đọc và kiểm tra cấu hình.
- B – `Core/DriverFactory.cs`, `Core/BaseTest.cs`: khởi tạo WebDriver, setup/teardown.
- C – `Pages/`, `Data/`, `Utilities/`, `Tests/`: POM, JSON data-driven test, đọc dữ liệu và chụp ảnh khi test lỗi.
- D – thư mục `Screenshots/` ở cấp `Lab 5/` giữ lại ảnh Git minh chứng ban đầu; tiếp tục bổ sung ảnh chạy test vào `Screenshots/Run/`.

Đã chỉnh lại nội dung hai file Page Object để tên class trùng với tên file:
- `Pages/LoginPage.cs` chứa class `LoginPage`.
- `Pages/InventoryPage.cs` chứa class `InventoryPage`.

`users.json` chứa 8 trường hợp phù hợp với kiểm thử chức năng; `AI_Generated_10.json` giữ đủ 10 kết quả AI, gồm cả 2 trường hợp bị loại.

## 7. Cách chạy toàn bộ test
Yêu cầu: .NET 8 SDK, Visual Studio hoặc terminal, Chrome đã cài đặt và có kết nối Internet để truy cập SauceDemo.

Mở terminal tại thư mục `Phần B+C`, chạy:

```bash
dotnet restore
dotnet build
dotnet test --logger "console;verbosity=normal"
```

Kết quả mong đợi: nhóm `ConfigTests` và `LoginTests` được thực thi. `LoginTests` đọc dữ liệu từ `Data/users.json`; mỗi test case tạo và đóng WebDriver riêng. Nếu test thất bại, ảnh được lưu trong thư mục `screenshots` bên trong thư mục output của test và đính kèm vào kết quả NUnit.

Không ghi số Passed/Failed giả định vào báo cáo. Điền số liệu theo kết quả chạy thật trên máy.

## 8. Ảnh minh chứng cần chụp
Lưu vào `Screenshots/Run/`:
1. `E01_prompt_ai.png` – prompt yêu cầu AI tạo 10 bộ dữ liệu.
2. `E02_ket_qua_10_data.png` – kết quả đủ 10 object.
3. `E03_json_schema.png` – file JSON và cấu trúc 4 trường.
4. `E04_solution_build.png` – lệnh `dotnet build` thành công.
5. `E05_all_tests.png` – kết quả `dotnet test`, thể hiện Passed/Failed/Skipped.
6. `E06_test_explorer.png` – Test Explorer hiển thị toàn bộ test.
7. `E07_screenshots_folder.png` – thư mục ảnh lỗi nếu có; nếu không có lỗi, chụp thư mục và ghi rõ không phát sinh ảnh lỗi tự động.

## 9. Mẫu tổng hợp kết quả chạy

| Hạng mục | Kết quả thực tế |
|---|---|
| Build | Điền theo terminal |
| Tổng test | Điền theo `dotnet test` |
| Passed | Điền theo `dotnet test` |
| Failed | Điền theo `dotnet test` |
| Skipped | Điền theo `dotnet test` |
| Ảnh minh chứng | Liệt kê file đã chụp |

## 10. Kết luận mẫu
Qua phần E, nhóm đã sử dụng AI để tạo dữ liệu kiểm thử theo schema, lưu lại prompt và toàn bộ kết quả. Nhóm kiểm tra kiểu dữ liệu, nội dung và mục đích của từng trường hợp; hai payload bảo mật được giữ trong kết quả AI nhưng loại khỏi bộ test chức năng vì khác phạm vi kiểm thử. Các thành phần cấu hình, WebDriver, Page Object, dữ liệu JSON và NUnit được tích hợp trong project chung. Kết quả cuối cùng cần căn cứ vào lần chạy `dotnet test` thực tế và ảnh minh chứng đính kèm.
