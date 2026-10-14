namespace EduHub.Ch01.Domain;

/// <summary>
/// Các hình thức điểm danh hợp lệ của hệ thống.
/// </summary>
public static class CheckInMethod
{
    /// <summary>Ban tổ chức điểm danh thủ công qua danh sách.</summary>
    public const string Manual = "Manual";

    /// <summary>Sinh viên quét mã QR tại sự kiện.</summary>
    public const string QrCode = "QrCode";
}

/// <summary>
/// Ghi nhận sự hiện diện thực tế của một phiếu đăng ký tham dự sự kiện (quan hệ 1 - 0..1).
/// </summary>
public class CheckIn
{
    /// <summary>Định danh duy nhất của lượt điểm danh.</summary>
    public Guid Id { get; private set; }

    /// <summary>Định danh phiếu đăng ký được điểm danh (khoá ngoại 1-1 RegistrationId).</summary>
    public Guid RegistrationId { get; private set; }

    /// <summary>Thời điểm điểm danh thành công (UTC).</summary>
    public DateTime CheckedInAt { get; private set; }

    /// <summary>Hình thức điểm danh (Manual hoặc QrCode).</summary>
    public string Method { get; private set; }

    /// <summary>Constructor riêng tư bảo vệ quy tắc tạo điểm danh.</summary>
    private CheckIn(Guid registrationId, string method)
    {
        Id = Guid.NewGuid();
        RegistrationId = registrationId;
        CheckedInAt = DateTime.UtcNow;
        Method = method;
    }

    /// <summary>
    /// Điểm danh cho một phiếu đăng ký tham dự sự kiện.
    /// Quy tắc nghiệp vụ: Chỉ những phiếu đã được xác nhận (Confirmed) mới được phép check-in.
    /// </summary>
    /// <param name="registration">Phiếu đăng ký tham dự.</param>
    /// <param name="method">Hình thức điểm danh (mặc định: Manual).</param>
    /// <exception cref="ArgumentNullException">Bắn ra khi registration là null.</exception>
    /// <exception cref="InvalidOperationException">Bắn ra khi phiếu chưa Confirmed.</exception>
    /// <exception cref="ArgumentException">Bắn ra khi hình thức điểm danh không hợp lệ.</exception>
    public static CheckIn Create(Registration registration, string method = CheckInMethod.Manual)
    {
        ArgumentNullException.ThrowIfNull(registration);

        if (registration.Status != RegistrationStatus.Confirmed)
            throw new InvalidOperationException($"Chỉ phiếu đã ở trạng thái Confirmed mới được phép điểm danh. Trạng thái hiện tại: {registration.Status}.");

        if (method is not (CheckInMethod.Manual or CheckInMethod.QrCode))
            throw new ArgumentException($"Hình thức điểm danh không hợp lệ: '{method}'. Phải là '{CheckInMethod.Manual}' hoặc '{CheckInMethod.QrCode}'.", nameof(method));

        return new CheckIn(registration.Id, method);
    }

    /// <summary>Xuất chuỗi tóm tắt lượt điểm danh để đưa vào báo cáo.</summary>
    public string Summary() => $"Điểm danh thành công lúc {CheckedInAt:HH:mm:ss} UTC qua {Method} (Mã phiếu: {RegistrationId.ToString()[..8]})";

    /// <inheritdoc />
    public override string ToString() => Summary();
}
