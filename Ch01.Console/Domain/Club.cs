namespace EduHub.Ch01.Domain;

/// <summary>
/// Câu lạc bộ sinh viên.
/// </summary>
public class Club
{
    private readonly List<Member> _members = [];

    /// <summary>Định danh duy nhất của câu lạc bộ.</summary>
    public Guid Id { get; private set; }

    /// <summary>Tên câu lạc bộ.</summary>
    public string Name { get; private set; }

    /// <summary>Mô tả mục tiêu hoạt động của CLB.</summary>
    public string Description { get; private set; }

    /// <summary>Thời điểm thành lập (UTC).</summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Danh sách thành viên dưới dạng chỉ đọc (Read-only).
    /// Bên ngoài không thể can thiệp trực tiếp mà phải thông qua <see cref="AddMember"/>.
    /// </summary>
    public IReadOnlyList<Member> Members => _members;

    /// <summary>Constructor riêng tư bảo vệ tính toàn vẹn nghiệp vụ.</summary>
    private Club(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Khởi tạo một câu lạc bộ mới.
    /// </summary>
    /// <param name="name">Tên câu lạc bộ (không được để trống).</param>
    /// <param name="description">Mô tả câu lạc bộ.</param>
    /// <exception cref="ArgumentException">Bắn ra khi tên câu lạc bộ trống.</exception>
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên CLB không được để trống.", nameof(name));

        return new Club(name.Trim(), description?.Trim() ?? string.Empty);
    }

    /// <summary>
    /// Thêm thành viên vào CLB, kiểm tra chặt chẽ trùng lặp theo cả Id và MSSV.
    /// </summary>
    /// <param name="member">Thành viên cần thêm.</param>
    /// <exception cref="ArgumentNullException">Bắn ra khi thành viên là null.</exception>
    /// <exception cref="InvalidOperationException">Bắn ra khi thành viên đã tham gia CLB.</exception>
    public void AddMember(Member member)
    {
        ArgumentNullException.ThrowIfNull(member);

        if (_members.Any(m => m.Id == member.Id || m.StudentCode.Equals(member.StudentCode, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"Sinh viên '{member}' đã là thành viên của CLB {Name}.");

        _members.Add(member);
    }

    /// <summary>Xoá thành viên khỏi CLB theo mã định danh.</summary>
    public bool RemoveMember(Guid memberId)
    {
        var existing = _members.FirstOrDefault(m => m.Id == memberId);
        if (existing is null) return false;
        return _members.Remove(existing);
    }

    /// <summary>Kiểm tra một thành viên có thuộc CLB hay không.</summary>
    public bool ContainsMember(Guid memberId) => _members.Any(m => m.Id == memberId);

    /// <summary>Lấy tổng số lượng thành viên hiện tại của CLB.</summary>
    public int MemberCount() => _members.Count;

    /// <inheritdoc />
    public override string ToString() => $"{Name} ({MemberCount()} thành viên)";
}
