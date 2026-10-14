# User Stories — EduHub

Tài liệu đặc tả User Stories cho hệ thống quản lý câu lạc bộ và sự kiện EduHub, bao gồm đầy đủ 3 vai trò: **Sinh viên**, **Ban CLB**, và **Admin**.

---

## 1. Vai trò: Sinh viên (Student)

### US-01 · Sinh viên: Đánh giá sự kiện
*Là một sinh viên đã tham dự sự kiện, tôi muốn gửi đánh giá và nhận xét về sự kiện, để đóng góp ý kiến giúp câu lạc bộ nâng cao chất lượng các hoạt động tiếp theo.*

- **Tiêu chí nghiệm thu 1:** Sinh viên đã check-in thành công có thể gửi đánh giá từ 1 đến 5 sao và bình luận; hệ thống lưu đánh giá và cập nhật điểm trung bình của sự kiện.
- **Tiêu chí nghiệm thu 2:** Sinh viên chưa check-in hoặc chưa đăng ký khi gửi đánh giá sẽ bị hệ thống từ chối và báo lỗi.

---

### US-02 · Sinh viên: Hủy đăng ký sự kiện
*Là một sinh viên đã đăng ký tham gia sự kiện, tôi muốn có thể hủy phiếu đăng ký khi có việc đột xuất, để nhường cơ hội tham dự cho các bạn sinh viên khác.*

- **Tiêu chí nghiệm thu 1:** Sinh viên có thể hủy phiếu đăng ký trước thời gian diễn ra sự kiện; trạng thái phiếu chuyển sang `Cancelled` và số chỗ trống của sự kiện được hoàn lại.
- **Tiêu chí nghiệm thu 2:** Sinh viên không thể hủy nếu sự kiện đã hoặc đang diễn ra; hệ thống từ chối và báo lỗi.

---

## 2. Vai trò: Ban chủ nhiệm CLB (Club Board)

### US-03 · Ban CLB: Duyệt đơn đăng ký sự kiện
*Là Ban chủ nhiệm CLB tổ chức sự kiện, tôi muốn duyệt các đơn đăng ký của sinh viên, để xác nhận danh sách tham gia chính thức.*

- **Tiêu chí nghiệm thu 1:** Ban CLB duyệt phiếu `Pending` của sinh viên; trạng thái chuyển sang `Confirmed` và hệ thống gửi thông báo xác nhận.
- **Tiêu chí nghiệm thu 2:** Với các phiếu đã ở trạng thái `Cancelled` hoặc `Confirmed`, hệ thống không cho phép duyệt lại và báo lỗi.

---

### US-04 · Ban CLB: Cập nhật thông tin CLB
*Là Ban chủ nhiệm CLB, tôi muốn cập nhật thông tin tên và mô tả của câu lạc bộ, để giới thiệu chính xác hoạt động và định hướng của CLB đến sinh viên.*

- **Tiêu chí nghiệm thu 1:** Ban CLB nhập tên và mô tả hợp lệ; hệ thống cập nhật thông tin mới lên trang của câu lạc bộ.
- **Tiêu chí nghiệm thu 2:** Nếu tên câu lạc bộ bị để trống, hệ thống sẽ từ chối lưu và báo lỗi.

---

## 3. Vai trò: Quản trị viên (Admin)

### US-05 · Admin: Duyệt đề xuất tạo CLB mới
*Là Quản trị viên hệ thống, tôi muốn xem xét và phê duyệt các đề xuất thành lập câu lạc bộ mới, để đảm bảo các CLB hoạt động đúng tôn chỉ và quy định của nhà trường.*

- **Tiêu chí nghiệm thu 1:** Admin chấp nhận đề xuất tạo CLB đang chờ duyệt; hệ thống khởi tạo CLB mới.
- **Tiêu chí nghiệm thu 2:** Đề xuất đã được xử lý trước đó khi bấm duyệt lại sẽ bị hệ thống từ chối và báo lỗi.
