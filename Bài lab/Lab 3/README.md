## Phần A — Chứng minh vấn đề đồng bộ (Bài Lab 3)

**1. Mô tả vấn đề:**
Trên trang `https://the-internet.herokuapp.com/dynamic_loading/2`, khi bấm nút "Start", phần tử `#finish` (chứa chữ "Hello World!") không xuất hiện ngay lập tức mà được JavaScript tạo ra sau vài giây.

**2. Kết quả khi KHÔNG dùng Wait (Test RỚT):**
Nếu tìm `#finish` ngay sau khi bấm Start, Selenium sẽ ném ra lỗi `NoSuchElementException` vì phần tử chưa được tạo ra trong DOM.
*   *Minh chứng:* Ảnh `Screenshots/Lab3_PhanA_NoWait_Failed.png`

**3. Kết quả khi DÙNG Explicit Wait (Test PASS):**
Sử dụng `WebDriverWait` để đợi tối đa 10 giây cho đến khi phần tử `#finish` xuất hiện. Test chạy thành công và lấy được chữ "Hello World!".
*   *Minh chứng:* Ảnh `Screenshots/Lab3_PhanA_ExplicitWait_Passed.png`

**4. Giải thích nguyên nhân:**
Cùng một đoạn code tìm phần tử, nhưng kết quả khác nhau là do yếu tố thời gian (Timing). Khi không có Wait, Selenium chạy quá nhanh, tìm phần tử khi JavaScript của trang web chưa kịp render xong. Khi có Explicit Wait, Selenium sẽ liên tục kiểm tra lại (polling) cho đến khi phần tử thực sự xuất hiện, giúp đồng bộ hóa giữa tốc độ chạy của code test và tốc độ tải của trang web.

---

## Phần E — AI chẩn đoán flaky test (Bài Lab 3)

**1. Prompt đã dùng để hỏi AI:**
"Tôi có một đoạn code Selenium C# như sau: `driver.FindElement(By.Id("finish")).Text;`. Tôi chạy test này trên trang the-internet.herokuapp.com/dynamic_loading/2. Khi tôi bấm nút Start và chạy lệnh tìm element ngay lập tức, test thường xuyên bị lỗi NoSuchElementException (khoảng 80% số lần chạy). Đôi khi chạy lại thì lại Pass. Hãy chẩn đoán nguyên nhân gây ra lỗi chập chờn (flaky test) này và đề xuất cách khắc phục."

**2. Nguyên nhân AI đề xuất:**
*   AI chỉ ra rằng trang web sử dụng JavaScript để tải dữ liệu bất đồng bộ (AJAX). Phần tử `#finish` chưa có trong DOM tại thời điểm code thực thi lệnh tìm kiếm.
*   AI đề xuất sử dụng **Explicit Wait** (`WebDriverWait`) kết hợp với `Until` để đợi phần tử xuất hiện thay vì tìm ngay lập tức.
*   AI cũng cảnh báo không nên dùng `Thread.Sleep` vì nó làm chậm test và không đáng tin cậy.

**3. Nhận xét của sinh viên (Tự thẩm định):**
Gợi ý của AI là **hoàn toàn chính xác** và đúng trọng tâm vấn đề. Nguyên nhân gốc rễ đúng là do sự chênh lệch tốc độ giữa Selenium và JS trên trang. Gợi ý dùng `WebDriverWait` (Explicit Wait) là giải pháp tối ưu nhất vì nó giúp test vừa nhanh (đợi vừa đủ) lại vừa ổn định (không bị rớt). Gợi ý tránh dùng `Thread.Sleep` cũng rất thực tế vì đây là lỗi phổ biến của người mới bắt đầu. Tôi đã áp dụng `WebDriverWait` vào code và chạy lại 10 lần, cả 10 lần đều Pass, chứng minh AI đã chẩn đoán đúng.