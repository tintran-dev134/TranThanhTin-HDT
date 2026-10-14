# User Stories — EduHub

Tập hợp các User Story mô tả các yêu cầu chức năng chính của hệ thống EduHub, bao gồm 3 vai trò người dùng (Sinh viên, Ban chủ nhiệm CLB, Quản trị viên hệ thống).

---

## 1. Vai trò: Sinh viên (Student)

### US-01 · Tham gia vào một Câu lạc bộ
Là một sinh viên, tôi muốn tham gia vào một CLB, để có thể sinh hoạt học thuật và giao lưu cùng các bạn cùng sở thích.
- **Tiêu chí 1 (Chưa tham gia):** Given sinh viên chưa có trong CLB, When thực hiện tham gia, Then sinh viên được ghi danh vào `club.Members` và tăng số lượng thành viên.
- **Tiêu chí 2 (Chặn trùng lặp):** Given sinh viên đã có tên trong CLB (theo Id hoặc MSSV), When gửi yêu cầu tham gia lại, Then hệ thống từ chối và báo lỗi không cho phép trùng lặp.

### US-02 · Đăng ký tham gia sự kiện
Là một sinh viên, tôi muốn đăng ký tham dự một sự kiện do CLB tổ chức, để giữ chỗ và có cơ hội học hỏi kỹ năng mới.
- **Tiêu chí 1 (Còn chỗ):** Given sự kiện còn sức chứa (`registeredCount < Capacity`), When sinh viên bấm đăng ký, Then hệ thống tạo mới một phiếu `Registration` ở trạng thái `Pending`.
- **Tiêu chí 2 (Hết chỗ):** Given sự kiện đã đủ người đăng ký (`registeredCount >= Capacity`), When sinh viên bấm đăng ký, Then hệ thống từ chối tiếp nhận và thông báo sự kiện đã kín chỗ.

### US-03 · Điểm danh (Check-in) tại sự kiện
Là một sinh viên đã đăng ký, tôi muốn thực hiện điểm danh bằng mã QR khi đến sự kiện, để ban tổ chức ghi nhận sự hiện diện thực tế của mình.
- **Tiêu chí 1 (Phiếu hợp lệ):** Given sinh viên sở hữu phiếu đăng ký ở trạng thái `Confirmed`, When thực hiện quét mã QR check-in, Then hệ thống tạo bản ghi `CheckIn` gắn với phiếu và lưu thời gian thực tế.
- **Tiêu chí 2 (Phiếu chưa hợp lệ):** Given phiếu đang ở trạng thái `Pending` hoặc đã bị `Cancelled`, When sinh viên cố tình check-in, Then hệ thống ngăn chặn và báo lỗi yêu cầu phiếu phải được xác nhận trước.

---

## 2. Vai trò: Ban chủ nhiệm CLB (Club Board)

### US-04 · Khởi tạo sự kiện mới
Là Ban chủ nhiệm CLB, tôi muốn tạo một sự kiện mới với thời gian và giới hạn số lượng tham dự, để quảng bá và thu hút hội viên tham gia hoạt động.
- **Tiêu chí 1 (Thông tin hợp lệ):** Given tiêu đề sự kiện hợp lệ và sức chứa `Capacity > 0`, When tạo sự kiện, Then sự kiện được tạo thành công và liên kết chính xác với `ClubId`.
- **Tiêu chí 2 (Dữ liệu sai):** Given tiêu đề để trống hoặc sức chứa `<= 0`, When khởi tạo, Then hệ thống ném ra ngoại lệ và từ chối tạo sự kiện.

### US-05 · Theo dõi và xuất báo cáo tham dự
Là Ban chủ nhiệm CLB, tôi muốn xem báo cáo tổng kết tham dự sự kiện, để đánh giá độ quan tâm và tỷ lệ có mặt thực tế của sinh viên.
- **Tiêu chí 1 (Thống kê số liệu):** Hệ thống hiển thị trực quan tỷ lệ lấp đầy (đăng ký / sức chứa) và tỷ lệ tham dự thực tế (check-in / đăng ký).
- **Tiêu chí 2 (Danh sách chi tiết):** Hiển thị danh sách cụ thể từng người tham gia kèm trạng thái phiếu (`Confirmed`, `Cancelled`) và thời gian điểm danh chi tiết.

---

## 3. Vai trò: Quản trị viên hệ thống (Admin)

### US-06 · Quản lý trạng thái hoạt động của thành viên
Là Quản trị viên hệ thống, tôi muốn có thể kích hoạt hoặc tạm dừng trạng thái hoạt động của một thành viên, để bảo đảm danh sách quản lý sinh viên luôn chính xác.
- **Tiêu chí 1 (Ngừng hoạt động):** Given thành viên đang ở trạng thái `Active`, When admin gọi `Deactivate()`, Then `IsActive()` trả về `false` và trạng thái chuyển sang `Inactive`.
- **Tiêu chí 2 (Bảo toàn dữ liệu):** Khi tài khoản bị vô hiệu hoá, lịch sử các sự kiện và phiếu đăng ký trước đó của thành viên vẫn được lưu trữ nguyên vẹn để phục vụ báo cáo.

