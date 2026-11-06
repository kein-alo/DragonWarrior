# PHẦN A – BẢNG LOCATOR SAUCEDEMO

- Website: https://www.saucedemo.com/
- Công cụ kiểm tra: Google Chrome DevTools
- Cách kiểm tra: mở DevTools bằng `F12` hoặc `Ctrl + Shift + I`, chọn tab **Elements**, nhấn `Ctrl + F`, nhập locator và kiểm tra kết quả hiển thị `1 of 1`.
- Lưu ý: chỉ đánh dấu locator đạt sau khi đã tự kiểm tra trên website và chụp ảnh minh chứng.

## 1. Bảng Locator

| STT | Phần tử | Loại locator | Giá trị locator | Kết quả Ctrl+F | Ảnh minh chứng |
|---:|---|---|---|---|---|
| 1 | Username | CSS Selector | `#user-name` | Chưa kiểm tra | `screenshots/01-username-id.png` |
| 2 | Username | CSS Selector | `input[name="user-name"]` | Chưa kiểm tra | `screenshots/02-username-name.png` |
| 3 | Password | CSS Selector | `#password` | Chưa kiểm tra | `screenshots/03-password-id.png` |
| 4 | Password | CSS Selector | `input[name="password"]` | Chưa kiểm tra | `screenshots/04-password-name.png` |
| 5 | Login | CSS Selector | `#login-button` | Chưa kiểm tra | `screenshots/05-login-id.png` |
| 6 | Login | CSS Selector | `input[type="submit"]` | Chưa kiểm tra | `screenshots/06-login-submit.png` |
| 7 | Tên sản phẩm (Sauce Labs Backpack) | CSS Selector | `#item_4_title_link` | Chưa kiểm tra | `screenshots/07-product-name-id.png` |
| 8 | Tên sản phẩm (Sauce Labs Backpack) | XPath | `//*[@id="item_4_title_link"]` | Chưa kiểm tra | `screenshots/08-product-name-xpath.png` |
| 9 | Giá sản phẩm (Sauce Labs Backpack) | CSS Selector | `.inventory_item:has(#item_4_title_link) .inventory_item_price` | Chưa kiểm tra | `screenshots/09-product-price-css.png` |
| 10 | Giá sản phẩm (Sauce Labs Backpack) | XPath | `//*[@id="item_4_title_link"]/ancestor::div[contains(@class,"inventory_item")]//div[contains(@class,"inventory_item_price")]` | Chưa kiểm tra | `screenshots/10-product-price-xpath.png` |

## 2. Cách kiểm tra locator

### Username, Password và Login
1. Mở https://www.saucedemo.com/.
2. Nhấn `F12` để mở DevTools.
3. Chọn tab **Elements**.
4. Nhấn `Ctrl + F`.
5. Dán từng locator trong bảng vào ô tìm kiếm.
6. Kiểm tra Chrome hiển thị `1 of 1` và phần tử được tô sáng.
7. Chụp ảnh màn hình, lưu vào thư mục `screenshots/` theo tên trong bảng.

### Tên sản phẩm và giá sản phẩm
1. Đăng nhập bằng tài khoản demo trên trang. Thông tin tài khoản được hiển thị trên chính trang đăng nhập.
2. Ở trang Products, xác định sản phẩm **Sauce Labs Backpack**.
3. Mở DevTools → **Elements** → nhấn `Ctrl + F`.
4. Kiểm tra lần lượt locator tên và giá sản phẩm.
5. Chỉ ghi **Đạt (1 of 1)** nếu Chrome thực sự trả về đúng một kết quả. Nếu trả về `0 of 0` hoặc nhiều kết quả, hãy sửa locator và kiểm tra lại.
6. Chụp ảnh minh chứng có cả locator và số kết quả tìm được.

## 3. Quy ước ảnh minh chứng

Tạo thư mục `screenshots` trong cùng thư mục với `README.md`. Mỗi ảnh nên thể hiện:
- Trang SauceDemo đang mở đúng màn hình.
- DevTools đang ở tab Elements.
- Locator được nhập trong ô tìm kiếm Ctrl+F.
- Kết quả `1 of 1` hiển thị rõ ràng.

## 4. Kết luận

Bảng trên liệt kê hai locator đề xuất cho mỗi phần tử được yêu cầu. Kết quả hiện để **Chưa kiểm tra** nhằm tránh khẳng định sai khi chưa thực thi trực tiếp trên trình duyệt. Sau khi kiểm tra, cập nhật kết quả thực tế và bổ sung ảnh chụp vào thư mục `screenshots/`.
