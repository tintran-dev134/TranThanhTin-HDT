namespace EduHub.Ch01.Domain;

/// <summary>
/// Các hằng số định nghĩa trạng thái của phiếu đăng ký.
/// Tránh sử dụng magic string rải rác trong mã nguồn.
/// </summary>
public static class RegistrationStatus
{
    /// <summary>Mới đăng ký, đang chờ xác nhận.</summary>
    public const string Pending = "Pending";

    /// <summary>Đã được xác nhận hợp lệ.</summary>
    public const string Confirmed = "Confirmed";

    /// <summary>Đã bị huỷ bỏ (trạng thái kết thúc của vòng đời).</summary>
    public const string Cancelled = "Cancelled";
}

/// <summary>
/// Phiếu đăng ký của một thành viên cho một sự kiện (bảng nối n - n).
/// </summary>
public class Registration
{
    /// <summary>Định danh duy nhất của phiếu đăng ký.</summary>
    public Guid Id { get; private set; }

    /// <summary>Mã thành viên đăng ký (khoá ngoại MemberId).</summary>
    public Guid MemberId { get; private set; }

    /// <summary>Mã sự kiện được đăng ký (khoá ngoại EventId).</summary>
    public Guid EventId { get; private set; }

    /// <summary>Thời điểm thực hiện đăng ký (UTC).</summary>
    public DateTime RegisteredAt { get; private set; }

    /// <summary>Trạng thái hiện tại của phiếu đăng ký.</summary>
    public string Status { get; private set; }

    /// <summary>
    /// Constructor riêng tư ngăn chặn việc tạo phiếu không qua quy trình kiểm soát.
    /// Khởi tạo trạng thái mặc định luôn là Pending.
    /// </summary>
    private Registration(Guid memberId, Guid eventId)
    {
        Id = Guid.NewGuid();
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = DateTime.UtcNow;
        Status = RegistrationStatus.Pending;
    }

    /// <summary>
    /// Khởi tạo một phiếu đăng ký mới ở trạng thái Pending.
    /// </summary>
    /// <param name="memberId">Mã thành viên đăng ký.</param>
    /// <param name="eventId">Mã sự kiện.</param>
    /// <exception cref="ArgumentException">Bắn ra khi MemberId hoặc EventId rỗng.</exception>
    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
            throw new ArgumentException("Mã thành viên đăng ký (MemberId) không được để rỗng.", nameof(memberId));

        if (eventId == Guid.Empty)
            throw new ArgumentException("Mã sự kiện đăng ký (EventId) không được để rỗng.", nameof(eventId));

        return new Registration(memberId, eventId);
    }

    /// <summary>
    /// Xác nhận phiếu đăng ký. Quy tắc nghiệp vụ: Chỉ hợp lệ khi phiếu đang ở trạng thái Pending.
    /// </summary>
    /// <exception cref="InvalidOperationException">Bắn ra khi phiếu không ở trạng thái Pending (ví dụ: đã huỷ).</exception>
    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
            throw new InvalidOperationException($"Không thể xác nhận phiếu đang ở trạng thái {Status}.");

        Status = RegistrationStatus.Confirmed;
    }

    /// <summary>
    /// Huỷ phiếu đăng ký. Hợp lệ khi phiếu đang Pending hoặc Confirmed.
    /// </summary>
    /// <exception cref="InvalidOperationException">Bắn ra khi phiếu đã ở trạng thái Cancelled trước đó.</exception>
    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("Phiếu đã được huỷ trước đó.");

        Status = RegistrationStatus.Cancelled;
    }

    /// <summary>Kiểm tra phiếu đã được xác nhận hay chưa.</summary>
    public bool IsConfirmed() => Status == RegistrationStatus.Confirmed;

    /// <summary>Kiểm tra phiếu có đang chờ xác nhận hay không.</summary>
    public bool IsPending() => Status == RegistrationStatus.Pending;

    /// <summary>Kiểm tra phiếu đã bị huỷ hay chưa.</summary>
    public bool IsCancelled() => Status == RegistrationStatus.Cancelled;

    /// <inheritdoc />
    public override string ToString() => $"Phiếu [{Id.ToString()[..8]}] - Trạng thái: {Status}";
}
