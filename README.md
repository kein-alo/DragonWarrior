# 🥊 Website CLB Boxing & Kickboxing

Website chính thức của câu lạc bộ **Boxing & Kickboxing** — nơi giới thiệu CLB, lịch tập, huấn luyện viên, bảng giá và cho phép học viên mới đăng ký tập thử trực tuyến.

![Status](https://img.shields.io/badge/status-active-success)
![License](https://img.shields.io/badge/license-MIT-blue)

---

## 📖 Giới thiệu

CLB Boxing & Kickboxing là nơi dành cho mọi người yêu thích bộ môn đối kháng, từ người mới bắt đầu muốn rèn luyện sức khỏe, giảm cân, tự vệ cho đến vận động viên thi đấu chuyên nghiệp.

Website được xây dựng nhằm:

- Giới thiệu CLB, triết lý và môi trường tập luyện
- Công khai lịch tập, học phí và thông tin huấn luyện viên
- Thu hút học viên mới thông qua form đăng ký tập thử
- Cập nhật tin tức, giải đấu và thành tích của CLB

---

## ✨ Tính năng

| Tính năng | Mô tả |
|---|---|
| 🏠 Trang chủ | Banner, giới thiệu nhanh, nút kêu gọi đăng ký |
| 👥 Về chúng tôi | Lịch sử, sứ mệnh, cơ sở vật chất |
| 🥋 Bộ môn | Chi tiết Boxing, Kickboxing, thể lực & tự vệ |
| 👨‍🏫 Huấn luyện viên | Hồ sơ, thành tích và chuyên môn của HLV |
| 📅 Lịch tập | Thời khóa biểu theo ngày, theo lớp, theo trình độ |
| 💰 Bảng giá | Các gói tập: tháng, quý, năm, tập kèm 1-1 |
| 📝 Đăng ký tập thử | Form đăng ký, gửi thông tin về email/CRM của CLB |
| 🏆 Thành tích & giải đấu | Kết quả thi đấu, vinh danh võ sĩ |
| 📰 Tin tức / Blog | Bài viết về kỹ thuật, dinh dưỡng, sự kiện |
| 🖼️ Thư viện ảnh & video | Hình ảnh buổi tập, giải đấu |
| 📍 Liên hệ | Bản đồ, số điện thoại, mạng xã hội |

---

## 🛠️ Công nghệ sử dụng

> Cập nhật lại theo công nghệ thực tế của dự án.

- **Frontend:** HTML5, CSS3, JavaScript (hoặc React / Next.js / Vue)
- **Styling:** Tailwind CSS / Bootstrap
- **Backend (tùy chọn):** Node.js + Express / PHP Laravel
- **Cơ sở dữ liệu (tùy chọn):** MongoDB / MySQL
- **Triển khai:** Vercel / Netlify / VPS

---

## 📁 Cấu trúc thư mục

```
boxing-club-website/
├── public/
│   ├── images/
│   ├── videos/
│   └── favicon.ico
├── src/
│   ├── components/      # Header, Footer, Card, Form...
│   ├── pages/           # Home, About, Schedule, Pricing, Contact...
│   ├── styles/
│   ├── data/            # Dữ liệu lịch tập, HLV, bảng giá
│   └── main.js
├── .env.example
├── package.json
└── README.md
```

---

## 🚀 Cài đặt & chạy dự án

### Yêu cầu

- [Node.js](https://nodejs.org/) >= 18
- npm hoặc yarn

### Các bước

```bash
# 1. Clone dự án
git clone https://github.com/<ten-ban>/boxing-club-website.git

# 2. Di chuyển vào thư mục dự án
cd boxing-club-website

# 3. Cài đặt thư viện
npm install

# 4. Tạo file môi trường
cp .env.example .env

# 5. Chạy môi trường phát triển
npm run dev
```

Mở trình duyệt tại `http://localhost:3000` để xem website.

### Build production

```bash
npm run build
npm run start
```

---

## ⚙️ Biến môi trường

Tạo file `.env` với nội dung mẫu:

```env
PORT=3000
CONTACT_EMAIL=clb@example.com
SMTP_HOST=smtp.example.com
SMTP_USER=your-email
SMTP_PASS=your-password
GOOGLE_MAPS_API_KEY=your-key
```

---

## 📅 Ví dụ lịch tập

| Lớp | Thứ | Giờ | Trình độ |
|---|---|---|---|
| Boxing cơ bản | 2 – 4 – 6 | 18:00 – 19:30 | Người mới |
| Kickboxing | 3 – 5 – 7 | 18:00 – 19:30 | Mọi trình độ |
| Thể lực & Conditioning | 2 – 4 | 05:30 – 06:30 | Mọi trình độ |
| Sparring / Đội tuyển | 7 | 08:00 – 10:00 | Nâng cao |

---

## 📸 Hình ảnh demo

<!-- Thay bằng ảnh thực tế của dự án -->
| Trang chủ | Lịch tập |
|---|---|
| ![Home](docs/screenshots/home.png) | ![Schedule](docs/screenshots/schedule.png) |

---

## 🗺️ Lộ trình phát triển

- [x] Giao diện trang chủ, giới thiệu, liên hệ
- [x] Trang lịch tập và bảng giá
- [ ] Form đăng ký tập thử gửi email tự động
- [ ] Hệ thống đăng nhập cho học viên
- [ ] Đặt lịch tập kèm 1-1 với HLV
- [ ] Đa ngôn ngữ (Tiếng Việt / English)
- [ ] Trang quản trị (Admin) để cập nhật nội dung

---

## 🤝 Đóng góp

Mọi đóng góp đều được hoan nghênh!

1. Fork dự án
2. Tạo nhánh mới: `git checkout -b feature/ten-tinh-nang`
3. Commit thay đổi: `git commit -m "Thêm tính năng ..."`
4. Push lên nhánh: `git push origin feature/ten-tinh-nang`
5. Mở Pull Request

---

## 📞 Liên hệ

- **Địa chỉ:** [Địa chỉ CLB]
- **Điện thoại / Zalo:** [Số điện thoại]
- **Email:** clb@example.com
- **Facebook:** [Link fanpage]
- **Instagram / TikTok:** [Link]

---

## 📄 Giấy phép

Dự án được phát hành theo giấy phép [MIT](LICENSE).

---

<p align="center">
  <b>🥊 Train hard. Fight smart. Stay humble. 🥊</b>
</p>