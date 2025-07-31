## ⚙️ Yêu cầu để chạy dự án goPet J2ME

Dự án này được phát triển bằng NetBeans và Java ME, bạn cần chuẩn bị các công cụ sau:

### 🛠 Công cụ & môi trường cần thiết:

- **NetBeans IDE 8.x**  
  Khuyến nghị dùng bản 8.2 để tương thích tốt nhất với Java ME plugin.

- **Java ME SDK / Wireless Toolkit (WTK)**  
  - Tải từ: [https://www.oracle.com/java/technologies/javame-sdk.html](https://www.oracle.com/java/technologies/javame-sdk.html)
  - Hoặc dùng bản cũ: Java Wireless Toolkit 2.5.2

- **Java Platform Manager trong NetBeans:**
  - Thêm Java ME SDK / WTK vào:  
    `Tools → Java Platforms → Add Platform → Java ME MIDP Platform Emulator`

### ✅ Cấu hình Project trong NetBeans

1. Mở NetBeans và chọn `File → Open Project`, trỏ đến thư mục `goPet-J2ME`.
2. Kiểm tra **Java Platform** được chọn là `Java ME MIDP Platform Emulator`.
3. Nhấn `Run` hoặc `Build` để biên dịch và chạy.

### 📌 Lưu ý

- Dự án sử dụng profile `CLDC 1.1` và `MIDP 2.0`.
- Nếu gặp lỗi không nhận emulator, hãy kiểm tra lại đường dẫn SDK trong Java Platform Manager.

---
