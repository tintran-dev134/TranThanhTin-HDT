namespace EduHub.Ch01.Domain;

/// <summary>
/// Trạng thái hoạt động của thành viên.
/// </summary>
public enum MemberStatus
{
    /// <summary>Đang hoạt động.</summary>
    Active,
    /// <summary>Đã ngừng hoạt động.</summary>
    Inactive
}

/// <summary>
/// Thành viên sinh viên của câu lạc bộ.
/// </summary>
public class Member
{
    /// <summary>Định danh duy nhất của thành viên.</summary>
    public Guid Id { get; private set; }

    /// <summary>Họ và tên thành viên.</summary>
    public string Name { get; private set; }

    /// <summary>Email liên hệ (được chuẩn hoá chữ thường).</summary>
    public string Email { get; private set; }

    /// <summary>Mã số sinh viên (chuẩn hoá chữ hoa).</summary>
    public string StudentCode { get; private set; }

    /// <summary>Thời điểm tham gia hệ thống (UTC).</summary>
    public DateTime JoinedAt { get; private set; }

    /// <summary>Trạng thái hoạt động của thành viên.</summary>
    public MemberStatus Status { get; private set; }

    /// <summary>
    /// Constructor riêng tư ngăn chặn việc tạo đối tượng tùy tiện từ bên ngoài.
    /// Bắt buộc đi qua Factory method Create(...) để đảm bảo dữ liệu luôn hợp lệ.
    /// </summary>
    private Member(string name, string email, string studentCode)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        StudentCode = studentCode;
        JoinedAt = DateTime.UtcNow;
        Status = MemberStatus.Active;
    }

    /// <summary>
    /// Khởi tạo thành viên mới với dữ liệu hợp lệ.
    /// </summary>
    /// <param name="name">Họ và tên (không được trống, tối thiểu 2 ký tự).</param>
    /// <param name="email">Địa chỉ email (phải chứa '@', tên miền hợp lệ).</param>
    /// <param name="studentCode">Mã số sinh viên (không được trống, tối thiểu 3 ký tự).</param>
    /// <exception cref="ArgumentException">Bắn ra khi dữ liệu đầu vào không hợp lệ.</exception>
    public static Member Create(string name, string email, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Trim().Length < 2)
            throw new ArgumentException("Họ và tên không được để trống và phải có ít nhất 2 ký tự.", nameof(name));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') || email.IndexOf('@') == 0 || email.EndsWith('@') || email.Contains(' '))
            throw new ArgumentException("Email không đúng định dạng.", nameof(email));

        if (string.IsNullOrWhiteSpace(studentCode) || studentCode.Trim().Length < 3)
            throw new ArgumentException("Mã số sinh viên không được để trống và phải có ít nhất 3 ký tự.", nameof(studentCode));

        return new Member(
            name.Trim(),
            email.Trim().ToLowerInvariant(),
            studentCode.Trim().ToUpperInvariant());
    }

    /// <summary>Kiểm tra thành viên có đang trong trạng thái hoạt động hay không.</summary>
    public bool IsActive() => Status == MemberStatus.Active;

    /// <summary>Ngừng hoạt động của thành viên trong hệ thống.</summary>
    public void Deactivate() => Status = MemberStatus.Inactive;

    /// <summary>Kích hoạt lại thành viên trong hệ thống.</summary>
    public void Activate() => Status = MemberStatus.Active;

    /// <inheritdoc />
    public override string ToString() => $"{Name} ({StudentCode})";
}
